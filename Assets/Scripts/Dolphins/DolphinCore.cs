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

        float happyTime;
        bool IsHappy => happyTime > 0;

        long score;

        public IReadOnlyReactiveProperty<bool> Fire => input.Fire;
        public IReadOnlyReactiveProperty<bool> Sonar => input.Sonar;
        public IReadOnlyReactiveProperty<Vector2> Move => input.Move;
        public IReadOnlyReactiveProperty<Vector2> Aim => input.Aim;

        public float Speed => IsHappy ? happySpeed : defaultSpeed;
        public float RotateSpeed => IsHappy ? happyRotateSpeed : defaultRotateSpeed;

        public void MakeHappy(float time)
        {
            happyTime = Mathf.Max(0, happyTime) + time;
            Debug.Log($"Happy time {happyTime}");
        }

        public void AddScore(long score)
        {
            this.score += score;
            Debug.Log($"Current score {this.score}");
        }

        void Update()
        {
            happyTime -= Time.deltaTime;
        }

        void OnTriggerEnter(Collider other)
        {
            var touchable = other.GetComponent<IDolphinTouchable>();
            touchable?.Touch(this);
        }
    }
}
