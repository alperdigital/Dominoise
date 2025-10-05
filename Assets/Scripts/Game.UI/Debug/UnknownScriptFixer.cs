using UnityEngine;
using System.Collections.Generic;
using Game.UI.Themes;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    /// <summary>
    /// Unknown Script Fixer - Unity'de "Unknown script" hatalarını düzeltir
    /// </summary>
    public sealed class UnknownScriptFixer : MonoBehaviour
    {
        [Header("Unknown Script Fixer")]
        [SerializeField] private bool autoFixOnStart = true;
        [SerializeField] private bool showFixLogs = true;

        void Start()
        {
            if (autoFixOnStart)
            {
                FixUnknownScripts();
            }
        }

        [ContextMenu("Fix Unknown Scripts")]
        public void FixUnknownScripts()
        {
            UnityEngine.Debug.Log("🍭 Starting Unknown Script Fix...");
            
            // Find all GameObjects with unknown scripts
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            int fixedCount = 0;

            foreach (GameObject obj in allObjects)
            {
                if (HasUnknownScript(obj))
                {
                    FixGameObjectUnknownScripts(obj);
                    fixedCount++;
                }
            }

            if (showFixLogs)
            {
                UnityEngine.Debug.Log($"🍭 Fixed {fixedCount} GameObjects with unknown scripts!");
            }
        }

        bool HasUnknownScript(GameObject obj)
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

        void FixGameObjectUnknownScripts(GameObject obj)
        {
            // Remove unknown script components
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
                UnityEngine.Debug.Log($"🍭 Fixed unknown scripts on: {obj.name}");
            }
        }

        [ContextMenu("Create Clean Sweet UI Scene")]
        public void CreateCleanSweetUIScene()
        {
            UnityEngine.Debug.Log("🍭 Creating Clean Sweet UI Scene...");
            
            // Clear existing scene
            ClearExistingScene();
            
            // Create clean main camera
            CreateCleanMainCamera();
            
            // Create Sweet UI Test Controller
            CreateSweetUITestController();
            
            // Create EightBitTheme
            CreateEightBitTheme();
            
            // Create UISceneManager
            CreateUISceneManager();
            
            UnityEngine.Debug.Log("🍭 Clean Sweet UI Scene Created!");
            UnityEngine.Debug.Log("🎮 Press Play to test your sweet UI!");
        }

        void ClearExistingScene()
        {
            // Find all GameObjects except the one with this script
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            
            foreach (GameObject obj in allObjects)
            {
                if (obj != gameObject && obj.name != "Main Camera")
                {
                    if (HasUnknownScript(obj))
                    {
                        DestroyImmediate(obj);
                    }
                }
            }
        }

        void CreateCleanMainCamera()
        {
            // Create or find main camera
            Camera mainCamera = Camera.main;
            if (mainCamera == null)
            {
                GameObject cameraGO = new GameObject("Main Camera");
                cameraGO.AddComponent<Camera>();
                cameraGO.AddComponent<AudioListener>();
                cameraGO.transform.position = new Vector3(0, 0, -10);
                cameraGO.tag = "MainCamera";
            }
        }

        void CreateSweetUITestController()
        {
            GameObject sweetUIController = new GameObject("Sweet UI Test Controller");
            sweetUIController.AddComponent<SweetUITest>();
            
            if (showFixLogs)
            {
                UnityEngine.Debug.Log("🍭 Sweet UI Test Controller created!");
            }
        }

        void CreateEightBitTheme()
        {
            if (EightBitTheme.Instance == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                themeGO.AddComponent<EightBitTheme>();
                
                if (showFixLogs)
                {
                    UnityEngine.Debug.Log("🍭 EightBitTheme created!");
                }
            }
        }

        void CreateUISceneManager()
        {
            GameObject uiSceneManagerGO = new GameObject("UISceneManager");
            uiSceneManagerGO.AddComponent<UISceneManager>();
            
            if (showFixLogs)
            {
                UnityEngine.Debug.Log("🍭 UISceneManager created!");
            }
        }

        [ContextMenu("Remove All Unknown Scripts")]
        public void RemoveAllUnknownScripts()
        {
            UnityEngine.Debug.Log("🍭 Removing all unknown scripts...");
            
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            int removedCount = 0;

            foreach (GameObject obj in allObjects)
            {
                if (HasUnknownScript(obj))
                {
                    // Remove all components with unknown scripts
                    Component[] components = obj.GetComponents<Component>();
                    foreach (Component comp in components)
                    {
                        if (comp == null)
                        {
                            DestroyImmediate(comp);
                            removedCount++;
                        }
                    }
                }
            }

            UnityEngine.Debug.Log($"🍭 Removed {removedCount} unknown script components!");
        }

        [ContextMenu("Validate All Scripts")]
        public void ValidateAllScripts()
        {
            UnityEngine.Debug.Log("🍭 Validating all scripts...");
            
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            int validCount = 0;
            int invalidCount = 0;

            foreach (GameObject obj in allObjects)
            {
                Component[] components = obj.GetComponents<Component>();
                bool hasInvalidScripts = false;
                
                foreach (Component comp in components)
                {
                    if (comp == null)
                    {
                        hasInvalidScripts = true;
                        break;
                    }
                }

                if (hasInvalidScripts)
                {
                    invalidCount++;
                    UnityEngine.Debug.LogWarning($"🍭 Invalid scripts found on: {obj.name}");
                }
                else
                {
                    validCount++;
                }
            }

            UnityEngine.Debug.Log($"🍭 Script Validation Complete!");
            UnityEngine.Debug.Log($"🍭 Valid GameObjects: {validCount}");
            UnityEngine.Debug.Log($"🍭 Invalid GameObjects: {invalidCount}");
        }
    }
}
