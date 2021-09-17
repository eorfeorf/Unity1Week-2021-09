using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseTarget : MonoBehaviour, ISuckable
{
    public ReactiveProperty<bool> Sucking { get; } = new ReactiveProperty<bool>();
    public IReactiveProperty<BaseTarget> Destroyed { get; } = new ReactiveProperty<BaseTarget>(null);

    private MainGameManager mainGameManager;
    private float suckedTimer = 0f;
    private float suckedTime = 1f;
    private int score = 0;
    private bool isSucking;
    private TargetSuckAnimation targetSuckAnimation;
    private TargetEffect targetEffect;
    private CancellationToken cancellationToken;

    private void Awake()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();
        targetSuckAnimation = gameObject.AddComponent<TargetSuckAnimation>();
        targetEffect = gameObject.GetComponentInChildren<TargetEffect>();
        cancellationToken = this.GetCancellationTokenOnDestroy();
    }

    public void Init(float suckedTime, int score, float height)
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
        Sucking.SkipLatestValueOnSubscribe().Subscribe(sucked => { isSucking = sucked; }).AddTo(this);
    }

    private void Update()
    {
        // 破棄された.
        if (Destroyed.Value)
        {
            return;
        }

        // 吸い込み中.
        if (isSucking)
        {
            if (suckedTimer >= suckedTime)
            {
                // 吸い込み終わった.
                Sucked(cancellationToken);
            }
            else
            {
                // まだ吸い込んでる.
                suckedTimer += Time.deltaTime;
                Mathf.Clamp(suckedTimer, 0f, suckedTime);
            }
        }
        else
        {
            suckedTimer = 0f;
        }

        targetSuckAnimation.SuckRatio.Value = suckedTimer / suckedTime;
    }

    private async UniTaskVoid Sucked(CancellationToken ct)
    {
        // 破棄通知.
        Destroyed.Value = this;

        // スコア加算.
        if (mainGameManager.Score != null)
        {
            mainGameManager.Score.AddScore.Value = score;
        }

        // エフェクト.
        await targetEffect.PlayDestroyEffect(ct);

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}