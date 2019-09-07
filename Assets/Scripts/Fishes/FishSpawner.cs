using UnityEngine;

namespace Fishes
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject fishPrefab;
        [SerializeField]
        int initialFishCount;

        [SerializeField]
        float spawnAreaRadius;

        void Start()
        {
            for (var i = 0; i < initialFishCount; i++)
            {
                Instantiate(fishPrefab, Random.insideUnitSphere * spawnAreaRadius, Random.rotation, transform);
            }
        }
    }
}
