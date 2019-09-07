using Dolphins;
using UnityEngine;

namespace Fishes
{
    public class Fugu : MonoBehaviour, IDolphinTouchable
    {
        [SerializeField]
        float happyTime;

        public void Touch(DolphinCore dolphinCore)
        {
            dolphinCore.MakeHappy(happyTime);
            Destroy(gameObject);
        }
    }
}
