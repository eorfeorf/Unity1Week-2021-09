using UniRx;

public interface IInputEventProvider
{
    IReadOnlyReactiveProperty<bool> MainButton { get; }
    IReadOnlyReactiveProperty<bool> SubButton { get; }
    IReadOnlyReactiveProperty<float> Vertical { get; }
    IReadOnlyReactiveProperty<float> Horizontal { get; }
}