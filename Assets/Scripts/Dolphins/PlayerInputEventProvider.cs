using UniRx;
using UnityEngine;

namespace Dolphins
{
    public class PlayerInputEventProvider : MonoBehaviour
    {
        readonly ReactiveProperty<bool> fire = new ReactiveProperty<bool>();
        readonly ReactiveProperty<bool> sonar = new ReactiveProperty<bool>();
        readonly ReactiveProperty<Vector2> move = new ReactiveProperty<Vector2>();
        readonly ReactiveProperty<Vector2> aim = new ReactiveProperty<Vector2>();

        public IReadOnlyReactiveProperty<bool> Fire => fire;
        public IReadOnlyReactiveProperty<bool> Sonar => sonar;
        public IReadOnlyReactiveProperty<Vector2> Move => move;
        public IReadOnlyReactiveProperty<Vector2> Aim => aim;

        void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            this.ObserveEveryValueChanged(_ => Input.GetButton("Fire1"))
                .Subscribe(x => fire.Value = x)
                .AddTo(this);
            this.ObserveEveryValueChanged(_ => Input.GetButton("Fire2"))
                .Subscribe(x => sonar.Value = x)
                .AddTo(this);
            this.ObserveEveryValueChanged(_ => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")))
                .Subscribe(x => move.Value = x)
                .AddTo(this);
            this.ObserveEveryValueChanged(_ => new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")))
                .Subscribe(x => aim.Value = x)
                .AddTo(this);
        }
    }
}
