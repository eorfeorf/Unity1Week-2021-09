using UniRx;

interface ITimer
{
    public IReactiveProperty<bool> IsEnd { get; }

    public void Reset();
}