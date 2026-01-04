namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.ValueObjects;

public record StartupParameterAllowedValue
{
    public string Value { get; }
    public string Label { get; }
    public StartupParameterAllowedValue(string value, string label)
    {
        Value = value;
        Label = label;
    }
}
