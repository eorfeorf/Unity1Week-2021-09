using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Title : MonoBehaviour, IScene
{
    public bool IsEnd { get; private set; }

    public async UniTask IsEndAsync()
    {
        await UniTask.WaitUntil(() => IsEnd);
        Debug.Log($"Title end");
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            IsEnd = true;
        }
    }
}