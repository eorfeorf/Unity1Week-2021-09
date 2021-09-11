using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private CollisionNotifier notifier;
    
    private void Awake()
    {
        notifier = GetComponentInChildren<CollisionNotifier>();
    }

    private void Start()
    {
        notifier.HitInfo.SkipLatestValueOnSubscribe().Subscribe(hitInfo =>
        {
            //Debug.Log($"Hit:{transform.name}");
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
}
