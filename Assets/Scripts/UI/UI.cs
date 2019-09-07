using System;
using Dolphins;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField]
        DolphinCore dolphinCore;

        [SerializeField]
        GameObject playingUIRoot;
        [SerializeField]
        GameObject resultUIRoot;

        [SerializeField]
        Text time;
        [SerializeField]
        Text[] scores;

        void Start()
        {
            dolphinCore.IsTimeUp
                .Select(b => !b)
                .Subscribe(SwitchUI)
                .AddTo(this);
        }

        void SwitchUI(bool playing)
        {
            playingUIRoot.SetActive(playing);
            resultUIRoot.SetActive(!playing);
        }

        void Update()
        {
            time.text = TimeSpan.FromSeconds(dolphinCore.LastTime).ToString(@"mm\:ss\.ff");
            foreach (var score in scores)
            {
                score.text = dolphinCore.Score.ToString();
            }
        }
    }
}
