using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenFadeFeature : ScriptableRendererFeature
{
    public ScreenFadeSettings settings;
    
    private ScreenFadePass renderPass;
    
    public override void Create()
    {
        renderPass = new ScreenFadePass(settings);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(renderPass);
    }
}
