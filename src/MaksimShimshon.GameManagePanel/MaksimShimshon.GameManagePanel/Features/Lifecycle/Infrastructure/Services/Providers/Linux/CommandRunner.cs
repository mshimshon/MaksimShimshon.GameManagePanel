using System.Diagnostics;
using System.Text;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Infrastructure.Services.Providers.Linux;

public class CommandRunner
{
    private readonly string _tmpPath;
    private readonly string _bash;
    public CommandRunner()
    {
        _tmpPath = Path.Combine("/", "tmp", "lunaticpanel");
        if (!Directory.Exists(_tmpPath))
            Directory.CreateDirectory(_tmpPath);

        _bash = Path.Combine("/", "bin", "bash");

    }

    public async Task<string> RunLinuxScript(string file)
    {

        var psi = new ProcessStartInfo
        {
            FileName = _bash,
            Arguments = $"-c \"{file}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using var process = new Process { StartInfo = psi };

        var stdout = new StringBuilder();
        var stderr = new StringBuilder();

        process.OutputDataReceived += (_, e) => { if (e.Data != null) stdout.AppendLine(e.Data); };
        process.ErrorDataReceived += (_, e) => { if (e.Data != null) stderr.AppendLine(e.Data); };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        string output = stdout.ToString();
        string error = stderr.ToString();

        return output + error;
    }

    public async Task<string> RunLinuxCommand(string command)
    {
        var script = Path.Combine(_tmpPath, Path.GetRandomFileName()) + ".sh";
        await File.WriteAllTextAsync(script, command);
        Console.WriteLine(script);
        var psi = new ProcessStartInfo
        {
            FileName = _bash,
            Arguments = script,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using var process = new Process { StartInfo = psi };

        var stdout = new StringBuilder();
        var stderr = new StringBuilder();

        process.OutputDataReceived += (_, e) => { if (e.Data != null) stdout.AppendLine(e.Data); };
        process.ErrorDataReceived += (_, e) => { if (e.Data != null) stderr.AppendLine(e.Data); };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        string output = stdout.ToString();
        string error = stderr.ToString();
        File.Delete(script);
        return output + error;
    }

}
