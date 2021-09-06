using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSequencerBoot : SceneSequencerBase 
{
    private void Start()
    {
        LoadScene(this.nextScene);
    }
}
