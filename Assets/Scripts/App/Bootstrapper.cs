using UnityEngine;
using Game.Services;
using Game.Services.Ads;
using Game.Services.Economy;
using Game.SharedSo;
using Game.UI;

// Bootstrapper, tüm modülleri bir araya getiren tek bir namespace'te durabilir.
namespace App 
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        [Header("Configuration Files")]
        [SerializeField] private GameRulesSo gameRules;
        [SerializeField] private EconomyConfigSo economyConfig;
        
        void Awake()
        {
            // Tüm servisleri barındıracak tek bir kalıcı GameObject oluşturulur.
            var servicesHost = new GameObject("Services");
            DontDestroyOnLoad(servicesHost);
            
            // --- 1. JENERİK TEMEL SERVİSLER (Hemen Hemen Her Projede Var) ---

            // EventBus'ı (Olay Yöneticisi) kaydet.
            Service.Register<IEventBus>(new EventBus());
            
            // TimerService'i (Zamanlayıcı) Component olarak ekle ve kaydet.
            var timerService = servicesHost.AddComponent<TimerService>();
            Service.Register<ITimerService>(timerService);

            // Camera Manager'ı ekle ve kaydet.
            var cameraManager = servicesHost.AddComponent<CameraManager>();
            Service.Register<ICameraManager>(cameraManager);

            // --- 2. OYUNA ÖZEL SERVİSLER ---

            // Ekonomi Servisi: Component olarak ekle, ayarla ve kaydet.
            var economyManager = servicesHost.AddComponent<EconomyManager>();
            economyManager.Init(economyConfig); 
            Service.Register<IEconomy>(economyManager);
            
            // Reklam Servisi (Dummy): Component olarak ekle ve kaydet.
            var adsService = servicesHost.AddComponent<DummyAdService>();
            Service.Register<IAds>(adsService);

            // --- 3. UI SİSTEMİNİ OLUŞTUR ---
            
            // UI Scene Manager'ı oluştur
            var uiSceneManagerGO = new GameObject("UISceneManager");
            var uiSceneManager = uiSceneManagerGO.AddComponent<Game.UI.SceneManager.UISceneManager>();
            DontDestroyOnLoad(uiSceneManagerGO);

            // Ana UI Canvas'ını oluştur
            var mainCanvas = UITemplateCreator.CreateMainUICanvas();
            DontDestroyOnLoad(mainCanvas);

            // Popup Manager'ı ekle
            var popupManager = mainCanvas.AddComponent<PopupManager>();

            // --- 4. AYAR DOSYALARINI KAYDET (Oyunun Her Yerinden Erişim İçin) ---
            
            // GameRulesSo ve EconomyConfigSo'yu doğrudan ServiceLocator'a kaydet.
            // Bu, GameFlow ve State script'lerinin Rules'a temiz erişimini sağlar.
            Service.Register(gameRules);
            Service.Register(economyConfig);
        }
    }
}
