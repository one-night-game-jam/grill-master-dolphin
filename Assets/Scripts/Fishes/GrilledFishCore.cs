using Dolphins;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Fishes
{
    public class GrilledFishCore : MonoBehaviour, IDolphinTouchable
    {
        [SerializeField]
        long score;

        [SerializeField]
        float speed;

        public void Inject(DolphinCore dolphinCore)
        {
            this.UpdateAsObservable()
                .Subscribe(_ => transform.position = Vector3.Lerp(transform.position, dolphinCore.transform.position, Time.deltaTime * speed))
                .AddTo(this);
        }

        public void Touch(DolphinCore dolphinCore)
        {
            dolphinCore.AddScore(score);
            Destroy(gameObject);
        }
    }
}
