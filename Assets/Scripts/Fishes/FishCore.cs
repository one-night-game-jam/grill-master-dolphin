using Dolphins;
using UnityEngine;

namespace Fishes
{
    public class FishCore : MonoBehaviour
    {
        [SerializeField]
        GrilledFishCore grilledPrefab;

        DolphinCore dolphinCore;

        public void Inject(DolphinCore dolphinCore)
        {
            this.dolphinCore = dolphinCore;
        }

        public void Burn()
        {
            Instantiate(grilledPrefab, transform.position, transform.rotation, transform.parent).Inject(dolphinCore);
            Destroy(gameObject);
        }
    }
}
