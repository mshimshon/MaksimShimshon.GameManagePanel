using CoreMap;
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
    private readonly JsonSerializerOptions _serializerOption = new JsonSerializerOptions()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true
    };
    public ModListService(PluginConfiguration pluginConfiguration, ICoreMap coreMap)
    {
        _pluginConfiguration = pluginConfiguration;
        _coreMap = coreMap;
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
        catch (OperationCanceledException)
        {

            throw new WebServiceException($"Operation to create modlist {descriptor.Id} was cancelled.");
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
        catch (OperationCanceledException)
        {
            throw new WebServiceException($"Operation to get modlist {id} was cancelled.");
        }
        catch (JsonException)
        {
            throw new WebServiceException($"The ModList {id} seems to be corrupted.");
        }
        catch
        {
            throw;
        }
    }
}
