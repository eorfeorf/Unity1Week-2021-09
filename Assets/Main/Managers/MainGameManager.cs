using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public enum eKind
    {
        Human, Tree, House, Car, Max
    }

    [Serializable]
    public class TargetDataSet
    {
        public eKind kind;
        public TargetDataScriptableObject data;
    }
    
    [SerializeField] private Score scorePrefab;
    [SerializeField] private TargetManager targetManagerPrefab;
    [SerializeField] private int targetNums;
    [SerializeField] private TargetDataSet[] targetData = new TargetDataSet[(int)eKind.Max];

    public Score Score { get; private set; }

    private void Awake()
    {
        // スコア.
        Score = Instantiate(scorePrefab, transform);
        
        // 敵.
        var TargetManager = Instantiate(targetManagerPrefab, transform);
        TargetManager.Init(targetNums, targetData);
    }
}
