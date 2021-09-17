using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public sealed class TargetManager : MonoBehaviour
{
    public IReactiveProperty<bool> AllDestroyed { get; } = new ReactiveProperty<bool>();

    private int targetNums = 0;
    private TargetDataSet[] targetDataSet;
    private List<BaseTarget> targetList;
    
    public TargetManager Init(int targetNums, TargetDataSet[] targetData)
    {
        this.targetNums = targetNums;
        this.targetDataSet = targetData;
        targetList = new List<BaseTarget>(targetNums);
        MakeTargets(targetList);

        return this;
    }

    private void MakeTargets(List<BaseTarget> targetList)
    {
        // エネミーの生成
        for(var i = 0; i < targetNums; ++i)
        {
            var kind = GetMakeTargetKind();
            var data = GetTargetPrefab(kind);
            var t = Instantiate(data.Prefab);
            
            // 初期化
            t.Init(data.SuckTime, data.Score, data.Height);
            
            // 破棄時
            t.Destroyed.SkipLatestValueOnSubscribe().Subscribe(target =>
            {
                targetList.Remove(target);

                if (targetList.Count == 0)
                {
                    AllDestroyed.Value = true;
                }
                
            }).AddTo(this);
            
            targetList.Add(t);
        }
    }

    private static TargetKind GetMakeTargetKind()
    {
        // TODO:生成アルゴリズムを考える.
        return (TargetKind)Random.Range(0, 4);
    }

    private TargetDataScriptableObject GetTargetPrefab(TargetKind targetKind)
    {
        return targetDataSet[(int)targetKind].data;
    }
}
