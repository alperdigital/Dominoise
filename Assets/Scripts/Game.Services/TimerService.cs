using System;
using System.Collections;
using UnityEngine;

namespace Game.Services
{
    public sealed class TimerService : MonoBehaviour, ITimerService
    {
        public void Delay(float duration, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(duration, onFinished));
        }

        private IEnumerator DelayCoroutine(float duration, Action onFinished)
        {
            yield return new WaitForSeconds(duration);
            onFinished?.Invoke();
        }
    }
}
