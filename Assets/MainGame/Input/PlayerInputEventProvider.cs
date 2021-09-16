using UniRx;
using UnityEngine;

public sealed class PlayerInputEventProvider : MonoBehaviour, IInputEventProvider
{
    public IReadOnlyReactiveProperty<bool> MainButton { get; } = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> SubButton { get; } = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<float> Vertical { get; } = new ReactiveProperty<float>();
    public IReadOnlyReactiveProperty<float> Horizontal { get; } = new ReactiveProperty<float>();

    private void Update()
    {
    }
}