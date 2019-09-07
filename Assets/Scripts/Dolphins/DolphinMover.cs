using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Dolphins
{
    public class DolphinMover : MonoBehaviour
    {
        [SerializeField]
        DolphinCore core;
        [SerializeField]
        Rigidbody rb;
        [SerializeField]
        Transform modelRoot;

        void Start()
        {
            this.FixedUpdateAsObservable()
                .WithLatestFrom(core.Move, (_, move) => move)
                .Subscribe(Move)
                .AddTo(this);
            this.FixedUpdateAsObservable()
                .WithLatestFrom(core.Aim, (_, aim) => aim)
                .Subscribe(Aim)
                .AddTo(this);
        }

        void Move(Vector2 delta)
        {
            delta *= core.Speed * Time.deltaTime;
            var deltaPosition = delta.y * rb.transform.forward + delta.x * rb.transform.right;
            if (deltaPosition.sqrMagnitude > float.Epsilon)
                modelRoot.LookAt(modelRoot.position + deltaPosition);
            rb.MovePosition(rb.position + deltaPosition);
        }

        void Aim(Vector2 delta)
        {
            delta *= core.RotateSpeed * Time.deltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(delta.y, delta.x, 0));
        }
    }
}
