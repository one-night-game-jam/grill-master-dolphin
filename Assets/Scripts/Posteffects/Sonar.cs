using System;
using UnityEngine;
using UniRx;

[ExecuteInEditMode]
public class Sonar : MonoBehaviour
{
    [SerializeField]
    Dolphins.DolphinCore dolphin = null;

    [SerializeField]
    float sonarWidth = 50.0f;

    [SerializeField]
    float sonarSpeed = 150.0f;

    [SerializeField]
    Color sonarColor = Color.white;

    [SerializeField]
    float sonarEffectiveRadius = 300.0f;

    float sonarStartTime = 0;
    Vector3 sonarCenter = Vector3.zero;

    Material material = null;

    public void BeginSonar(Vector3 center)
    {
        sonarStartTime = Time.time;
        sonarCenter = center;
    }

    void Awake()
    {
        sonarStartTime = float.MinValue;

        dolphin.Sonar.Where(x => x).Subscribe(_ => BeginSonar(dolphin.transform.position)).AddTo(this);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            material = new Material(Shader.Find("Posteffects/Sonar"));
        }

        float elapsedTime = Time.time - sonarStartTime;
        float sonarRadius = elapsedTime * sonarSpeed;
        material.SetFloat("_SonarRadius", sonarRadius);
        material.SetVector("_SonarCenter", sonarCenter);

        material.SetFloat("_SonarWidth", sonarWidth);
        material.SetColor("_SonarColor", sonarColor);
        material.SetFloat("_SonarEffectiveRadius", sonarEffectiveRadius);
        material.SetMatrix("_ViewToWorld", Camera.current.cameraToWorldMatrix);
        Graphics.Blit(source, destination, material);
    }
}
