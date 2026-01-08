namespace MaksimShimshon.GameManagePanel.Kernel.StateManagement;

public interface IState<out TState>
{
    TState State { get; }
}
