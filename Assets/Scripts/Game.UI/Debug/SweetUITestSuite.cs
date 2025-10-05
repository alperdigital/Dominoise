using UnityEngine;
using Game.UI.Themes;
using Game.UI.SceneManager;
using System.Collections;

namespace Game.UI.Debug
{
    /// <summary>
    /// Sweet UI Test Suite - Kapsamlı test sistemi
    /// </summary>
    public sealed class SweetUITestSuite : MonoBehaviour
    {
        [Header("Sweet UI Test Suite")]
        [SerializeField] private bool autoStartTests = true;
        [SerializeField] private bool showTestInstructions = true;
        [SerializeField] private float testDelay = 1f;

        private UISceneManager _uiSceneManager;
        private bool _testsRunning = false;

        void Start()
        {
            if (showTestInstructions)
            {
                ShowTestInstructions();
            }

            if (autoStartTests)
            {
                StartCoroutine(RunAllTests());
            }
        }

        void ShowTestInstructions()
        {
            UnityEngine.Debug.Log("🍭 === SWEET UI TEST SUITE === 🍭");
            UnityEngine.Debug.Log("🎮 Test Controls:");
            UnityEngine.Debug.Log("   T - Run All Tests");
            UnityEngine.Debug.Log("   1 - Test Main Menu");
            UnityEngine.Debug.Log("   2 - Test Game UI");
            UnityEngine.Debug.Log("   3 - Test Loading UI");
            UnityEngine.Debug.Log("   0 - Hide All UI");
            UnityEngine.Debug.Log("   R - Reset Tests");
            UnityEngine.Debug.Log("🍭 ================================ 🍭");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(RunAllTests());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TestMainMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TestGameUI();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TestLoadingUI();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                HideAllUI();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ResetTests();
            }
        }

        [ContextMenu("Run All Tests")]
        public void RunAllTestsMenu()
        {
            StartCoroutine(RunAllTests());
        }

        IEnumerator RunAllTests()
        {
            if (_testsRunning)
            {
                UnityEngine.Debug.Log("🍭 Tests already running!");
                yield break;
            }

            _testsRunning = true;
            UnityEngine.Debug.Log("🍭 === STARTING SWEET UI TESTS === 🍭");

            // Initialize UI Scene Manager
            InitializeUISceneManager();

            // Test 1: Main Menu
            yield return StartCoroutine(TestMainMenuCoroutine());

            // Test 2: Game UI
            yield return StartCoroutine(TestGameUICoroutine());

            // Test 3: Loading UI
            yield return StartCoroutine(TestLoadingUICoroutine());

            // Test 4: Hide All
            yield return StartCoroutine(TestHideAllCoroutine());

            // Test 5: Theme System
            yield return StartCoroutine(TestThemeSystemCoroutine());

            // Test 6: Animations
            yield return StartCoroutine(TestAnimationsCoroutine());

            UnityEngine.Debug.Log("🍭 === ALL TESTS COMPLETED === 🍭");
            _testsRunning = false;
        }

        void InitializeUISceneManager()
        {
            if (_uiSceneManager == null)
            {
                GameObject uiSceneManagerGO = new GameObject("UISceneManager");
                _uiSceneManager = uiSceneManagerGO.AddComponent<UISceneManager>();
                UnityEngine.Debug.Log("🍭 UISceneManager initialized");
            }
        }

        IEnumerator TestMainMenuCoroutine()
        {
            UnityEngine.Debug.Log("🍭 Testing Main Menu...");
            
            if (_uiSceneManager != null)
            {
                _uiSceneManager.ShowMainMenu();
                UnityEngine.Debug.Log("✅ Main Menu shown");
            }
            
            yield return new WaitForSeconds(testDelay);
        }

        IEnumerator TestGameUICoroutine()
        {
            UnityEngine.Debug.Log("🍭 Testing Game UI...");
            
            if (_uiSceneManager != null)
            {
                _uiSceneManager.ShowGame();
                UnityEngine.Debug.Log("✅ Game UI shown");
            }
            
            yield return new WaitForSeconds(testDelay);
        }

        IEnumerator TestLoadingUICoroutine()
        {
            UnityEngine.Debug.Log("🍭 Testing Loading UI...");
            
            if (_uiSceneManager != null)
            {
                _uiSceneManager.ShowLoading();
                UnityEngine.Debug.Log("✅ Loading UI shown");
            }
            
            yield return new WaitForSeconds(testDelay);
        }

        IEnumerator TestHideAllCoroutine()
        {
            UnityEngine.Debug.Log("🍭 Testing Hide All...");
            
            if (_uiSceneManager != null)
            {
                _uiSceneManager.HideAll();
                UnityEngine.Debug.Log("✅ All UI hidden");
            }
            
            yield return new WaitForSeconds(testDelay);
        }

        IEnumerator TestThemeSystemCoroutine()
        {
            UnityEngine.Debug.Log("🍭 Testing Theme System...");
            
            // Test EightBitTheme
            if (EightBitTheme.Instance == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                themeGO.AddComponent<EightBitTheme>();
                UnityEngine.Debug.Log("✅ EightBitTheme created");
            }
            else
            {
                UnityEngine.Debug.Log("✅ EightBitTheme already exists");
            }
            
            yield return new WaitForSeconds(testDelay);
        }

        IEnumerator TestAnimationsCoroutine()
        {
            UnityEngine.Debug.Log("🍭 Testing Animations...");
            
            // Test sweet button creation
            if (EightBitTheme.Instance != null)
            {
                GameObject testButton = EightBitTheme.Instance.CreateSweetButton(
                    "🍭 TEST BUTTON", 
                    Vector2.zero, 
                    new Vector2(200, 60), 
                    () => UnityEngine.Debug.Log("🍭 Test button clicked!")
                );
                
                testButton.transform.SetParent(transform, false);
                UnityEngine.Debug.Log("✅ Sweet button created and animated");
            }
            
            yield return new WaitForSeconds(testDelay);
        }

        void TestMainMenu()
        {
            UnityEngine.Debug.Log("🍭 Testing Main Menu...");
            
            if (_uiSceneManager != null)
            {
                _uiSceneManager.ShowMainMenu();
                UnityEngine.Debug.Log("✅ Main Menu shown");
            }
            else
            {
                UnityEngine.Debug.Log("❌ UISceneManager not found");
            }
        }

        void TestGameUI()
        {
            UnityEngine.Debug.Log("🍭 Testing Game UI...");
            
            if (_uiSceneManager != null)
            {
                _uiSceneManager.ShowGame();
                UnityEngine.Debug.Log("✅ Game UI shown");
            }
            else
            {
                UnityEngine.Debug.Log("❌ UISceneManager not found");
            }
        }

        void TestLoadingUI()
        {
            UnityEngine.Debug.Log("🍭 Testing Loading UI...");
            
            if (_uiSceneManager != null)
            {
                _uiSceneManager.ShowLoading();
                UnityEngine.Debug.Log("✅ Loading UI shown");
            }
            else
            {
                UnityEngine.Debug.Log("❌ UISceneManager not found");
            }
        }

        void HideAllUI()
        {
            UnityEngine.Debug.Log("🍭 Hiding all UI...");
            
            if (_uiSceneManager != null)
            {
                _uiSceneManager.HideAll();
                UnityEngine.Debug.Log("✅ All UI hidden");
            }
            else
            {
                UnityEngine.Debug.Log("❌ UISceneManager not found");
            }
        }

        void ResetTests()
        {
            UnityEngine.Debug.Log("🍭 Resetting tests...");
            
            // Hide all UI
            if (_uiSceneManager != null)
            {
                _uiSceneManager.HideAll();
            }
            
            // Stop all coroutines
            StopAllCoroutines();
            _testsRunning = false;
            
            UnityEngine.Debug.Log("✅ Tests reset");
        }

        void OnGUI()
        {
            // Create a sweet GUI overlay
            GUILayout.BeginArea(new Rect(10, 10, 400, 300));
            GUILayout.Box("🍭 Sweet UI Test Suite 🍭");
            GUILayout.Space(10);
            
            GUILayout.Label("🎮 Test Controls:");
            GUILayout.Label("T - Run All Tests");
            GUILayout.Label("1 - Test Main Menu");
            GUILayout.Label("2 - Test Game UI");
            GUILayout.Label("3 - Test Loading UI");
            GUILayout.Label("0 - Hide All UI");
            GUILayout.Label("R - Reset Tests");
            
            GUILayout.Space(10);
            GUILayout.Label($"Tests Running: {(_testsRunning ? "🍭 Yes" : "✨ No")}");
            
            GUILayout.Space(10);
            GUILayout.Label("🎮 Enjoy your sweet 8-bit adventure!");
            
            GUILayout.EndArea();
        }
    }
}
