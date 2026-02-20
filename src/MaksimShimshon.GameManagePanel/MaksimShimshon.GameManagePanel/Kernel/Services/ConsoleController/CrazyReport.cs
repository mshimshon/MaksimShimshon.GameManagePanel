namespace MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;

internal class CrazyReport : ICrazyReport
{
    private string _moduleName = "[System]";
    private string _className = "[Object]";
    public CrazyReport()
    {

    }
    public void Report(string line) => Console.WriteLine(line);
    public void Report(string format, params object?[]? arg) => Console.WriteLine(format, arg);
    public void ReportError(string line)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"{_moduleName}::{_className}:: {line}");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportError(string format, params object?[]? arg)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"{_moduleName}::{_className}:: {format}", arg);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportInfo(string line)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"{_moduleName}::{_className}:: {line}");

        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportInfo(string format, params object?[]? arg)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"{_moduleName}::{_className}:: {format}", arg);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public void ReportSuccess(string line)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{_moduleName}::{_className}:: {line}");

        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportSuccess(string format, params object?[]? arg)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{_moduleName}::{_className}:: {format}", arg);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public void ReportWarning(string line)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{_moduleName}::{_className}:: {line}");

        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportWarning(string format, params object?[]? arg)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{_moduleName}::{_className}:: {format}", arg);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public void SetModule(string moduleName) => _moduleName = $"[{moduleName}]";
    public void SetModule<TClass>(string moduleName) where TClass : class
    {
        SetModule(moduleName);
        _className = $"[{typeof(TClass).Name}]";
    }
}
