using UnityEngine;
using Game.UI.Debug;
using Game.UI.SceneManager;
using Game.Services;
using Game.Core;

namespace Game.UI.Debug
{
    /// <summary>
    /// Oyunu çalıştırmak için tek tıkla setup
    /// </summary>
    public sealed class GameRunner : MonoBehaviour
    {
        [Header("🎮 Game Runner")]
        [SerializeField] private bool autoSetupOnStart = true;
        [SerializeField] private bool showInstructions = true;
        
        [Header("Test Components")]
        [SerializeField] private RealGameTestSuite testSuite;
        [SerializeField] private UISceneManager uiManager;
        
        void Start()
        {
            if (autoSetupOnStart)
            {
                SetupGame();
            }
            
            if (showInstructions)
            {
                ShowGameInstructions();
            }
        }
        
        [ContextMenu("🎮 Setup Game")]
        void SetupGame()
        {
            UnityEngine.Debug.Log("🎮 === DOMINOISE GAME SETUP === 🎮");
            
            // 1. Test Suite'i bul veya oluştur
            if (testSuite == null)
            {
                testSuite = FindFirstObjectByType<RealGameTestSuite>();
                if (testSuite == null)
                {
                    GameObject testGO = new GameObject("RealGameTestSuite");
                    testSuite = testGO.AddComponent<RealGameTestSuite>();
                    UnityEngine.Debug.Log("✅ RealGameTestSuite oluşturuldu");
                }
            }
            
            // 2. UI Manager'ı bul veya oluştur
            if (uiManager == null)
            {
                uiManager = FindFirstObjectByType<UISceneManager>();
                if (uiManager == null)
                {
                    GameObject uiGO = new GameObject("UISceneManager");
                    uiManager = uiGO.AddComponent<UISceneManager>();
                    DontDestroyOnLoad(uiGO);
                    UnityEngine.Debug.Log("✅ UISceneManager oluşturuldu");
                }
            }
            
            // 3. Bootstrapper'ı kontrol et
            var bootstrapper = FindFirstObjectByType<MonoBehaviour>(); // Generic olarak bul
            if (bootstrapper == null)
            {
                GameObject bootGO = new GameObject("Bootstrapper");
                // Bootstrapper component'i App assembly'sinde olduğu için burada oluşturamayız
                UnityEngine.Debug.Log("⚠️ Bootstrapper App assembly'sinde - manuel olarak eklenmeli");
            }
            
            UnityEngine.Debug.Log("🎮 === GAME SETUP COMPLETE === 🎮");
            UnityEngine.Debug.Log("🎯 Oyun hazır! Test tuşlarını kullanın:");
            UnityEngine.Debug.Log("T = Tüm testleri çalıştır");
            UnityEngine.Debug.Log("C = Kamera testi");
            UnityEngine.Debug.Log("P = Oyuncu testi");
            UnityEngine.Debug.Log("G = Oyun akışı testi");
            UnityEngine.Debug.Log("U = UI testi");
        }
        
        void ShowGameInstructions()
        {
            UnityEngine.Debug.Log("🎮 === DOMINOISE GAME INSTRUCTIONS === 🎮");
            UnityEngine.Debug.Log("🎯 Modern Similarity Game - Camera Based");
            UnityEngine.Debug.Log("📱 Mobile Ready (iOS/Android)");
            UnityEngine.Debug.Log("🎨 Modern Minimal UI (Apple Style)");
            UnityEngine.Debug.Log("🔧 Service Locator Architecture");
            UnityEngine.Debug.Log("📡 Event-Driven System");
            UnityEngine.Debug.Log("🧪 Comprehensive Test Suite");
            UnityEngine.Debug.Log("=====================================");
        }
        
        [ContextMenu("🚀 Start All Tests")]
        void StartAllTests()
        {
            if (testSuite != null)
            {
                testSuite.RunRealGameTestsMenu();
            }
            else
            {
                UnityEngine.Debug.LogError("❌ TestSuite bulunamadı! Önce Setup Game'i çalıştırın.");
            }
        }
        
        [ContextMenu("🎨 Show Modern UI")]
        void ShowModernUI()
        {
            if (uiManager != null)
            {
                uiManager.ShowMainMenu();
                UnityEngine.Debug.Log("✅ Modern UI gösteriliyor");
            }
            else
            {
                UnityEngine.Debug.LogError("❌ UISceneManager bulunamadı! Önce Setup Game'i çalıştırın.");
            }
        }
        
        void Update()
        {
            // Hızlı test tuşları
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetupGame();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                StartAllTests();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowModernUI();
            }
        }
    }
}
