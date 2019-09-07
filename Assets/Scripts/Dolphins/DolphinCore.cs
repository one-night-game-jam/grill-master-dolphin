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

        public IReadOnlyReactiveProperty<bool> Fire => input.Fire;
        public IReadOnlyReactiveProperty<bool> Sonar => input.Sonar;
        public IReadOnlyReactiveProperty<Vector2> Move => input.Move;
        public IReadOnlyReactiveProperty<Vector2> Aim => input.Aim;

        public float Speed => defaultSpeed;
        public float RotateSpeed => defaultRotateSpeed;
    }
}
