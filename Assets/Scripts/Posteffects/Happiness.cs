using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Happiness : MonoBehaviour
{
    [SerializeField, HideInInspector]
    Material aberration = null;

    [SerializeField]
    Dolphins.DolphinCore dolphin = null;

    float happiness = 0.0f;

    const float RefreshTime = 0.5f;

    void OnValidate()
    {
        aberration = new Material(Shader.Find("Posteffects/Happiness Aberration"));
    }

    void Awake()
    {
        dolphin.LateUpdateAsObservable()
            .Subscribe(_ => happiness = Mathf.SmoothStep(0.0f, RefreshTime, dolphin.HappyTime))
            .AddTo(this);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        aberration.SetFloat("_Happiness", happiness);
        Graphics.Blit(source, destination, aberration);
    }
}
