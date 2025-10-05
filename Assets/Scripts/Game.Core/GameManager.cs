using UnityEngine;
using Game.Services;
using Game.Services.Economy;
using Game.SharedSo;

namespace Game.Core
{
    public sealed class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private GameRulesSo gameRules;
        [SerializeField] private EconomyConfigSo economyConfig;

        [Header("Managers")]
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private GameFlow gameFlow;

        private IEventBus _eventBus;
        private IEconomy _economy;
        private ICameraManager _cameraManager;

        public GameRulesSo GameRules => gameRules;
        public EconomyConfigSo EconomyConfig => economyConfig;
        public ICameraManager CameraManager => _cameraManager;

        void Awake()
        {
            InitializeServices();
            InitializeManagers();
        }

        void InitializeServices()
        {
            _eventBus = Service.Get<IEventBus>();
            _economy = Service.Get<IEconomy>();
            _cameraManager = cameraManager;
        }

        void InitializeManagers()
        {
            if (gameFlow == null)
                gameFlow = GetComponent<GameFlow>();

            if (cameraManager == null)
                cameraManager = GetComponent<CameraManager>();
        }

        public void StartGame()
        {
            if (_economy != null && economyConfig != null)
            {
                if (_economy.TrySpend(economyConfig.roundCost))
                {
                    _eventBus?.Publish(new UiEvents.HideInsufficientGold());
                    gameFlow?.Change<CountdownState>();
                }
                else
                {
                    _eventBus?.Publish(new UiEvents.ShowInsufficientGold());
                }
            }
        }

        public void EndGame()
        {
            if (cameraManager != null)
                cameraManager.SwitchToMainCamera();
        }

        public void ResetGame()
        {
            if (gameFlow != null)
                gameFlow.Change<LobbyState>();
        }
    }
}
