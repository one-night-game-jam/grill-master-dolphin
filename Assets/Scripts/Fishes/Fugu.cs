using Dolphins;
using UnityEngine;

namespace Fishes
{
    public class Fugu : MonoBehaviour
    {
        [SerializeField]
        float happyTime;

        void OnTriggerEnter(Collider other)
        {
            var dolphinCore = other.GetComponent<DolphinCore>();
            if (dolphinCore != null) dolphinCore.MakeHappy(happyTime);
        }
    }
}
