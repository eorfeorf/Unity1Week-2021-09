using System;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "Data", menuName = "MainGame/TargetDataScriptableObject", order = 1)]
public class TargetDataScriptableObject : ScriptableObject
{
    public BaseTarget Prefab;
    public float SuckTime;
    public int Score;
    public float Height;
}
