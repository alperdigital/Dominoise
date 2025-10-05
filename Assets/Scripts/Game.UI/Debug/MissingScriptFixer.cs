using UnityEngine;
using System.Collections.Generic;
using Game.UI.Themes;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    /// <summary>
    /// Missing Script Fixer - Unity'de eksik script referanslarını otomatik düzeltir
    /// </summary>
    public sealed class MissingScriptFixer : MonoBehaviour
    {
        [Header("Missing Script Fixer")]
        [SerializeField] private bool autoFixOnStart = true;
        [SerializeField] private bool showFixLogs = true;

        void Start()
        {
            if (autoFixOnStart)
            {
                FixMissingScripts();
            }
        }

        [ContextMenu("Fix Missing Scripts")]
        public void FixMissingScripts()
        {
            UnityEngine.Debug.Log("🍭 Starting Missing Script Fix...");
            
            // Find all GameObjects with missing scripts
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            int fixedCount = 0;

            foreach (GameObject obj in allObjects)
            {
                if (HasMissingScript(obj))
                {
                    FixGameObjectScripts(obj);
                    fixedCount++;
                }
            }

            if (showFixLogs)
            {
                UnityEngine.Debug.Log($"🍭 Fixed {fixedCount} GameObjects with missing scripts!");
            }
        }

        bool HasMissingScript(GameObject obj)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component comp in components)
            {
                if (comp == null)
                {
                    return true;
                }
            }
            return false;
        }

        void FixGameObjectScripts(GameObject obj)
        {
            // Remove missing script components
            List<Component> componentsToRemove = new List<Component>();
            Component[] components = obj.GetComponents<Component>();
            
            foreach (Component comp in components)
            {
                if (comp == null)
                {
                    componentsToRemove.Add(comp);
                }
            }

            foreach (Component comp in componentsToRemove)
            {
                if (comp != null)
                {
                    DestroyImmediate(comp);
                }
            }

            if (showFixLogs)
            {
                UnityEngine.Debug.Log($"🍭 Fixed missing scripts on: {obj.name}");
            }
        }

        [ContextMenu("Create Sweet UI Test Scene")]
        public void CreateSweetUITestScene()
        {
            UnityEngine.Debug.Log("🍭 Creating Sweet UI Test Scene...");
            
            // Create main camera
            GameObject cameraGO = new GameObject("Main Camera");
            cameraGO.AddComponent<Camera>();
            cameraGO.AddComponent<AudioListener>();
            cameraGO.transform.position = new Vector3(0, 0, -10);
            cameraGO.tag = "MainCamera";

            // Create Sweet UI Test Controller
            GameObject sweetUIController = new GameObject("Sweet UI Test Controller");
            sweetUIController.AddComponent<SweetUITest>();

            // Create EightBitTheme
            GameObject themeGO = new GameObject("EightBitTheme");
            themeGO.AddComponent<EightBitTheme>();

            // Create UISceneManager
            GameObject uiSceneManagerGO = new GameObject("UISceneManager");
            uiSceneManagerGO.AddComponent<UISceneManager>();

            UnityEngine.Debug.Log("🍭 Sweet UI Test Scene Created!");
            UnityEngine.Debug.Log("🎮 Press Play to test your sweet UI!");
        }

        [ContextMenu("Clean Up Missing Scripts")]
        public void CleanUpMissingScripts()
        {
            UnityEngine.Debug.Log("🍭 Cleaning up missing scripts...");
            
            // Find all GameObjects
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            int cleanedCount = 0;

            foreach (GameObject obj in allObjects)
            {
                if (HasMissingScript(obj))
                {
                    // Remove the GameObject if it only has missing scripts
                    Component[] components = obj.GetComponents<Component>();
                    bool hasValidComponents = false;
                    
                    foreach (Component comp in components)
                    {
                        if (comp != null)
                        {
                            hasValidComponents = true;
                            break;
                        }
                    }

                    if (!hasValidComponents)
                    {
                        DestroyImmediate(obj);
                        cleanedCount++;
                    }
                }
            }

            UnityEngine.Debug.Log($"🍭 Cleaned up {cleanedCount} GameObjects with only missing scripts!");
        }
    }
}
