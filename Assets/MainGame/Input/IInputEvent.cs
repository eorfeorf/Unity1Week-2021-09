using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IInputEvent
{
    ReactiveProperty<bool> sucked { get; }
}
