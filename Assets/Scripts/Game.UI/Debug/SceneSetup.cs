using UnityEngine;
using Game.UI.Debug;
using Game.UI.SceneManager;
using Game.Services;
using Game.Core;

namespace Game.UI.Debug
{
    /// <summary>
    /// Scene setup için yardımcı script
    /// </summary>
    public sealed class SceneSetup : MonoBehaviour
    {
        [ContextMenu("🎮 Setup Test Scene")]
        void SetupTestScene()
        {
            UnityEngine.Debug.Log("🎮 === TEST SCENE SETUP === 🎮");
            
            // 1. Ana GameManager oluştur
            GameObject gameManager = new GameObject("GameManager");
            gameManager.AddComponent<GameManager>();
            gameManager.AddComponent<GameRunner>();
            gameManager.AddComponent<RealGameTestSuite>();
            
            UnityEngine.Debug.Log("✅ GameManager oluşturuldu");
            
            // 2. UI Manager oluştur
            GameObject uiManager = new GameObject("UISceneManager");
            var uiSceneManager = uiManager.AddComponent<UISceneManager>();
            DontDestroyOnLoad(uiManager);
            
            UnityEngine.Debug.Log("✅ UISceneManager oluşturuldu");
            
            // 3. Test Controller oluştur
            GameObject testController = new GameObject("TestController");
            testController.AddComponent<GameRunner>();
            
            UnityEngine.Debug.Log("✅ TestController oluşturuldu");
            
            UnityEngine.Debug.Log("🎮 === SCENE SETUP COMPLETE === 🎮");
            UnityEngine.Debug.Log("🎯 Press SPACE to setup");
            UnityEngine.Debug.Log("🎯 Press ENTER to run tests");
            UnityEngine.Debug.Log("🎯 Press ESC to show UI");
            UnityEngine.Debug.Log("🎯 Press T for all tests");
            UnityEngine.Debug.Log("🎯 Press C for camera test");
            UnityEngine.Debug.Log("🎯 Press P for player test");
            UnityEngine.Debug.Log("🎯 Press G for game flow test");
            UnityEngine.Debug.Log("🎯 Press U for UI test");
        }
        
        [ContextMenu("🎨 Create Modern UI Scene")]
        void CreateModernUIScene()
        {
            UnityEngine.Debug.Log("🎨 === CREATING MODERN UI SCENE === 🎨");
            
            // Modern UI Scene oluştur
            var uiManager = FindFirstObjectByType<UISceneManager>();
            if (uiManager == null)
            {
                GameObject uiGO = new GameObject("UISceneManager");
                uiManager = uiGO.AddComponent<UISceneManager>();
            }
            
            // Modern UI'ları göster
            uiManager.ShowMainMenu();
            UnityEngine.Debug.Log("✅ Modern Main Menu gösteriliyor");
            
            // 3 saniye sonra Game UI'ya geç
            Invoke(nameof(ShowGameUI), 3f);
        }
        
        void ShowGameUI()
        {
            var uiManager = FindFirstObjectByType<UISceneManager>();
            if (uiManager != null)
            {
                uiManager.ShowGame();
                UnityEngine.Debug.Log("✅ Modern Game UI gösteriliyor");
            }
        }
        
        [ContextMenu("🧪 Run Complete Test Suite")]
        void RunCompleteTestSuite()
        {
            UnityEngine.Debug.Log("🧪 === RUNNING COMPLETE TEST SUITE === 🧪");
            
            var testSuite = FindFirstObjectByType<RealGameTestSuite>();
            if (testSuite != null)
            {
                testSuite.RunRealGameTestsMenu();
                UnityEngine.Debug.Log("✅ Tüm testler çalıştırılıyor");
            }
            else
            {
                UnityEngine.Debug.LogError("❌ TestSuite bulunamadı!");
            }
        }
    }
}
