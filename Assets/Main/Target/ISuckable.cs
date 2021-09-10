using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface ISuckable
{
    ReactiveProperty<bool> Sucking { get; }
}
