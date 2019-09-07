using System;
using Fishes;
using UniRx;
using UnityEngine;

namespace Dolphins
{
    public class DolphinCore : MonoBehaviour
    {
        [SerializeField]
        PlayerInputEventProvider input;

        [SerializeField]
        float defaultSpeed;
        [SerializeField]
        float defaultRotateSpeed;

        [SerializeField]
        float happySpeed;
        [SerializeField]
        float happyRotateSpeed;

        [SerializeField]
        float sonarCooldownTime;

        public float HappyTime { get; private set; }
        bool IsHappy => HappyTime > 0;

        public long Score { get; private set; }
        public const float PlayTime = 60;
        public float PastTime { get; private set; }
        public float LastTime => PlayTime - PastTime;
        public IObservable<bool> IsTimeUp => this.ObserveEveryValueChanged(x => PlayTime < x.PastTime);

        public IObservable<bool> Fire => input.Fire.CombineLatest(IsTimeUp, (fire, isTimeUp) => fire && !isTimeUp);
        public IObservable<Unit> Sonar => input.Sonar
            .Where(b => b)
            .ThrottleFirst(TimeSpan.FromSeconds(sonarCooldownTime))
            .AsUnitObservable();
        public IReadOnlyReactiveProperty<Vector2> Move => input.Move;
        public IReadOnlyReactiveProperty<Vector2> Aim => input.Aim;

        public float Speed => IsHappy ? happySpeed : defaultSpeed;
        public float RotateSpeed => IsHappy ? happyRotateSpeed : defaultRotateSpeed;

        public void MakeHappy(float time)
        {
            HappyTime = Mathf.Max(0, HappyTime) + time;
            PastTime -= 10f;
            Debug.Log($"Happy time {HappyTime}");
        }

        public void AddScore(long score)
        {
            Score += score;
            Debug.Log($"Current score {this.Score}");
        }

        void Update()
        {
            HappyTime -= Time.deltaTime;
            PastTime += Time.deltaTime;
        }

        void OnTriggerEnter(Collider other)
        {
            var touchable = other.GetComponent<IDolphinTouchable>();
            touchable?.Touch(this);
        }
    }
}
