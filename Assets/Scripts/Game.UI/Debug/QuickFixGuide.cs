using UnityEngine;
using Game.UI.Themes;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    /// <summary>
    /// Quick Fix Guide - Unity'de "Unknown script" hatalarını hızlıca düzeltir
    /// </summary>
    public sealed class QuickFixGuide : MonoBehaviour
    {
        [Header("Quick Fix Guide")]
        [SerializeField] private bool showInstructions = true;
        [SerializeField] private bool autoCreateCleanScene = true;

        void Start()
        {
            if (showInstructions)
            {
                ShowQuickFixInstructions();
            }

            if (autoCreateCleanScene)
            {
                CreateCleanScene();
            }
        }

        void ShowQuickFixInstructions()
        {
            UnityEngine.Debug.Log("🍭 === QUICK FIX GUIDE === 🍭");
            UnityEngine.Debug.Log("📋 Step 1: Delete all GameObjects with unknown scripts");
            UnityEngine.Debug.Log("📋 Step 2: Create new clean scene");
            UnityEngine.Debug.Log("📋 Step 3: Add SweetUITest component");
            UnityEngine.Debug.Log("📋 Step 4: Press Play to test");
            UnityEngine.Debug.Log("🍭 ======================== 🍭");
        }

        void CreateCleanScene()
        {
            UnityEngine.Debug.Log("🍭 Creating clean scene...");
            
            // Create main camera if not exists
            if (Camera.main == null)
            {
                GameObject cameraGO = new GameObject("Main Camera");
                cameraGO.AddComponent<Camera>();
                cameraGO.AddComponent<AudioListener>();
                cameraGO.transform.position = new Vector3(0, 0, -10);
                cameraGO.tag = "MainCamera";
            }

            // Create Sweet UI Test Controller
            GameObject sweetUIController = new GameObject("Sweet UI Test Controller");
            sweetUIController.AddComponent<SweetUITest>();

            // Create EightBitTheme
            if (EightBitTheme.Instance == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                themeGO.AddComponent<EightBitTheme>();
            }

            // Create UISceneManager
            GameObject uiSceneManagerGO = new GameObject("UISceneManager");
            uiSceneManagerGO.AddComponent<UISceneManager>();

            UnityEngine.Debug.Log("🍭 Clean scene created successfully!");
            UnityEngine.Debug.Log("🎮 Press Play to test your sweet UI!");
        }

        [ContextMenu("Quick Fix Unknown Scripts")]
        public void QuickFixUnknownScripts()
        {
            UnityEngine.Debug.Log("🍭 Quick fixing unknown scripts...");
            
            // Find all GameObjects
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            
            foreach (GameObject obj in allObjects)
            {
                if (obj != gameObject)
                {
                    // Check if object has unknown scripts
                    Component[] components = obj.GetComponents<Component>();
                    bool hasUnknownScripts = false;
                    
                    foreach (Component comp in components)
                    {
                        if (comp == null)
                        {
                            hasUnknownScripts = true;
                            break;
                        }
                    }

                    // Remove objects with unknown scripts
                    if (hasUnknownScripts)
                    {
                        DestroyImmediate(obj);
                        UnityEngine.Debug.Log($"🍭 Removed object with unknown scripts: {obj.name}");
                    }
                }
            }

            UnityEngine.Debug.Log("🍭 Quick fix complete!");
        }
    }
}
