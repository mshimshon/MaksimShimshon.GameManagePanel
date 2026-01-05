using System.Diagnostics;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux.Exceptions;

public class FailedToStartProcessException : Exception
{
    public FailedToStartProcessException(ProcessStartInfo processStartInfo) :
        base($"{processStartInfo.FileName} failed to start.")
    {
    }
}
