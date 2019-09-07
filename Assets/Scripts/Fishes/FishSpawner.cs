using UnityEngine;

namespace Fishes
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField]
        FishCore fishPrefab;
        [SerializeField]
        int initialFishCount;

        [SerializeField]
        Fugu fuguPrefab;
        [SerializeField]
        int initialFuguCount;

        [SerializeField]
        float spawnAreaRadius;

        [SerializeField]
        EnvironmentObserver environmentObserver;

        void Start()
        {
            for (var i = 0; i < initialFishCount; i++)
            {
                Instantiate(fishPrefab, Random.insideUnitSphere * spawnAreaRadius, Random.rotation, transform).Inject(environmentObserver.DolphinCore);
            }

            for (var i = 0; i < initialFuguCount; i++)
            {
                Instantiate(fuguPrefab, Random.insideUnitSphere * spawnAreaRadius, Random.rotation, transform);
            }
        }
    }
}
