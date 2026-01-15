using LunaticPanel.Core.Abstraction.Tools;
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
    private readonly string _bashFolder;
    public LinuxSystemInfoService(ILinuxCommand linuxCommand, LinuxSystemInfoConfiguration linuxSystemInfoConfiguration, PluginConfiguration pluginConfiguration)
    {
        _linuxCommand = linuxCommand;
        _linuxSystemInfoConfiguration = linuxSystemInfoConfiguration;
        _bashFolder = pluginConfiguration.GetBashBase("systeminfo");
    }

    public async Task<SystemInfoEntity?> GetSystemInfoAsync(CancellationToken ct = default)
    {
        var ramScript = Path.Combine(_bashFolder, "getraminfo.sh");
        var ram = await _linuxCommand.RunLinuxScript(ramScript);
        Console.WriteLine(ram);
        var ramUsage = float.Parse(ram.Split(';')[0]);
        var ramTotal = float.Parse(ram.Split(';')[1]);

        var diskScript = Path.Combine(_bashFolder, "getdiskinfo.sh");
        var disk = await _linuxCommand.RunLinuxScript(diskScript + " " + _linuxSystemInfoConfiguration.WorkingDisk);
        Console.WriteLine(disk);
        var diskUsage = float.Parse(disk.Split(';')[0]);
        var diskTotal = float.Parse(disk.Split(';')[1]);

        var processorScript = Path.Combine(_bashFolder, "getcpuinfo.sh");
        var processor = await _linuxCommand.RunLinuxScript(processorScript);
        Console.WriteLine(processor);
        var processorUsage = float.Parse(processor.Split(';')[0]);
        var processorCores = int.Parse(processor.Split(';')[1]);
        var processorModel = processor.Split(';')[2];

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
