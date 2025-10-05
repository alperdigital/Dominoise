using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Core;
using Game.Services;
using Game.Services.Ads;
using Game.Services.Economy;
using Game.SharedSo;

namespace Game.UI
{
    public sealed class PopupManager : MonoBehaviour
    {
        [Header("Popup References")]
        [SerializeField] private GameObject insufficientGoldPanel;
        [SerializeField] private Button watchAdBtn;
        [SerializeField] private TMP_Text goldAmountText;
        [SerializeField] private Button closeBtn;

        private IEventBus _eventBus;
        private IEconomy _economy;
        private IAds _ads;

        void Awake()
        {
            _eventBus = Service.Get<IEventBus>();
            _economy = Service.Get<IEconomy>();
            _ads = Service.Get<IAds>();

            InitializePopup();
            SubscribeToEvents();
        }

        void InitializePopup()
        {
            if (watchAdBtn != null)
                watchAdBtn.onClick.AddListener(OnWatchAdClicked);

            if (closeBtn != null)
                closeBtn.onClick.AddListener(OnCloseClicked);

            UpdateGoldDisplay();
        }

        void SubscribeToEvents()
        {
            _eventBus.Subscribe<UiEvents.ShowInsufficientGold>(OnShowInsufficientGold);
            _eventBus.Subscribe<UiEvents.HideInsufficientGold>(OnHideInsufficientGold);
        }

        void OnShowInsufficientGold(UiEvents.ShowInsufficientGold e)
        {
            if (insufficientGoldPanel != null)
            {
                insufficientGoldPanel.SetActive(true);
                UpdateGoldDisplay();
            }
        }

        void OnHideInsufficientGold(UiEvents.HideInsufficientGold e)
        {
            if (insufficientGoldPanel != null)
                insufficientGoldPanel.SetActive(false);
        }

        void OnWatchAdClicked()
        {
            if (_ads != null)
            {
                _ads.ShowRewarded(OnAdRewarded, OnAdFailed);
            }
        }

        void OnAdRewarded()
        {
            if (_economy != null)
            {
                var cfg = Service.Get<EconomyConfigSo>();
                _economy.Grant(cfg.adReward);
                UpdateGoldDisplay();
            }

            if (insufficientGoldPanel != null)
                insufficientGoldPanel.SetActive(false);
        }

        void OnAdFailed()
        {
            UnityEngine.Debug.Log("Ad failed to show or was not rewarded");
        }

        void OnCloseClicked()
        {
            if (insufficientGoldPanel != null)
                insufficientGoldPanel.SetActive(false);
        }

        void UpdateGoldDisplay()
        {
            if (goldAmountText != null && _economy != null)
            {
                goldAmountText.text = $"{_economy.Balance}🪙";
            }
        }

        void OnDestroy()
        {
            if (_eventBus != null)
            {
                _eventBus.Unsubscribe<UiEvents.ShowInsufficientGold>(OnShowInsufficientGold);
                _eventBus.Unsubscribe<UiEvents.HideInsufficientGold>(OnHideInsufficientGold);
            }
        }
    }
}