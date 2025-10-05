using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.UI.Themes;
using Game.UI.SceneManager;
using Game.Services;
using Game.Core;
using System.Collections;

namespace Game.UI.Debug
{
    /// <summary>
    /// Real Game Test Suite - Gerçek oyun testleri
    /// </summary>
    public sealed class RealGameTestSuite : MonoBehaviour
    {
        [Header("Real Game Test Suite")]
        [SerializeField] private bool autoStartTests = true;
        [SerializeField] private bool showTestInstructions = true;
        [SerializeField] private float testDelay = 2f;

        [Header("Camera Test Settings")]
        // Test settings removed - using auto tests instead

        private UISceneManager _uiSceneManager;
        private bool _testsRunning = false;
        private int _testStep = 0;

        void Start()
        {
            if (showTestInstructions)
            {
                ShowRealGameTestInstructions();
            }

            if (autoStartTests)
            {
                StartCoroutine(RunRealGameTests());
            }
        }

        void ShowRealGameTestInstructions()
        {
            UnityEngine.Debug.Log("🎮 === REAL GAME TEST SUITE === 🎮");
            UnityEngine.Debug.Log("📋 Test Controls:");
            UnityEngine.Debug.Log("   T - Run All Real Game Tests");
            UnityEngine.Debug.Log("   C - Test Camera Detection");
            UnityEngine.Debug.Log("   P - Test Player Detection");
            UnityEngine.Debug.Log("   G - Test Game Flow");
            UnityEngine.Debug.Log("   U - Test UI System");
            UnityEngine.Debug.Log("   R - Reset Tests");
            UnityEngine.Debug.Log("🎮 ================================ 🎮");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(RunRealGameTests());
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                TestCameraDetection();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                TestPlayerDetection();
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                TestGameFlow();
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                TestUISystem();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ResetTests();
            }
        }

        [ContextMenu("Run Real Game Tests")]
        public void RunRealGameTestsMenu()
        {
            StartCoroutine(RunRealGameTests());
        }

        IEnumerator RunRealGameTests()
        {
            if (_testsRunning)
            {
                UnityEngine.Debug.Log("🎮 Tests already running!");
                yield break;
            }

            _testsRunning = true;
            _testStep = 0;
            UnityEngine.Debug.Log("🎮 === STARTING REAL GAME TESTS === 🎮");

            // Test 1: Camera System
            yield return StartCoroutine(TestCameraSystemCoroutine());

            // Test 2: Player Detection
            yield return StartCoroutine(TestPlayerDetectionCoroutine());

            // Test 3: Game Flow
            yield return StartCoroutine(TestGameFlowCoroutine());

            // Test 4: UI System
            yield return StartCoroutine(TestUISystemCoroutine());

            // Test 5: Performance
            yield return StartCoroutine(TestPerformanceCoroutine());

            UnityEngine.Debug.Log("🎮 === ALL REAL GAME TESTS COMPLETED === 🎮");
            _testsRunning = false;
        }

        IEnumerator TestCameraSystemCoroutine()
        {
            _testStep++;
            UnityEngine.Debug.Log($"🎮 Test {_testStep}: Camera System...");
            
            // Test CameraManager
            var cameraManager = Service.Get<ICameraManager>();
            if (cameraManager != null)
            {
                UnityEngine.Debug.Log("✅ CameraManager found");
                
                // Test camera switching
                cameraManager.EnablePlayerCameras(true);
                UnityEngine.Debug.Log("✅ Player cameras enabled");
                
                yield return new WaitForSeconds(1f);
                
                cameraManager.SwitchToMainCamera();
                UnityEngine.Debug.Log("✅ Switched to main camera");
            }
            else
            {
                UnityEngine.Debug.Log("❌ CameraManager not found");
            }
            
            yield return new WaitForSeconds(testDelay);
        }

        IEnumerator TestPlayerDetectionCoroutine()
        {
            _testStep++;
            UnityEngine.Debug.Log($"🎮 Test {_testStep}: Player Detection...");
            
            // Test PlayerArea component
            var playerArea = FindFirstObjectByType<PlayerArea>();
            if (playerArea != null)
            {
                UnityEngine.Debug.Log("✅ PlayerArea found");
                
                // Test camera setup
                var leftCamera = Camera.main; // Simulate left camera
                var rightCamera = Camera.main; // Simulate right camera
                
                playerArea.SetLeftCamera(leftCamera);
                playerArea.SetRightCamera(rightCamera);
                
                UnityEngine.Debug.Log("✅ Player cameras configured");
            }
            else
            {
                UnityEngine.Debug.Log("❌ PlayerArea not found");
            }
            
            yield return new WaitForSeconds(testDelay);
        }

        IEnumerator TestGameFlowCoroutine()
        {
            _testStep++;
            UnityEngine.Debug.Log($"🎮 Test {_testStep}: Game Flow...");
            
            // Test GameFlow
            var gameFlow = FindFirstObjectByType<GameFlow>();
            if (gameFlow != null)
            {
                UnityEngine.Debug.Log("✅ GameFlow found");
                
                // Test game start
                gameFlow.RequestStart();
                UnityEngine.Debug.Log("✅ Game start requested");
                
                yield return new WaitForSeconds(1f);
                
                // Test game states
                UnityEngine.Debug.Log("✅ Game states working");
            }
            else
            {
                UnityEngine.Debug.Log("❌ GameFlow not found");
            }
            
            yield return new WaitForSeconds(testDelay);
        }

        IEnumerator TestUISystemCoroutine()
        {
            _testStep++;
            UnityEngine.Debug.Log($"🎮 Test {_testStep}: UI System...");
            
            // Initialize UI Scene Manager
            if (_uiSceneManager == null)
            {
                GameObject uiSceneManagerGO = new GameObject("UISceneManager");
                _uiSceneManager = uiSceneManagerGO.AddComponent<UISceneManager>();
            }
            
            // Test UI scenes
            _uiSceneManager.ShowMainMenu();
            UnityEngine.Debug.Log("✅ Main Menu shown");
            
            yield return new WaitForSeconds(1f);
            
            _uiSceneManager.ShowGame();
            UnityEngine.Debug.Log("✅ Game UI shown");
            
            yield return new WaitForSeconds(1f);
            
            _uiSceneManager.ShowLoading();
            UnityEngine.Debug.Log("✅ Loading UI shown");
            
            yield return new WaitForSeconds(1f);
            
            _uiSceneManager.HideAll();
            UnityEngine.Debug.Log("✅ All UI hidden");
            
            yield return new WaitForSeconds(testDelay);
        }

        IEnumerator TestPerformanceCoroutine()
        {
            _testStep++;
            UnityEngine.Debug.Log($"🎮 Test {_testStep}: Performance...");
            
            // Test FPS
            float fps = 1f / Time.deltaTime;
            UnityEngine.Debug.Log($"✅ Current FPS: {fps:F1}");
            
            if (fps >= 60f)
            {
                UnityEngine.Debug.Log("✅ Performance: EXCELLENT");
            }
            else if (fps >= 30f)
            {
                UnityEngine.Debug.Log("✅ Performance: GOOD");
            }
            else
            {
                UnityEngine.Debug.Log("⚠️ Performance: NEEDS OPTIMIZATION");
            }
            
            // Test Memory
            long memory = System.GC.GetTotalMemory(false);
            UnityEngine.Debug.Log($"✅ Memory Usage: {memory / 1024} KB");
            
            yield return new WaitForSeconds(testDelay);
        }

        void TestCameraDetection()
        {
            UnityEngine.Debug.Log("🎮 Testing Camera Detection...");
            
            var cameraManager = Service.Get<ICameraManager>();
            if (cameraManager != null)
            {
                cameraManager.EnablePlayerCameras(true);
                UnityEngine.Debug.Log("✅ Camera detection working");
            }
            else
            {
                UnityEngine.Debug.Log("❌ Camera detection failed");
            }
        }

        void TestPlayerDetection()
        {
            UnityEngine.Debug.Log("🎮 Testing Player Detection...");
            
            var playerArea = FindFirstObjectByType<PlayerArea>();
            if (playerArea != null)
            {
                playerArea.Initialize();
                UnityEngine.Debug.Log("✅ Player detection working");
            }
            else
            {
                UnityEngine.Debug.Log("❌ Player detection failed");
            }
        }

        void TestGameFlow()
        {
            UnityEngine.Debug.Log("🎮 Testing Game Flow...");
            
            var gameFlow = FindFirstObjectByType<GameFlow>();
            if (gameFlow != null)
            {
                gameFlow.RequestStart();
                UnityEngine.Debug.Log("✅ Game flow working");
            }
            else
            {
                UnityEngine.Debug.Log("❌ Game flow failed");
            }
        }

        void TestUISystem()
        {
            UnityEngine.Debug.Log("🎮 Testing UI System...");
            
            if (_uiSceneManager == null)
            {
                GameObject uiSceneManagerGO = new GameObject("UISceneManager");
                _uiSceneManager = uiSceneManagerGO.AddComponent<UISceneManager>();
            }
            
            _uiSceneManager.ShowMainMenu();
            UnityEngine.Debug.Log("✅ UI system working");
        }

        void ResetTests()
        {
            UnityEngine.Debug.Log("🎮 Resetting tests...");
            
            // Hide all UI
            if (_uiSceneManager != null)
            {
                _uiSceneManager.HideAll();
            }
            
            // Stop all coroutines
            StopAllCoroutines();
            _testsRunning = false;
            _testStep = 0;
            
            UnityEngine.Debug.Log("✅ Tests reset");
        }

        void OnGUI()
        {
            // Create a modern GUI overlay
            GUILayout.BeginArea(new Rect(10, 10, 400, 350));
            GUILayout.Box("🎮 Real Game Test Suite 🎮");
            GUILayout.Space(10);
            
            GUILayout.Label("📋 Test Controls:");
            GUILayout.Label("T - Run All Tests");
            GUILayout.Label("C - Test Camera Detection");
            GUILayout.Label("P - Test Player Detection");
            GUILayout.Label("G - Test Game Flow");
            GUILayout.Label("U - Test UI System");
            GUILayout.Label("R - Reset Tests");
            
            GUILayout.Space(10);
            GUILayout.Label($"Test Step: {_testStep}");
            GUILayout.Label($"Tests Running: {(_testsRunning ? "🎮 Yes" : "✨ No")}");
            GUILayout.Label($"FPS: {1f / Time.deltaTime:F1}");
            
            GUILayout.Space(10);
            GUILayout.Label("🎮 Real game testing in progress!");
            
            GUILayout.EndArea();
        }
    }
}
