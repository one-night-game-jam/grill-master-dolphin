using UniRx.Async;
using UniRx.Async.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        Button button;

        [SerializeField]
        string sceneName;

        void Start()
        {
            ClickAndLoadSceneAsync().Forget();
        }

        async UniTaskVoid ClickAndLoadSceneAsync()
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            await button.OnClickAsync(cancellationToken);
            await SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
