using UnityEngine;
using Game.Services; // Service (ServiceLocator), IEventBus ve IEvent için
using Game.SharedSo; // EconomyConfigSo için

namespace Game.Services.Economy
{
    // YENİ: Ekonomi ile ilgili tüm event'ler bu namespace içinde tanımlanır.
    public static class EconomyEvents
    {
        // Bakiyenin güncellendiğini bildiren event. UI, GameFlow ve diğer servisler bunu dinler.
        public struct BalanceUpdated : IEvent
        {
            public int CurrentBalance;
            public BalanceUpdated(int balance) => CurrentBalance = balance;
        }
    }

    public sealed class EconomyManager : MonoBehaviour, IEconomy
    {
        const string KEY = "eco.gold.v1";
        int _balance;
        EconomyConfigSo _cfg;

        public int Balance => _balance;

        public void Init(EconomyConfigSo cfg)
        {
            _cfg = cfg;
            _balance = PlayerPrefs.GetInt(KEY, _cfg.startGold);
            Notify();
        }

        public bool TrySpend(int amount)
        {
            if (_balance < amount) return false;
            _balance -= amount; Save(); Notify(); return true;
        }

        public void Grant(int amount)
        {
            _balance += amount; Save(); Notify();
        }

        void Save(){ PlayerPrefs.SetInt(KEY, _balance); }
        
        // Notify metodu artık UI event'i yerine, kendi servisinin event'ini yayınlıyor.
        void Notify()
        { 
            // Bu event'i yayınlamak için artık UI'a ait hiçbir şeye ihtiyacımız yok.
            // UI, sadece bu event'i dinleyerek kendi UpdateScoreboard'unu çağıracak.
            Service.Get<IEventBus>().Publish(new EconomyEvents.BalanceUpdated(_balance)); 
        } 
    }
}
