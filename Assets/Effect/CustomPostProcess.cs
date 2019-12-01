using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPostProcess : MonoBehaviour
{
    public Material material;

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
