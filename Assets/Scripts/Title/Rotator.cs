using UnityEngine;

namespace Title
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        Vector3 speed;

        void Update()
        {
            transform.Rotate(speed * Time.deltaTime);
        }
    }
}
