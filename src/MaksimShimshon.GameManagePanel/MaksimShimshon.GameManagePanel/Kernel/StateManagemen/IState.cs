namespace MaksimShimshon.GameManagePanel.Kernel.StateManagemen;

public interface IState<out TState>
{
    TState State { get; }
}
