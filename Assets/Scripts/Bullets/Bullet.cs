using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        Rigidbody rb;
        [SerializeField]
        Transform collisionTransform;

        [SerializeField]
        float speed;
        [SerializeField]
        float scaleRatio;

        [SerializeField]
        float LifetimeSeconds;

        void Start()
        {
            this.FixedUpdateAsObservable()
                .Subscribe(_ => rb.MovePosition(rb.transform.position += rb.transform.forward * speed * Time.deltaTime))
                .AddTo(this);
            this.UpdateAsObservable()
                .Subscribe(_ => collisionTransform.localScale += Vector3.one * scaleRatio * Time.deltaTime)
                .AddTo(this);

            Destroy(gameObject, LifetimeSeconds);
        }
    }
}
