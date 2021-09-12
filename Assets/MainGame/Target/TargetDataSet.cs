using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public enum TargetKind
{
    Human, Tree, House, Car, Max
}

[Serializable]
public class TargetDataSet
{
    public TargetKind kind;
    public TargetDataScriptableObject data;
}
