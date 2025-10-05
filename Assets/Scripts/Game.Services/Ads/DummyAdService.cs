using System;
using System.Collections;
using UnityEngine;

// Namespace doğru bir şekilde Game.Services.Ads altında kalıyor.
namespace Game.Services.Ads
{
    /// <summary>
    /// Gerçek bir reklam servisi entegre edilene kadar test amaçlı kullanılan sahte reklam servisi.
    /// Reklamları, belirtilen süre kadar bekleyerek simüle eder.
    /// </summary>
    public sealed class DummyAdService : MonoBehaviour, IAds
    {
        [Header("Ayarlar")]
        [Tooltip("Geçiş reklamının saniye cinsinden süresi")]
        [SerializeField] private float _interstitialDuration = 1.5f;

        [Tooltip("Ödüllü reklamın saniye cinsinden süresi")]
        [SerializeField] private float _rewardedDuration = 2.5f;

        // Bir reklamın hazır olup olmadığını belirtir. Test için her zaman hazır.
        public bool Ready(string placement = "default") => true;

        /// <summary>
        /// Geçiş reklamı gösterir.
        /// </summary>
        public void ShowInterstitial(Action onClosed)
        {
            Debug.Log($"Sahte Geçiş Reklamı başlatılıyor ({_interstitialDuration} saniye sürecek)...");
            StartCoroutine(SimulateAdCoroutine(_interstitialDuration, onClosed));
        }

        /// <summary>
        /// Ödüllü reklam gösterir.
        /// </summary>
        public void ShowRewarded(Action onRewarded, Action onFailed = null)
        {
            Debug.Log($"Sahte Ödüllü Reklam başlatılıyor ({_rewardedDuration} saniye sürecek)...");
            // Bu sahte servis her zaman başarılı olacağı için onFailed callback'ini hiç kullanmıyoruz.
            StartCoroutine(SimulateAdCoroutine(_rewardedDuration, onRewarded));
        }

        /// <summary>
        /// Reklam beklemesini simüle eden ve bitince verilen eylemi çağıran yardımcı metot.
        /// </summary>
        private IEnumerator SimulateAdCoroutine(float duration, Action onFinished)
        {
            // TODO: Burada bir "Reklam Sürüyor..." paneli gösterilebilir.
            
            yield return new WaitForSeconds(duration);

            Debug.Log("Reklam bitti.");
            onFinished?.Invoke(); // Süre dolunca callback'i güvenli bir şekilde çağır.

            // TODO: "Reklam Sürüyor..." paneli burada gizlenebilir.
        }
    }
}