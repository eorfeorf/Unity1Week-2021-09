using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IScene
{
    bool IsEnd { get; }
    void Close();
}
