using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Game.UI
{
    public sealed class CenterCard : MonoBehaviour
    {
        [Header("Center Card References")]
        [SerializeField] private Image objectIcon;
        [SerializeField] private TMP_Text countdownText;
        [SerializeField] private GameObject percentGroup;
        [SerializeField] private TMP_Text p1Percent;
        [SerializeField] private TMP_Text p2Percent;

        [Header("Animation Settings")]
        [SerializeField] private float countdownScale = 1.2f;
        [SerializeField] private float countdownDuration = 0.1f;

        private Coroutine _countdownAnimation;

        public void Initialize()
        {
            // Initialize center card components
            if (countdownText != null)
                countdownText.gameObject.SetActive(false);

            if (percentGroup != null)
                percentGroup.SetActive(false);

            if (objectIcon != null)
                objectIcon.gameObject.SetActive(false);
        }

        public void ShowCountdown(int seconds)
        {
            if (countdownText != null)
            {
                countdownText.gameObject.SetActive(true);
                countdownText.text = seconds.ToString();
                StartCountdownAnimation();
            }
        }

        public void UpdateCountdown(int value)
        {
            if (countdownText != null)
            {
                countdownText.text = value.ToString();
                StartCountdownAnimation();
            }
        }

        public void HideCountdown()
        {
            if (countdownText != null)
                countdownText.gameObject.SetActive(false);

            if (_countdownAnimation != null)
            {
                StopCoroutine(_countdownAnimation);
                _countdownAnimation = null;
            }
        }

        public void SetCenterIcon()
        {
            if (objectIcon != null)
            {
                objectIcon.gameObject.SetActive(true);
                // TODO: Set random object icon
                // objectIcon.sprite = GetRandomObjectIcon();
            }
        }

        public void ShowPercents(int p1, int p2)
        {
            if (percentGroup != null)
                percentGroup.SetActive(true);

            if (p1Percent != null)
                p1Percent.text = $"{p1}%";

            if (p2Percent != null)
                p2Percent.text = $"{p2}%";
        }

        public void HidePercents()
        {
            if (percentGroup != null)
                percentGroup.SetActive(false);
        }

        void StartCountdownAnimation()
        {
            if (_countdownAnimation != null)
                StopCoroutine(_countdownAnimation);

            _countdownAnimation = StartCoroutine(CountdownAnimation());
        }

        IEnumerator CountdownAnimation()
        {
            if (countdownText == null) yield break;

            Vector3 originalScale = countdownText.transform.localScale;
            Vector3 targetScale = originalScale * countdownScale;

            // Scale up
            float elapsed = 0f;
            while (elapsed < countdownDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / countdownDuration;
                countdownText.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
                yield return null;
            }

            // Scale down
            elapsed = 0f;
            while (elapsed < countdownDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / countdownDuration;
                countdownText.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
                yield return null;
            }

            countdownText.transform.localScale = originalScale;
        }
    }
}
