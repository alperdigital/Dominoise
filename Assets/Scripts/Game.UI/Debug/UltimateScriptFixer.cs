using UnityEngine;
using System.Collections.Generic;
using Game.UI.Themes;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    /// <summary>
    /// Ultimate Script Fixer - Tüm script hatalarını düzeltir
    /// </summary>
    public sealed class UltimateScriptFixer : MonoBehaviour
    {
        [Header("Ultimate Script Fixer")]
        [SerializeField] private bool autoFixOnStart = true;
        [SerializeField] private bool showFixLogs = true;
        [SerializeField] private bool createNewScene = true;

        void Start()
        {
            if (autoFixOnStart)
            {
                UltimateFix();
            }
        }

        [ContextMenu("Ultimate Fix - Fix Everything")]
        public void UltimateFix()
        {
            UnityEngine.Debug.Log("🍭 === ULTIMATE SCRIPT FIXER === 🍭");
            UnityEngine.Debug.Log("🔧 Starting ultimate fix process...");
            
            // Step 1: Remove all problematic GameObjects
            RemoveProblematicGameObjects();
            
            // Step 2: Create clean scene
            if (createNewScene)
            {
                CreateUltimateCleanScene();
            }
            
            // Step 3: Validate everything
            ValidateEverything();
            
            UnityEngine.Debug.Log("🍭 === ULTIMATE FIX COMPLETE === 🍭");
        }

        void RemoveProblematicGameObjects()
        {
            UnityEngine.Debug.Log("🍭 Removing problematic GameObjects...");
            
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            int removedCount = 0;

            foreach (GameObject obj in allObjects)
            {
                if (obj != gameObject && ShouldRemoveGameObject(obj))
                {
                    DestroyImmediate(obj);
                    removedCount++;
                    
                    if (showFixLogs)
                    {
                        UnityEngine.Debug.Log($"🍭 Removed: {obj.name}");
                    }
                }
            }

            UnityEngine.Debug.Log($"🍭 Removed {removedCount} problematic GameObjects");
        }

        bool ShouldRemoveGameObject(GameObject obj)
        {
            // Remove objects with unknown scripts
            Component[] components = obj.GetComponents<Component>();
            foreach (Component comp in components)
            {
                if (comp == null)
                {
                    return true;
                }
            }

            // Remove objects with missing scripts
            if (obj.name.Contains("(Missing Script)"))
            {
                return true;
            }

            // Remove objects with problematic names
            string[] problematicNames = {
                "Unknown",
                "Missing",
                "Broken",
                "Error"
            };

            foreach (string problematicName in problematicNames)
            {
                if (obj.name.Contains(problematicName))
                {
                    return true;
                }
            }

            return false;
        }

        void CreateUltimateCleanScene()
        {
            UnityEngine.Debug.Log("🍭 Creating ultimate clean scene...");
            
            // Create main camera
            CreateMainCamera();
            
            // Create Sweet UI Test Controller
            CreateSweetUITestController();
            
            // Create EightBitTheme
            CreateEightBitTheme();
            
            // Create UISceneManager
            CreateUISceneManager();
            
            UnityEngine.Debug.Log("🍭 Ultimate clean scene created!");
        }

        void CreateMainCamera()
        {
            if (Camera.main == null)
            {
                GameObject cameraGO = new GameObject("Main Camera");
                cameraGO.AddComponent<Camera>();
                cameraGO.AddComponent<AudioListener>();
                cameraGO.transform.position = new Vector3(0, 0, -10);
                cameraGO.tag = "MainCamera";
                
                if (showFixLogs)
                {
                    UnityEngine.Debug.Log("🍭 Main Camera created");
                }
            }
        }

        void CreateSweetUITestController()
        {
            GameObject sweetUIController = new GameObject("Sweet UI Test Controller");
            sweetUIController.AddComponent<SweetUITest>();
            
            if (showFixLogs)
            {
                UnityEngine.Debug.Log("🍭 Sweet UI Test Controller created");
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
                    UnityEngine.Debug.Log("🍭 EightBitTheme created");
                }
            }
        }

        void CreateUISceneManager()
        {
            GameObject uiSceneManagerGO = new GameObject("UISceneManager");
            uiSceneManagerGO.AddComponent<UISceneManager>();
            
            if (showFixLogs)
            {
                UnityEngine.Debug.Log("🍭 UISceneManager created");
            }
        }

        void ValidateEverything()
        {
            UnityEngine.Debug.Log("🍭 Validating everything...");
            
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            int validCount = 0;
            int invalidCount = 0;

            foreach (GameObject obj in allObjects)
            {
                if (IsGameObjectValid(obj))
                {
                    validCount++;
                }
                else
                {
                    invalidCount++;
                    UnityEngine.Debug.LogWarning($"🍭 Invalid GameObject: {obj.name}");
                }
            }

            UnityEngine.Debug.Log($"🍭 Validation Complete!");
            UnityEngine.Debug.Log($"🍭 Valid GameObjects: {validCount}");
            UnityEngine.Debug.Log($"🍭 Invalid GameObjects: {invalidCount}");
        }

        bool IsGameObjectValid(GameObject obj)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component comp in components)
            {
                if (comp == null)
                {
                    return false;
                }
            }
            return true;
        }

        [ContextMenu("Emergency Clean Scene")]
        public void EmergencyCleanScene()
        {
            UnityEngine.Debug.Log("🍭 === EMERGENCY CLEAN SCENE === 🍭");
            
            // Remove everything except this script
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            
            foreach (GameObject obj in allObjects)
            {
                if (obj != gameObject)
                {
                    DestroyImmediate(obj);
                }
            }
            
            // Create clean scene
            CreateUltimateCleanScene();
            
            UnityEngine.Debug.Log("🍭 Emergency clean scene complete!");
        }

        [ContextMenu("Show Scene Status")]
        public void ShowSceneStatus()
        {
            UnityEngine.Debug.Log("🍭 === SCENE STATUS === 🍭");
            
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            
            foreach (GameObject obj in allObjects)
            {
                string status = IsGameObjectValid(obj) ? "✅ Valid" : "❌ Invalid";
                UnityEngine.Debug.Log($"🍭 {obj.name}: {status}");
            }
            
            UnityEngine.Debug.Log("🍭 === END SCENE STATUS === 🍭");
        }
    }
}
