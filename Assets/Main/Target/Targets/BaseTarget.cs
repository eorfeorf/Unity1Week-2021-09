using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class BaseTarget : MonoBehaviour, ISuckable
{
    public ReactiveProperty<bool> Sucking { get; } = new ReactiveProperty<bool>();

    private MainGameManager mainGameManager;
    private float suckedTimer = 0f;
    private bool prevSucked = false;
    private bool nextSucked = false;
    private bool nowSucked = false;
    private float suckedTime = 1f;
    private int score = 0;
    private float height = 0;
    
    private void Awake()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();
    }
    public void Init(float suckedTime, int score, float height)
    {
        this.suckedTime = suckedTime;
        this.score = score;
        this.height = height;
    }

    private void Start()
    {
        Sucking.SkipLatestValueOnSubscribe().Subscribe(sucked =>
        {
            if (suckedTimer >= suckedTime)
            {
                Sucked();
            }
                
            nowSucked = true;
            suckedTimer += Time.deltaTime;
        }).AddTo(this);
    }

    private void Update()
    {
        prevSucked = nextSucked;
        nextSucked = nowSucked;
        nowSucked = false;
        
        // 吸い込み判定が当たってる状態から外れたとき.
        if (prevSucked && !nextSucked)
        {
            suckedTimer = 0f;
        }
    }

    private void Sucked()
    {
        mainGameManager.Score.AddScore.Value = score;
        Destroy(gameObject);
    }
}
