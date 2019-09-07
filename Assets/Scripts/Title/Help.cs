using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class Help : MonoBehaviour
    {
        [SerializeField]
        Button openButton;

        [SerializeField]
        Button closeButton;

        [SerializeField]
        GameObject container;

        void Start()
        {
            openButton.OnClickAsObservable()
                .Select(_ => true)
                .Merge(closeButton.OnClickAsObservable().Select(_ => false))
                .Subscribe(b => container.SetActive(b))
                .AddTo(this);
        }
    }
}
