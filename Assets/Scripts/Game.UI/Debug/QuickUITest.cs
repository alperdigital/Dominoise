using UnityEngine;
using Game.UI.Prefabs;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    public sealed class QuickUITest : MonoBehaviour
    {
        [Header("Quick UI Test")]
        [SerializeField] private KeyCode mainMenuKey = KeyCode.Alpha1;
        [SerializeField] private KeyCode gameUIKey = KeyCode.Alpha2;
        [SerializeField] private KeyCode loadingUIKey = KeyCode.Alpha3;
        [SerializeField] private KeyCode hideUIKey = KeyCode.Alpha0;

        private UISceneManager _uiSceneManager;
        private bool _uiVisible = false;

        void Start()
        {
            // Create UI Scene Manager
            var uiSceneManagerGO = new GameObject("UISceneManager");
            _uiSceneManager = uiSceneManagerGO.AddComponent<UISceneManager>();
            
            UnityEngine.Debug.Log("=== UI Test Controls ===");
            UnityEngine.Debug.Log($"{mainMenuKey} - Show Main Menu");
            UnityEngine.Debug.Log($"{gameUIKey} - Show Game UI");
            UnityEngine.Debug.Log($"{loadingUIKey} - Show Loading UI");
            UnityEngine.Debug.Log($"{hideUIKey} - Hide All UI");
        }

        void Update()
        {
            if (Input.GetKeyDown(mainMenuKey))
            {
                _uiSceneManager.ShowMainMenu();
                _uiVisible = true;
                UnityEngine.Debug.Log("✅ Main Menu Shown");
            }
            else if (Input.GetKeyDown(gameUIKey))
            {
                _uiSceneManager.ShowGame();
                _uiVisible = true;
                UnityEngine.Debug.Log("✅ Game UI Shown");
            }
            else if (Input.GetKeyDown(loadingUIKey))
            {
                _uiSceneManager.ShowLoading();
                _uiVisible = true;
                UnityEngine.Debug.Log("✅ Loading UI Shown");
            }
            else if (Input.GetKeyDown(hideUIKey))
            {
                _uiSceneManager.HideAll();
                _uiVisible = false;
                UnityEngine.Debug.Log("✅ All UI Hidden");
            }
        }

        void OnGUI()
        {
            // Create a simple GUI overlay
            GUILayout.BeginArea(new Rect(10, 10, 300, 200));
            GUILayout.Box("🎮 UI Test Controls");
            GUILayout.Space(10);
            
            GUILayout.Label($"Press {mainMenuKey} - Main Menu");
            GUILayout.Label($"Press {gameUIKey} - Game UI");
            GUILayout.Label($"Press {loadingUIKey} - Loading UI");
            GUILayout.Label($"Press {hideUIKey} - Hide All");
            
            GUILayout.Space(10);
            GUILayout.Label($"UI Status: {(_uiVisible ? "Visible" : "Hidden")}");
            
            GUILayout.EndArea();
        }
    }
}
