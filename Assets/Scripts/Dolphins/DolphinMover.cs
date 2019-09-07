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
            rb.MovePosition(rb.position + deltaPosition);
        }

        float eulerX;
        float eulerY;
        void Aim(Vector2 delta)
        {
            delta *= core.RotateSpeed * Time.deltaTime;
            eulerX += delta.y;
            eulerY += delta.x;
            rb.MoveRotation(Quaternion.Euler(eulerX, eulerY, 0));
        }
    }
}
