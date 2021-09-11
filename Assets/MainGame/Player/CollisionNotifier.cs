using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class HitInfo
{
    public bool Hitting;
    public Collision Collision;
}

public class CollisionNotifier : MonoBehaviour
{
    [SerializeField] private LayerMask Targetlayer;
    
    public IReactiveProperty<HitInfo> HitInfo => hitInfo;
    private readonly ReactiveProperty<HitInfo> hitInfo = new ReactiveProperty<HitInfo>();

    private void OnCollisionEnter(Collision other)
    {
        Notification(other, true);
    }

    private void OnCollisionExit(Collision other)
    {
        Notification(other, false);
    }

    private void Notification(Collision other, bool hitFlag)
    {
        int bit = Targetlayer.value >> other.collider.gameObject.layer; 
        if (bit != 1)
        {
            return;
        }

        hitInfo.SetValueAndForceNotify(new HitInfo()
        {
            Hitting = hitFlag,
            Collision = other
        });
    }
}
