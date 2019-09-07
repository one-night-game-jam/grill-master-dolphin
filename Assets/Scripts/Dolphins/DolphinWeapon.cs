using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Dolphins
{
    public class DolphinWeapon : MonoBehaviour
    {
        [SerializeField]
        DolphinCore core;

        [SerializeField]
        float throttleDueTimeSeconds;

        [SerializeField]
        GameObject bulletPrefab;

        void Start()
        {
            this.UpdateAsObservable()
                .WithLatestFrom(core.Fire, (_, b) => b)
                .Where(b => b)
                .ThrottleFirst(TimeSpan.FromSeconds(throttleDueTimeSeconds))
                .Subscribe(_ => Fire())
                .AddTo(this);
        }

        void Fire()
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}
