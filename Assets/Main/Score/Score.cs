using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
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
