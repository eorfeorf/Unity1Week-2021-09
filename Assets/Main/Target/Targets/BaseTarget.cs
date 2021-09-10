using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class BaseTarget : MonoBehaviour, ISuckable
{
    [SerializeField] protected float suckedTime = 1f;
    [SerializeField] protected int score = 0;

    public ReactiveProperty<bool> Sucking => sucking;

    private readonly ReactiveProperty<bool> sucking = new ReactiveProperty<bool>();
    private float suckedTimer = 0f;
    private bool prevSucked = false;
    private bool nextSucked = false;
    private bool nowSucked = false;
    private MainGameManager mainGameManager;
    
    private void Awake()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();
    }

    private void Start()
    {
        sucking.SkipLatestValueOnSubscribe().Subscribe(sucked =>
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
