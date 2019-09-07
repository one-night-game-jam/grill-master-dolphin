using System;
using UnityEngine;
using UniRx;

[ExecuteInEditMode]
public class Fog : MonoBehaviour
{
    [SerializeField, HideInInspector]
    Material material = null;

    [SerializeField]
    float fogGamma = 2.0f;

    [SerializeField]
    float farDistance = 100.0f;

    [SerializeField]
    Color depthFogColor = Color.white;

    [SerializeField]
    Color heightFogColor = Color.white;

    void OnValidate()
    {
        material = new Material(Shader.Find("Posteffects/Fog"));
    }

    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_FogGamma", fogGamma);
        material.SetFloat("_FarDistance", farDistance);
        material.SetColor("_DepthFogColor", depthFogColor);
        material.SetColor("_HeightFogColor", heightFogColor);
        material.SetMatrix("_ViewToWorld", Camera.current.cameraToWorldMatrix);
        Graphics.Blit(source, destination, material);
    }
}
