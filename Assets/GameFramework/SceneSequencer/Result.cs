using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Result : MonoBehaviour, IScene
{
    public bool IsEnd { get; private set; }

    public async UniTask IsEndAsync()
    {
        await UniTask.WaitUntil(() => IsEnd);
        Debug.Log($"Result End");
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            IsEnd = true;
        }
    }
}
