using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private Score scorePrefab;
    [SerializeField] private TargetManager targetManagerPrefab;
    [SerializeField] private int targetNums;
    [SerializeField] private TargetDataSet[] targetData = new TargetDataSet[(int)TargetKind.Max];

    public Score Score { get; private set; }

    private TargetManager targetManager;
    private ISceneSequencer sequencer;

    private void Awake()
    {
        // シーケンサ.
        sequencer = GetComponent<SceneSequencerBase>();
        
        // スコア.
        //Score = Instantiate(scorePrefab, transform);
        
        // 敵.
        targetManager = Instantiate(targetManagerPrefab, transform).Init(targetNums, targetData);
        targetManager.AllDestroyed.SkipLatestValueOnSubscribe().Subscribe(_ =>
        {
            Reset();
        }).AddTo(this);
    }

    private void Reset()
    {
        sequencer.LoadScene("Main");
    }
}
