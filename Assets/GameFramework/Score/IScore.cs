using UniRx;

public interface IScore
{
    public ReactiveProperty<int> AddScore { get; }

    public void Reset();
}