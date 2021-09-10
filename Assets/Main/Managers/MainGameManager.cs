using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private int targetNums;

    public Score Score => score;

    private Score score;
    public TargetManager TargetManager { get; private set; }

    private void Awake()
    {
        // スコア.
        score = GameObject.FindObjectOfType<Score>();
        
        // 敵.
        TargetManager = gameObject.AddComponent<TargetManager>();
        TargetManager.Init(targetNums);
    }
}
