namespace MaksimShimshon.GameManagePanel.Kernel.ConsoleController;


public interface ICrazyReport
{
    void SetModule(string moduleName);
    void Report(string line);
    void Report(string format, params object?[]? arg);
    void ReportError(string line);
    void ReportError(string format, params object?[]? arg);
    void ReportWarning(string line);
    void ReportWarning(string format, params object?[]? arg);
    void ReportInfo(string line);
    void ReportInfo(string format, params object?[]? arg);

    void ReportSuccess(string line);
    void ReportSuccess(string format, params object?[]? arg);
}
