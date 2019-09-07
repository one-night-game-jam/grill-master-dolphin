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

        public IReadOnlyReactiveProperty<bool> Fire => input.Fire;
        public IReadOnlyReactiveProperty<bool> Sonar => input.Sonar;
        public IReadOnlyReactiveProperty<Vector2> Move => input.Move;
        public IReadOnlyReactiveProperty<Vector2> Aim => input.Aim;

        public float Speed => IsHappy ? happySpeed : defaultSpeed;
        public float RotateSpeed => IsHappy ? happyRotateSpeed : defaultRotateSpeed;

        public void MakeHappy(float time)
        {
            happyTime = Mathf.Max(0, happyTime) + time;
        }

        void Update()
        {
            happyTime -= Time.deltaTime;
        }
    }
}
