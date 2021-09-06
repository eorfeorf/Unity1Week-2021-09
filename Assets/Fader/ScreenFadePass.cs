using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenFadePass : ScriptableRenderPass
{
    private ScreenFadeSettings settings = null;

    public ScreenFadePass(ScreenFadeSettings newSettings)
    {
        settings = newSettings;
        renderPassEvent = newSettings.renderPassEvent;
    }
    
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer command = CommandBufferPool.Get(settings.profilerTag);

        RenderTargetIdentifier source = BuiltinRenderTextureType.CameraTarget;
        RenderTargetIdentifier dest = BuiltinRenderTextureType.CurrentActive;
        
        command.Blit(source, dest, settings.runTimeMaterial);
        context.ExecuteCommandBuffer(command);
        
        CommandBufferPool.Release(command);
    }
}