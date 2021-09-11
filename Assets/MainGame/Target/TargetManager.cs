using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public sealed class TargetManager : MonoBehaviour
{
    private int targetNums = 0;
    private MainGameManager.TargetDataSet[] targetDataSet;
    
    public void Init(int targetNums, MainGameManager.TargetDataSet[] targetData)
    {
        this.targetNums = targetNums;
        this.targetDataSet = targetData;
        InitTargets();
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

    private static MainGameManager.eKind GetMakeTargetKind()
    {
        // TODO:生成アルゴリズムを考える.
        return (MainGameManager.eKind)Random.Range(0, 4);
    }

    private TargetDataScriptableObject GetTargetPrefab(MainGameManager.eKind kind)
    {
        return targetDataSet[(int)kind].data;
    }
}
