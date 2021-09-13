using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(BaseTarget))]
public class TargetSuckAnimation : MonoBehaviour
{
    public IReactiveProperty<float> SuckRatio { get; } = new ReactiveProperty<float>();

    private List<Material> materials = new List<Material>();
    
    private void Start()
    {
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var mr in meshRenderers)
        {
            if (mr.material == null)
            {
                continue;
            }
            materials.Add(mr.material);
        }

        SuckRatio.SkipLatestValueOnSubscribe().Subscribe(ratio =>
        {
            foreach(var mat in materials)
            {
                mat.SetFloat("_SuckRatio", ratio);
            }
        }).AddTo(this);
    }
}
