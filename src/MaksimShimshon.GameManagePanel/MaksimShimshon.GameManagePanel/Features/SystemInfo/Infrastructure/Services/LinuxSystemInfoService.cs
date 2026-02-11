using LunaticPanel.Core.Abstraction.Tools.LinuxCommand;
using MaksimShimshon.GameManagePanel.Features.LinuxGameServer;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Services;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.ValueObjects;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Configurations;
using MaksimShimshon.GameManagePanel.Kernel.Configuration;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Services;

internal class LinuxSystemInfoService : ISystemInfoService
{
    private readonly ILinuxCommand _linuxCommand;
    private readonly LinuxSystemInfoConfiguration _linuxSystemInfoConfiguration;
    private readonly PluginConfiguration _pluginConfiguration;
    private readonly string _bashFolder;
    public LinuxSystemInfoService(ILinuxCommand linuxCommand, LinuxSystemInfoConfiguration linuxSystemInfoConfiguration, PluginConfiguration pluginConfiguration)
    {
        _linuxCommand = linuxCommand;
        _linuxSystemInfoConfiguration = linuxSystemInfoConfiguration;
        _pluginConfiguration = pluginConfiguration;
        _bashFolder = pluginConfiguration.GetBashBase("systeminfo");
    }

    public async Task<SystemInfoEntity?> GetSystemInfoAsync(CancellationToken ct = default)
    {
        var ramScript = _pluginConfiguration.GetBashFor(SystemInfoModule.ModuleName, "get_ram_info.sh");
        var ram = await _linuxCommand.RunLinuxScript(ramScript, true, ct);

        var ramUsage = float.Parse(ram.StandardOutput.Split(';')[0]);
        var ramTotal = float.Parse(ram.StandardOutput.Split(';')[1]);

        var diskScript = _pluginConfiguration.GetBashFor(SystemInfoModule.ModuleName, "get_disk_info.sh", _linuxSystemInfoConfiguration.WorkingDisk);
        var disk = await _linuxCommand.RunLinuxScript(diskScript, true, ct);
        var diskUsage = float.Parse(disk.StandardOutput.Split(';')[0]);
        var diskTotal = float.Parse(disk.StandardOutput.Split(';')[1]);

        var processorScript = _pluginConfiguration.GetBashFor(SystemInfoModule.ModuleName, "get_cpu_info.sh");
        var processor = await _linuxCommand.RunLinuxScript(processorScript, true, ct);
        var processorUsage = float.Parse(processor.StandardOutput.Split(';')[0]);
        var processorCores = int.Parse(processor.StandardOutput.Split(';')[1]);
        var processorModel = processor.StandardOutput.Split(';')[2];

        SystemInfoEntity? result = default;
        if (ram != default && disk != default && processor != default)
            result = new SystemInfoEntity()
            {
                Disk = new(diskUsage, diskTotal),
                Memory = new(ramUsage, ramTotal),
                Processor = new SystemProcessor(processorUsage, processorCores, processorModel)
            };

        return result;
    }

}
