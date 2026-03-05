using LunaticPanel.Core.Extensions;
using System.Text.Json;

namespace MaksimShimshon.GameManagePanel.Kernel.Extensions;

public static class LinuxCommandExt
{
    public static async Task<TResponse> ExecAndReadAs<TResponse>(this LinuxCommandBuilderContext context, Func<string, TResponse> OnAbnormalError, CancellationToken ct = default)
    {
        var response = await context.LinuxCommand.RunCommand(context.CommandBuilder);
        if (response.Failed && OnAbnormalError != default)
            return OnAbnormalError.Invoke("Error with BASH: " + response.StandardError);

        if (!IsValidJson(response.StandardOutput) && OnAbnormalError != default)
            return OnAbnormalError.Invoke("Invalid JSON: " + response.StandardOutput);

        return JsonSerializer.Deserialize<TResponse>(response.StandardOutput, new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            PropertyNameCaseInsensitive = true,
        })!;
    }

    static bool IsValidJson(string s)
    {
        try
        {
            JsonDocument.Parse(s);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
