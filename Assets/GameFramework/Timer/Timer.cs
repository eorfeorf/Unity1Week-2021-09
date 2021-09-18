using System;
using System.Globalization;
using TMPro;
using UniRx;
using UnityEngine;

public sealed class Timer : MonoBehaviour, ITimer
{
    enum Type
    {
        Minutes,
        Seconds,
    }

    [SerializeField] private Type type = Type.Minutes;
    [SerializeField] private float time = 60f;

    public IReactiveProperty<bool> IsEnd { get; } = new ReactiveProperty<bool>(false);

    private ReactiveProperty<float> timer = new ReactiveProperty<float>();
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        timer.SkipLatestValueOnSubscribe().Subscribe(timer =>
        {
            var str = Reshape(type, timer);
            tmp.text = timer.ToString(CultureInfo.InvariantCulture);
        }).AddTo(this);
    }

    private void Update()
    {
        if (timer.Value >= time)
        {
            IsEnd.Value = true;
        }

        var tempTime = timer.Value += Time.deltaTime;
        timer.Value = Mathf.Clamp(tempTime, 0f, time);
    }

    public void Reset()
    {
        timer.Value = 0f;
    }

    private string Reshape(Type type, float timer)
    {
        switch (type)
        {
            case Type.Minutes:
                return ReshapeMinutes(timer);
            case Type.Seconds:
                return ReshapeSeconds(timer);
            default:
                return "xxxxxx";
        }
    }

    private string ReshapeMinutes(float timer)
    {
        var m = timer / 60;
        var s = timer % 60;
        return $"{m.ToString("00")}:{s.ToString("00")}";
    }

    private string ReshapeSeconds(float timer)
    {
        return $"{timer.ToString("00.00")}";
    }
}