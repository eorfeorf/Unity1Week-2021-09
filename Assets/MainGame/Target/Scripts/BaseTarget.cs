using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseTarget : MonoBehaviour, ISuckable
{
    public ReactiveProperty<bool> Sucking { get; } = new ReactiveProperty<bool>();
    public IReactiveProperty<BaseTarget> Destroyed { get; } = new ReactiveProperty<BaseTarget>();

    private MainGameManager mainGameManager;
    private float suckedTimer = 0f;
    private float suckedTime = 1f;
    private int score = 0;
    private bool isSucking;
    private TargetSuckAnimation targetSuckAnimation;
    
    private void Awake()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();
        targetSuckAnimation = gameObject.AddComponent<TargetSuckAnimation>();
    }

    public  void Init(float suckedTime, int score, float height)
    {
        this.suckedTime = suckedTime;
        this.score = score;

        // xyz軸の乱数を取って、ベクトルをその方向に向ける、Heightを掛ける.
        float x = Random.Range(0, 365);
        float y = Random.Range(0, 365);
        float z = Random.Range(0, 365);
        Quaternion q = quaternion.Euler(x, y, z);
        transform.rotation = q;
        transform.position = q * Vector3.up * height;
    }

    private void Start()
    {
        Sucking.SkipLatestValueOnSubscribe().Subscribe(sucked =>
        {
            isSucking = sucked;
        }).AddTo(this);
    }

    private void Update()
    {
        if (isSucking)
        {
            if (suckedTimer >= suckedTime)
            {
                Sucked();
                return;
            }
            suckedTimer += Time.deltaTime;
        }
        else
        {
            suckedTimer = 0f;
        }

        targetSuckAnimation.SuckRatio.Value = suckedTimer / suckedTime;
    }

    private void Sucked()
    {
        if(mainGameManager.Score != null)
        {
            mainGameManager.Score.AddScore.Value = score;
        }
        Destroyed.Value = this;
        Destroy(gameObject);
    }
}
