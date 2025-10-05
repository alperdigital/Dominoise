using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Core;
using Game.Services;

namespace Game.UI
{
    public sealed class UIManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private TopBar topBar;
        [SerializeField] private ModeRow modeRow;
        [SerializeField] private PlayerArea playerArea;
        [SerializeField] private CenterCard centerCard;
        [SerializeField] private BottomBar bottomBar;
        [SerializeField] private Audio audioManager;

        [Header("Popup References")]
        [SerializeField] private GameObject insufficientGoldPanel;

        private GameFlow _gameFlow;
        private IEventBus _eventBus;

        void Awake()
        {
            _gameFlow = FindFirstObjectByType<GameFlow>();
            _eventBus = Service.Get<IEventBus>();
            
            InitializeUI();
            SubscribeToEvents();
        }

        void InitializeUI()
        {
            // Initialize all UI components
            if (topBar != null) topBar.Initialize();
            if (modeRow != null) modeRow.Initialize(_gameFlow);
            if (playerArea != null) playerArea.Initialize();
            if (centerCard != null) centerCard.Initialize();
            if (bottomBar != null) bottomBar.Initialize(_gameFlow);
            if (audioManager != null) audioManager.Initialize();
        }

        void SubscribeToEvents()
        {
            _eventBus.Subscribe<UiEvents.CountdownShow>(OnCountdownShow);
            _eventBus.Subscribe<UiEvents.CountdownTick>(OnCountdownTick);
            _eventBus.Subscribe<UiEvents.CountdownHide>(OnCountdownHide);
            _eventBus.Subscribe<UiEvents.UpdateScoreboard>(OnUpdateScoreboard);
            _eventBus.Subscribe<UiEvents.ShowPercents>(OnShowPercents);
            _eventBus.Subscribe<UiEvents.SetCenterIcon>(OnSetCenterIcon);
            _eventBus.Subscribe<UiEvents.ShowInsufficientGold>(OnShowInsufficientGold);
            _eventBus.Subscribe<UiEvents.HideInsufficientGold>(OnHideInsufficientGold);
        }

        // Event Handlers
        void OnCountdownShow(UiEvents.CountdownShow e)
        {
            centerCard?.ShowCountdown(e.seconds);
        }

        void OnCountdownTick(UiEvents.CountdownTick e)
        {
            centerCard?.UpdateCountdown(e.value);
        }

        void OnCountdownHide(UiEvents.CountdownHide e)
        {
            centerCard?.HideCountdown();
        }

        void OnUpdateScoreboard(UiEvents.UpdateScoreboard e)
        {
            bottomBar?.UpdateScoreboard(e.a, e.b);
        }

        void OnShowPercents(UiEvents.ShowPercents e)
        {
            centerCard?.ShowPercents(e.p1, e.p2);
        }

        void OnSetCenterIcon(UiEvents.SetCenterIcon e)
        {
            centerCard?.SetCenterIcon();
        }

        void OnShowInsufficientGold(UiEvents.ShowInsufficientGold e)
        {
            if (insufficientGoldPanel != null)
                insufficientGoldPanel.SetActive(true);
        }

        void OnHideInsufficientGold(UiEvents.HideInsufficientGold e)
        {
            if (insufficientGoldPanel != null)
                insufficientGoldPanel.SetActive(false);
        }

        void OnDestroy()
        {
            if (_eventBus != null)
            {
                _eventBus.Unsubscribe<UiEvents.CountdownShow>(OnCountdownShow);
                _eventBus.Unsubscribe<UiEvents.CountdownTick>(OnCountdownTick);
                _eventBus.Unsubscribe<UiEvents.CountdownHide>(OnCountdownHide);
                _eventBus.Unsubscribe<UiEvents.UpdateScoreboard>(OnUpdateScoreboard);
                _eventBus.Unsubscribe<UiEvents.ShowPercents>(OnShowPercents);
                _eventBus.Unsubscribe<UiEvents.SetCenterIcon>(OnSetCenterIcon);
                _eventBus.Unsubscribe<UiEvents.ShowInsufficientGold>(OnShowInsufficientGold);
                _eventBus.Unsubscribe<UiEvents.HideInsufficientGold>(OnHideInsufficientGold);
            }
        }
    }
}
