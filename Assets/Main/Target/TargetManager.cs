using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class TargetManager : MonoBehaviour
{
    [SerializeField] private TargetData[] targetData;

    private int targetNums = 0;

    public void Init(int targetNums)
    {
        this.targetNums = targetNums;
    }

    private void InitTargets()
    {
        // エネミーの生成
        for(var i = 0; i < targetNums; ++i)
        {
            var kind = GetMakeTargetKind();
            var data = GetTargetPrefab(kind);
            var t = Instantiate(data.Prefab) as BaseTarget;
            t.Init(data.SuckTime, data.Score, data.Height);
        }
    }

    private static TargetData.eKind GetMakeTargetKind()
    {
        // TODO:生成アルゴリズムを考える.
        return TargetData.eKind.Human;
    }

    private TargetData GetTargetPrefab(TargetData.eKind kind)
    {
        return targetData[(int)kind];
    }
}
