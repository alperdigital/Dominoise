using UnityEngine;
using Game.UI.Prefabs;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    public sealed class UIViewer : MonoBehaviour
    {
        [Header("UI Viewer Settings")]
        [SerializeField] private bool showMainMenu = true;
        [SerializeField] private bool showGameUI = false;
        [SerializeField] private bool showLoadingUI = false;

        private UISceneManager _uiSceneManager;

        void Awake()
        {
            // Create UI Scene Manager
            var uiSceneManagerGO = new GameObject("UISceneManager");
            _uiSceneManager = uiSceneManagerGO.AddComponent<UISceneManager>();
            
            // Show initial UI
            if (showMainMenu)
                _uiSceneManager.ShowMainMenu();
            else if (showGameUI)
                _uiSceneManager.ShowGame();
            else if (showLoadingUI)
                _uiSceneManager.ShowLoading();
        }

        void Update()
        {
            // Keyboard shortcuts for testing
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _uiSceneManager.ShowMainMenu();
                UnityEngine.Debug.Log("Showing Main Menu");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _uiSceneManager.ShowGame();
                UnityEngine.Debug.Log("Showing Game UI");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _uiSceneManager.ShowLoading();
                UnityEngine.Debug.Log("Showing Loading UI");
            }
        }

        void OnGUI()
        {
            // Create a simple GUI for testing
            GUILayout.BeginArea(new Rect(10, 10, 200, 150));
            GUILayout.Label("UI Viewer Controls:");
            GUILayout.Label("1 - Main Menu");
            GUILayout.Label("2 - Game UI");
            GUILayout.Label("3 - Loading UI");
            GUILayout.EndArea();
        }
    }
}
