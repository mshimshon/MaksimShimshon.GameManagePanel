using MaksimShimshon.GameManagePanel.Features.SystemInfo.Application.Services;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.Entites;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Domain.ValueObjects;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Configurations;
using MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Services.Helpers;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Services;

internal class WindowsSystemInfoService : ISystemInfoService
{
    private readonly WindowsSystemInfoConfiguration _configuration;

    public WindowsSystemInfoService(WindowsSystemInfoConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<SystemInfoEntity?> GetSystemInfoAsync(CancellationToken ct = default)
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
    => Task.FromResult(new SystemInfoEntity()
    {
        Disk = GetDiskInfo(),
        Memory = GetRamInfo(),
        Processor = GetProcessorInfo()
    }
        );
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

    private SystemDisk GetDiskInfo()
    {
        var drive = new DriveInfo(_configuration.WorkingDrive.ToString());

        long total = drive.TotalSize;
        long free = drive.TotalFreeSpace;

        float totalGb = total / 1024f / 1024f / 1024f;
        float freeGb = free / 1024f / 1024f / 1024f;
        float usedGb = totalGb - freeGb;

        return new SystemDisk(usedGb, totalGb);

    }
    const float GB = 1024f * 1024f * 1024f;
    private SystemMemory GetRamInfo()
    {
        var (total, available, load) = WindowsResources.SystemRam();

        float totalGb = total / GB;
        float usedGb = totalGb * (load / 100f);
        return new SystemMemory(usedGb, totalGb);
    }
    private SystemProcessor GetProcessorInfo()
    {
        float cpuLoadPercent = WindowsResources.GetCpuLoad();
        int logicalCores = Environment.ProcessorCount;
        string cpuModel = WindowsResources.GetCpuModel();
        return new SystemProcessor(cpuLoadPercent, logicalCores, cpuModel);
    }



}


