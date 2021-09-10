using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    
    private PlayerMover mover;
    private int targetLayer;
    private RaycastHit[] hits = new RaycastHit[10];
    
    private void Awake()
    {
        mover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        RaycastTarget();
    }

    private void RaycastTarget()
    {
        Debug.DrawRay(transform.position, -transform.up * mover.Height, Color.red);
        if (Physics.RaycastNonAlloc(transform.position, -transform.up, hits, mover.Height, layerMask) <= 0) return;
        
        foreach (var hit in hits)
        {
            if (hit.collider == null)
            {
                continue;
            }
                
            Debug.Log("Hit");
            if (hit.transform.gameObject.GetComponentInParent<ISuckable>() is ISuckable suckable)
            {
                suckable.Sucking.SetValueAndForceNotify(true);
            }
            else
            {
                Debug.LogError("Can't get ISuckable.");
            }
        }
    }
}
