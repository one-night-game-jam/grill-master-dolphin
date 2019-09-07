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

        public float HappyTime { get; private set; }
        bool IsHappy => HappyTime > 0;

        long score;

        public IReadOnlyReactiveProperty<bool> Fire => input.Fire;
        public IReadOnlyReactiveProperty<bool> Sonar => input.Sonar;
        public IReadOnlyReactiveProperty<Vector2> Move => input.Move;
        public IReadOnlyReactiveProperty<Vector2> Aim => input.Aim;

        public float Speed => IsHappy ? happySpeed : defaultSpeed;
        public float RotateSpeed => IsHappy ? happyRotateSpeed : defaultRotateSpeed;

        public void MakeHappy(float time)
        {
            HappyTime = Mathf.Max(0, HappyTime) + time;
            Debug.Log($"Happy time {HappyTime}");
        }

        public void AddScore(long score)
        {
            this.score += score;
            Debug.Log($"Current score {this.score}");
        }

        void Update()
        {
            HappyTime -= Time.deltaTime;
        }

        void OnTriggerEnter(Collider other)
        {
            var touchable = other.GetComponent<IDolphinTouchable>();
            touchable?.Touch(this);
        }
    }
}
