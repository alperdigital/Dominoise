using UnityEngine;
using Game.UI.Prefabs;
using Game.UI.SceneManager;
using Game.UI.Themes;

namespace Game.UI.Debug
{
    /// <summary>
    /// Sweet UI Test - 8-bit temalı UI test sistemi
    /// </summary>
    public sealed class SweetUITest : MonoBehaviour
    {
        [Header("Sweet UI Test Settings")]
        [SerializeField] private bool autoSetup = true;
        [SerializeField] private bool showSweetInstructions = true;
        [SerializeField] private KeyCode mainMenuKey = KeyCode.Alpha1;
        [SerializeField] private KeyCode gameUIKey = KeyCode.Alpha2;
        [SerializeField] private KeyCode loadingUIKey = KeyCode.Alpha3;
        [SerializeField] private KeyCode hideUIKey = KeyCode.Alpha0;

        private UISceneManager _uiSceneManager;
        private bool _uiVisible = false;

        void Start()
        {
            if (showSweetInstructions)
            {
                ShowSweetInstructions();
            }

            if (autoSetup)
            {
                SetupSweetUITest();
            }
        }

        void ShowSweetInstructions()
        {
            UnityEngine.Debug.Log("🍭 === SWEET UI TEST INSTRUCTIONS === 🍭");
            UnityEngine.Debug.Log("🎮 Press 1 - Show Sweet Main Menu");
            UnityEngine.Debug.Log("🎮 Press 2 - Show Sweet Game UI");
            UnityEngine.Debug.Log("🎮 Press 3 - Show Sweet Loading UI");
            UnityEngine.Debug.Log("🎮 Press 0 - Hide All Sweet UI");
            UnityEngine.Debug.Log("✨ Enjoy your sweet 8-bit adventure! ✨");
            UnityEngine.Debug.Log("🍭 ================================ 🍭");
        }

        void SetupSweetUITest()
        {
            // Create Sweet UI Test GameObject
            GameObject sweetUITestGO = new GameObject("Sweet UI Test Controller");
            sweetUITestGO.AddComponent<SweetUITest>();
            
            // Initialize EightBitTheme
            if (EightBitTheme.Instance == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                themeGO.AddComponent<EightBitTheme>();
            }
            
            // Create UI Scene Manager
            var uiSceneManagerGO = new GameObject("UISceneManager");
            _uiSceneManager = uiSceneManagerGO.AddComponent<UISceneManager>();
            
            UnityEngine.Debug.Log("🍭 Sweet UI Test Setup Complete!");
            UnityEngine.Debug.Log("🎮 Press Play and use keyboard keys to test sweet UI");
        }

        void Update()
        {
            if (Input.GetKeyDown(mainMenuKey))
            {
                if (_uiSceneManager != null)
                {
                    _uiSceneManager.ShowMainMenu();
                    _uiVisible = true;
                    UnityEngine.Debug.Log("🍭 Sweet Main Menu Shown!");
                }
            }
            else if (Input.GetKeyDown(gameUIKey))
            {
                if (_uiSceneManager != null)
                {
                    _uiSceneManager.ShowGame();
                    _uiVisible = true;
                    UnityEngine.Debug.Log("🎮 Sweet Game UI Shown!");
                }
            }
            else if (Input.GetKeyDown(loadingUIKey))
            {
                if (_uiSceneManager != null)
                {
                    _uiSceneManager.ShowLoading();
                    _uiVisible = true;
                    UnityEngine.Debug.Log("⏳ Sweet Loading UI Shown!");
                }
            }
            else if (Input.GetKeyDown(hideUIKey))
            {
                if (_uiSceneManager != null)
                {
                    _uiSceneManager.HideAll();
                    _uiVisible = false;
                    UnityEngine.Debug.Log("✨ All Sweet UI Hidden!");
                }
            }
        }

        void OnGUI()
        {
            // Create a sweet GUI overlay
            GUILayout.BeginArea(new Rect(10, 10, 350, 250));
            GUILayout.Box("🍭 Sweet UI Test Controls 🍭");
            GUILayout.Space(10);
            
            GUILayout.Label($"Press {mainMenuKey} - 🍭 Sweet Main Menu");
            GUILayout.Label($"Press {gameUIKey} - 🎮 Sweet Game UI");
            GUILayout.Label($"Press {loadingUIKey} - ⏳ Sweet Loading UI");
            GUILayout.Label($"Press {hideUIKey} - ✨ Hide All Sweet UI");
            
            GUILayout.Space(10);
            GUILayout.Label($"Sweet UI Status: {(_uiVisible ? "🍭 Visible" : "✨ Hidden")}");
            
            GUILayout.Space(10);
            GUILayout.Label("🎮 Enjoy your sweet 8-bit adventure!");
            
            GUILayout.EndArea();
        }
    }
}
