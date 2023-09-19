using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class InGame : MonoBehaviour, IScene
{
    public bool IsEnd { get; private set; }

    public async UniTask IsEndAsync()
    {
        await UniTask.WaitUntil(() => IsEnd);
        Debug.Log($"InGame end");
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            IsEnd = true;
        }
    }
}
