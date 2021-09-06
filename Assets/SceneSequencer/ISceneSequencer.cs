using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneSequencer
{
    public void LoadScene(string sceneName);
    public void LoadSceneAsync(string sceneName);
}
