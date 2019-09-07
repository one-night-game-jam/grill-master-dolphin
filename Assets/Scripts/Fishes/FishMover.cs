using System;
using System.Threading;
using UniRx.Async;
using UniRx.Async.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fishes
{
    public class FishMover : MonoBehaviour
    {
        void Start()
        {
            RandomMoveTask().Forget();
        }

        async UniTaskVoid RandomMoveTask()
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(Random.value * 3f), cancellationToken: cancellationToken);
                await LookAtRandom(cancellationToken);
                await UniTask.Delay(TimeSpan.FromSeconds(Random.value * 2f), cancellationToken: cancellationToken);
                await MoveForwardRandom(cancellationToken);
            }
        }

        async UniTask LookAtRandom(CancellationToken cancellationToken)
        {
            var targetRotation = Quaternion.LookRotation(Random.onUnitSphere);
            var startTime = Time.timeSinceLevelLoad;
            var duration = Random.Range(0.5f, 2f);
            while (true)
            {
                var lastTime = startTime + duration - Time.timeSinceLevelLoad;
                if (lastTime < 0)
                {
                    transform.rotation = targetRotation;
                    return;
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (duration - lastTime) / duration);
                await UniTask.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        async UniTask MoveForwardRandom(CancellationToken cancellationToken)
        {
            var targetPosition = transform.position + transform.forward * Random.Range(5f, 20f);
            var startTime = Time.timeSinceLevelLoad;
            var duration = Random.Range(5f, 10f);
            while (true)
            {
                var lastTime = startTime + duration - Time.timeSinceLevelLoad;
                if (lastTime < 0)
                {
                    transform.position = targetPosition;
                    return;
                }
                transform.position = Vector3.Slerp(transform.position, targetPosition, (duration - lastTime) / duration);
                await UniTask.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}
