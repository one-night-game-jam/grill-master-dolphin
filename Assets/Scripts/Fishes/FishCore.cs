using UnityEngine;

namespace Fishes
{
    public class FishCore : MonoBehaviour
    {
        [SerializeField]
        GameObject grilledPrefab;

        public void Burn()
        {
            Instantiate(grilledPrefab, transform.position, transform.rotation, transform.parent);
            Destroy(gameObject);
        }
    }
}
