using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public class ScreenFadeSettings
{
    public string profilerTag = "Screen Fade";
    public RenderPassEvent renderPassEvent = RenderPassEvent. AfterRenderingPostProcessing;
    public Material material = null;
    
    [HideInInspector]
    public Material runTimeMaterial;
}