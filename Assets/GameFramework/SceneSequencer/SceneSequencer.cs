using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SceneSequencer : MonoBehaviour
{
    [SerializeField] private Title titleScenePrefab;
    [SerializeField] private InGame inGameScenePrefab;
    [SerializeField] private Result resultScenePrefab;

    private async void Start()
    {
        for (;;)
        {
            // タイトル.
            var title = Instantiate(titleScenePrefab, transform);
            await title.IsEndAsync();
            title.Close();

            // インゲーム.
            var inGame = Instantiate(inGameScenePrefab, transform);
            await inGame.IsEndAsync();
            inGame.Close();

            // リザルト.
            var result = Instantiate(resultScenePrefab, transform);
            await result.IsEndAsync();
            result.Close();
        }
    }
}