using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class CameraBlend : MonoBehaviour
{
    public Material primary;
    public Material secondary;
    public RenderTexture firefly;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (primary == null || secondary == null)
        {
            Graphics.Blit(source, destination);
            return;
        }
    
        Graphics.Blit(source, destination, primary);
        Graphics.Blit(firefly, destination, secondary);   
    }
}
