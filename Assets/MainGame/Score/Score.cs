using TMPro;
using UniRx;
using UnityEngine;

public sealed class Score : MonoBehaviour, IScore
{
    public ReactiveProperty<int> AddScore { get; } = new ReactiveProperty<int>();

    private int total = 0;
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        AddScore.SkipLatestValueOnSubscribe().Subscribe(score =>
        {
            total += score;
            tmp.SetText(total.ToString());
        }).AddTo(this);
    }

    public void Reset()
    {
        total = 0;
    }
}
