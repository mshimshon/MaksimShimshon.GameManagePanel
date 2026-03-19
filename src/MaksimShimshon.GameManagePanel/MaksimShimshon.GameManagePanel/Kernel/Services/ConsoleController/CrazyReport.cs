using LunaticPanel.Core.Abstraction.Circuit;

namespace MaksimShimshon.GameManagePanel.Kernel.Services.ConsoleController;

internal class CrazyReport<TClass> : CrazyReport, ICrazyReport<TClass> where TClass : class
{
    public CrazyReport(ICircuitRegistry circuitRegistry) : base(circuitRegistry)
    {
        SetClass<TClass>();
    }
}

internal class CrazyReport : ICrazyReport
{
    private string? _moduleName = "[System]";
    private string? _className = "[Object]";
    Guid _circuitId;
    public CrazyReport(ICircuitRegistry circuitRegistry)
    {
        _circuitId = circuitRegistry?.CurrentCircuit?.CircuitId ?? Guid.Empty;
    }

    public void Report(string line) => Console.WriteLine(line);
    public void Report(string format, params object?[]? arg) => Console.WriteLine(format, arg);
    public void ReportError(string line)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"{GetPrefix()}: {line}");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportError(string format, params object?[]? arg)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"{GetPrefix()}: {format}", arg);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportInfo(string line)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"{GetPrefix()}: {line}");

        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportInfo(string format, params object?[]? arg)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"{GetPrefix()}: {format}", arg);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public void ReportSuccess(string line)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{GetPrefix()}: {line}");

        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportSuccess(string format, params object?[]? arg)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{GetPrefix()}: {format}", arg);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public void ReportWarning(string line)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{GetPrefix()}: {line}");

        Console.ForegroundColor = ConsoleColor.Gray;
    }
    public void ReportWarning(string format, params object?[]? arg)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{GetPrefix()}: {format}", arg);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    private string GetPrefix()
    {
        List<string> prefixes = new() {
            $"[{DateTime.Now.ToString()}]"
        };
        if (_circuitId != Guid.Empty)
            prefixes.Add($"[{_circuitId}]");
        if (!string.IsNullOrWhiteSpace(_moduleName))
            prefixes.Add($"[{_moduleName}]");
        if (!string.IsNullOrWhiteSpace(_className))
            prefixes.Add($"[{_className}]");
        return string.Join("::", prefixes);
    }

    public void SetModule(string moduleName) => _moduleName = moduleName;
    public void SetModule<TClass>(string moduleName) where TClass : class
    {
        SetModule(moduleName);
        SetClass<TClass>();
    }

    public void SetClass<TClass>() where TClass : class
    {
        _className = typeof(TClass).Name;

    }
}
