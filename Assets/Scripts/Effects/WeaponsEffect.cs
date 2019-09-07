using UnityEngine;
using UniRx;

public class WeaponsEffect : MonoBehaviour
{
    [SerializeField]
    Dolphins.DolphinCore dolphin;

    [SerializeField]
    ParticleSystem fire;

    [SerializeField]
    ParticleSystem smoke;

    void Awake()
    {
        Clear();
        End();

        dolphin.Fire.Subscribe(enablesEffect =>
        {
            if (enablesEffect)
            {
                Begin();
            }
            else
            {
                End();
            }
        })
        .AddTo(this);
    }

    public void Begin()
    {
        fire.Play();
        smoke.Play();
    }

    public void End()
    {
        fire.Stop();
        smoke.Stop();
    }

    public void Clear()
    {
        fire.Clear();
        smoke.Clear();
    }
}
