using UnityEngine;
using Game.UI.Prefabs;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    /// <summary>
    /// UI Test Setup - Unity'de manuel olarak scene oluşturmak için rehber
    /// </summary>
    public sealed class UITestSetup : MonoBehaviour
    {
        [Header("UI Test Setup")]
        [SerializeField] private bool autoSetup = true;
        [SerializeField] private bool showInstructions = true;

        void Start()
        {
            if (showInstructions)
            {
                ShowSetupInstructions();
            }

            if (autoSetup)
            {
                SetupUITest();
            }
        }

        void ShowSetupInstructions()
        {
            UnityEngine.Debug.Log("=== UI Test Setup Instructions ===");
            UnityEngine.Debug.Log("1. Create Empty GameObject");
            UnityEngine.Debug.Log("2. Add QuickUITest component");
            UnityEngine.Debug.Log("3. Press Play to test UI");
            UnityEngine.Debug.Log("4. Use keyboard keys: 1, 2, 3, 0");
            UnityEngine.Debug.Log("================================");
        }

        void SetupUITest()
        {
            // Create UI Test GameObject
            GameObject uiTestGO = new GameObject("UI Test Controller");
            uiTestGO.AddComponent<QuickUITest>();
            
            UnityEngine.Debug.Log("✅ UI Test Setup Complete!");
            UnityEngine.Debug.Log("Press Play and use keyboard keys to test UI");
        }
    }
}
