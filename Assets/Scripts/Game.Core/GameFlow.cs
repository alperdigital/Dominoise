using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Services;
using Game.Services.Economy;
using Game.SharedSo;

namespace Game.Core
{
    public sealed class GameFlow : MonoBehaviour
    {
        // Artık GameRulesSo'yu doğrudan ServiceLocator'dan alacağız,
        // ancak GameFlow'un rules'a sık erişimi olacağı için
        // bunu bir property olarak tutmak clean-code açısından daha pratik.
        private GameRulesSo Rules => Service.Get<GameRulesSo>();

        Dictionary<Type, IGameState> _states;
        public IGameState Current { get; private set; }

        void Awake()
        {
            // GameRulesSo'nun Bootstrapper'da Service.Register edildiğini varsayıyoruz.
            
            _states = new()
            {
                { typeof(LobbyState),     new LobbyState(this) },
                { typeof(CountdownState), new CountdownState(this) },
                { typeof(PoseState),      new PoseState(this) },
                { typeof(ScoringState),   new ScoringState(this) },
                { typeof(ResultState),    new ResultState(this) }
            };
            Change<LobbyState>();
        }

        public void Change<T>() where T:IGameState
        {
            Current?.Exit();
            Current = _states[typeof(T)];
            Current.Enter();
        }

        void Update() => Current?.Tick();

        // UI butonları buradan çağırır
        public void RequestStart()
        {
            // Servislere erişim
            var eco = Service.Get<IEconomy>();
            var economyConfig = Service.Get<EconomyConfigSo>();

            if (!eco.TrySpend(economyConfig.roundCost))
            {
                // altın yok → UI'ye haber
                Service.Get<IEventBus>().Publish(new UiEvents.ShowInsufficientGold());
                return;
            }
            Service.Get<IEventBus>().Publish(new UiEvents.HideInsufficientGold());
            Change<CountdownState>();
        }
    }
}