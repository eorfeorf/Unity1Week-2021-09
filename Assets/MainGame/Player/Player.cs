using System;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    public IReactiveProperty<Vector3> Position { get; } = new ReactiveProperty<Vector3>();

    private CollisionNotifier notifier;
    
    private void Awake()
    {
        notifier = GetComponentInChildren<CollisionNotifier>();
    }

    private void Start()
    {
        notifier.HitInfo.SkipLatestValueOnSubscribe().Subscribe(hitInfo =>
        {
            if (hitInfo.Collision.transform.gameObject.GetComponentInParent<ISuckable>() is { } suckable)
            {
                suckable.Sucking.SetValueAndForceNotify(hitInfo.Hitting);
            }
            else
            {
                Debug.LogError("Could not get ISuckable.");
            }
        }).AddTo(this);
    }

    private void Update()
    {
        Position.Value = transform.position;
    }
}
