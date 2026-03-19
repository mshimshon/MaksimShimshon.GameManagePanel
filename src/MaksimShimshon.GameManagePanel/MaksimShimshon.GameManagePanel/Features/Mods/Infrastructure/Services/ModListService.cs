using CoreMap;
using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using MaksimShimshon.GameManagePanel.Core.Features;
using MaksimShimshon.GameManagePanel.Features.Mods.Application.Services;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.Entities;
using MaksimShimshon.GameManagePanel.Features.Mods.Domain.ValueObjects;
using MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services.Dto;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;
using MaksimShimshon.GameManagePanel.Kernel.Exceptions;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace MaksimShimshon.GameManagePanel.Features.Mods.Infrastructure.Services;

internal sealed class ModListService : IModListService
{
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly ICoreMap _coreMap;
    private readonly IQueryBus _queryBus;
    private readonly JsonSerializerOptions _serializerOption = new JsonSerializerOptions()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true
    };
    public ModListService(PluginConfiguration pluginConfiguration, ICoreMap coreMap, IQueryBus queryBus)
    {
        _pluginConfiguration = pluginConfiguration;
        _coreMap = coreMap;
        _queryBus = queryBus;
    }
    public async Task CreateAsync(ModListDescriptor descriptor, CancellationToken ct = default)
    {
        if (descriptor.Id == Guid.Empty)
            throw new WebServiceException($"{descriptor.Id} is not defined.");
        string filename = _pluginConfiguration.GetUserConfigFor(ModListKeys.ModuleName, $"modlist-{descriptor.Id}.json");
        bool fileDoesExist = File.Exists(filename);
        if (fileDoesExist)
            throw new WebServiceException($"ModList {descriptor.Id} already exist.");
        try
        {
            var dto = new ModListResponse()
            {
                Id = descriptor.Id,
                Name = descriptor.Name
            };
            var content = JsonSerializer.Serialize(dto, _serializerOption);
            await File.WriteAllTextAsync(filename, content, ct);
        }
        catch (OperationCanceledException ex)
        {

            throw new WebServiceException($"Operation to create modlist {descriptor.Id} was cancelled.", ex);
        }
        catch
        {
            throw;
        }


    }

    // TODO: Switch Pagination Model with Lightweight DTO usage instead of JsonNode.
    public async Task<ICollection<ModListDescriptor>> GetAllAsync(CancellationToken ct = default)
    {
        string path = _pluginConfiguration.GetUserConfigBase(ModListKeys.ModuleName);
        var modlists = Directory.EnumerateFiles(path, "modlist-*.json");
        List<ModListDescriptor> result = new();
        foreach (var item in modlists)
        {
            try
            {
                var content = await File.ReadAllTextAsync(item, ct);
                var node = JsonNode.Parse(content);
                string? name = node?[nameof(ModListDescriptor.Name)]?.GetValue<string>();
                Guid? id = node?[nameof(ModListDescriptor.Id)]?.GetValue<Guid>();
                if (id == default || name == default)
                    continue;
                result.Add(new((Guid)id!, name));
            }
            catch
            {
                continue;
            }
        }
        return result;
    }

    public async Task<ModListEntity?> GetAsync(Guid id, CancellationToken ct = default)
    {
        string filename = _pluginConfiguration.GetUserConfigFor(ModListKeys.ModuleName, $"modlist-{id}.json");
        bool fileDoesNotExist = !File.Exists(filename);
        if (fileDoesNotExist)
            throw new WebServiceException($"ModList {id} does not exist.");
        try
        {
            string content = await File.ReadAllTextAsync(filename, ct);
            var dto = JsonSerializer.Deserialize<ModListResponse>(content, _serializerOption);
            if (dto == default)
                throw new WebServiceException($"The ModList {id} seems to be corrupted.");
            var result = _coreMap.Map(dto).To<ModListEntity>();
            return result;
        }
        catch (OperationCanceledException ex)
        {
            throw new WebServiceException($"Operation to get modlist {id} was cancelled.", ex);
        }
        catch (JsonException ex)
        {
            throw new WebServiceException($"The ModList {id} seems to be corrupted.", ex);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IReadOnlyCollection<PartSchematicEntity>?> GetSchematic(CancellationToken ct = default)
    {
        var qryResult = await _queryBus.QueryWithoutDataAsync(LifecycleKeys.Queries.GetRawGameInfo);
        var json = await qryResult.ReadAs<string>();
        if (json == default) return default;
        var gameinfo = JsonSerializer.Deserialize<GameInfoResponse>(json, _serializerOption);
        if (gameinfo == default) return default;
        if (gameinfo.Schema == default) return default;
        // TODO: USE COREMAP WHEN 2.0 RELEASES
        List<PartSchematicEntity> result = gameinfo.Schema.Select(p => new PartSchematicEntity(p.Key, p.Value.Name, p.Value.Type)).ToList();
        return result.AsReadOnly();
    }
}
