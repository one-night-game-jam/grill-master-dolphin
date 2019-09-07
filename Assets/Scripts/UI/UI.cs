using System;
using Dolphins;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField]
        DolphinCore dolphinCore;

        [SerializeField]
        Text time;

        [SerializeField]
        Text score;

        void Update()
        {
            time.text = TimeSpan.FromSeconds(dolphinCore.LastTime).ToString(@"mm\:ss\.ff");
            score.text = dolphinCore.Score.ToString();
        }
    }
}
