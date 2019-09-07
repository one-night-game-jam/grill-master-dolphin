using Dolphins;
using UniRx;
using UnityEngine;

namespace Fishes
{
    public class EnvironmentObserver : MonoBehaviour
    {
        [SerializeField]
        DolphinCore dolphinCore;

        public DolphinCore DolphinCore => dolphinCore;
    }
}
