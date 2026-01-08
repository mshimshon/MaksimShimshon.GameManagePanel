using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MaksimShimshon.GameManagePanel.Features.SystemInfo.Infrastructure.Services.Helpers;



internal static class WindowsResources
{
    public static float GetCpuLoad()
    {
        GetSystemTimes(out var idle1, out var kernel1, out var user1);
        Thread.Sleep(200);
        GetSystemTimes(out var idle2, out var kernel2, out var user2);

        ulong idle = idle2 - idle1;
        ulong kernel = kernel2 - kernel1;
        ulong user = user2 - user1;

        ulong total = kernel + user;
        return total == 0 ? 0f : (1f - (idle / (float)total)) * 100f;
    }

    // Native
    [DllImport("kernel32.dll")]
    static extern bool GetSystemTimes(
        out ulong idleTime,
        out ulong kernelTime,
        out ulong userTime
    );

    public static int GetPhysicalCoreCount()
    {
        uint len = 0;
        GetLogicalProcessorInformationEx(0, IntPtr.Zero, ref len);

        var ptr = Marshal.AllocHGlobal((int)len);
        GetLogicalProcessorInformationEx(0, ptr, ref len);

        int offset = 0;
        int cores = 0;

        while (offset < len)
        {
            var info = Marshal.PtrToStructure<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX>(
                ptr + offset);

            if (info.Relationship == LOGICAL_PROCESSOR_RELATIONSHIP.RelationProcessorCore)
                cores++;

            offset += (int)info.Size;
        }

        Marshal.FreeHGlobal(ptr);
        return cores;
    }

    public static string GetCpuModel()
    {
        int size = 256;
        var sb = new System.Text.StringBuilder(size);
        RegGetValue(
            new IntPtr(unchecked((int)0x80000002)),
            @"HARDWARE\DESCRIPTION\System\CentralProcessor\0",
            "ProcessorNameString",
            0x00000002,
            IntPtr.Zero,
            sb,
            ref size);

        return sb.ToString();
    }
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool GetLogicalProcessorInformationEx(
    LOGICAL_PROCESSOR_RELATIONSHIP relationship,
    IntPtr buffer,
    ref uint returnedLength);

    [DllImport("advapi32.dll", SetLastError = true)]
    static extern int RegGetValue(
        IntPtr hKey,
        string lpSubKey,
        string lpValue,
        int dwFlags,
        IntPtr pdwType,
        System.Text.StringBuilder pvData,
        ref int pcbData);

    enum LOGICAL_PROCESSOR_RELATIONSHIP : uint
    {
        RelationProcessorCore = 0
    }

    [StructLayout(LayoutKind.Sequential)]
    struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX
    {
        public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
        public uint Size;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    public static (ulong Total, ulong Available, uint Load) SystemRam()
    {
        var mem = new MEMORYSTATUSEX
        {
            dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>()
        };

        GlobalMemoryStatusEx(ref mem);

        return (mem.ullTotalPhys, mem.ullAvailPhys, mem.dwMemoryLoad);
    }

    public static (long WorkingSet, long PrivateBytes) ProcessRam()
    {
        var p = Process.GetCurrentProcess();
        return (p.WorkingSet64, p.PrivateMemorySize64);
    }
}