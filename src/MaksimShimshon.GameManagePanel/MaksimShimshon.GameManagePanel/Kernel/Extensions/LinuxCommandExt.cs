using LunaticPanel.Core.Abstraction.Tools.LinuxCommand;
using System.Text.Json;

namespace MaksimShimshon.GameManagePanel.Kernel.Extensions;

public static class LinuxCommandExt
{
    public static async Task<TResponse> RunLinuxScriptWithReplyAs<TResponse>(this ILinuxCommand linuxCommand, string file, bool sudo = true,
        Func<string, TResponse>? OnAbnormalError = default)
    {
        var response = await linuxCommand.RunLinuxScript(file, sudo);
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
