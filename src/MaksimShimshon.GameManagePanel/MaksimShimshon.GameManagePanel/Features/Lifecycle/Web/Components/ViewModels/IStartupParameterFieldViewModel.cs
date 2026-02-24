using LunaticPanel.Core.Abstraction.Widgets;
using MaksimShimshon.GameManagePanel.Features.Lifecycle.Domain.Entites;

namespace MaksimShimshon.GameManagePanel.Features.Lifecycle.Web.Components.ViewModels;

public interface IStartupParameterFieldViewModel : IWidgetViewModel
{
    public GameStartupParameterEntity Parameter { get; set; }
    public string Value { get; set; }
    public string InitialValue { get; set; }
    public bool HasValidation { get; }
    public bool IsList { get; }
    public bool IsDecimal { get; }
    public bool IsInt { get; }
    public bool IsNumber { get; }
    bool IsTouched { get; }
    public Task Save();
    public void Reset();
    public bool Validate();
    string GetLabel();
}
