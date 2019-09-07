using Dolphins;
using UnityEngine;

namespace Fishes
{
    public class GrilledFishCore : MonoBehaviour, IDolphinTouchable
    {
        [SerializeField]
        long score;

        public void Touch(DolphinCore dolphinCore)
        {
            dolphinCore.AddScore(score);
            Destroy(gameObject);
        }
    }
}
