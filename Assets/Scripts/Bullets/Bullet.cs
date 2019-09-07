using Fishes;
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
                .Subscribe(_ => transform.localScale += Vector3.one * scaleRatio * Time.deltaTime)
                .AddTo(this);

            Destroy(gameObject, LifetimeSeconds);
        }

        void OnTriggerEnter(Collider other)
        {
            var fish = other.GetComponent<FishCore>();
            if (fish != null) fish.Burn();
        }
    }
}
