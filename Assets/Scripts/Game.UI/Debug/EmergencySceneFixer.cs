using UnityEngine;
using Game.UI.Themes;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    /// <summary>
    /// Emergency Scene Fixer - Acil durum scene düzeltme
    /// </summary>
    public sealed class EmergencySceneFixer : MonoBehaviour
    {
        [Header("Emergency Scene Fixer")]
        [SerializeField] private bool autoFixOnStart = true;
        [SerializeField] private bool showInstructions = true;

        void Start()
        {
            if (showInstructions)
            {
                ShowEmergencyInstructions();
            }

            if (autoFixOnStart)
            {
                EmergencyFix();
            }
        }

        void ShowEmergencyInstructions()
        {
            UnityEngine.Debug.Log("🚨 === EMERGENCY SCENE FIXER === 🚨");
            UnityEngine.Debug.Log("📋 Step 1: STOP PLAY MODE (if playing)");
            UnityEngine.Debug.Log("📋 Step 2: File > New Scene");
            UnityEngine.Debug.Log("📋 Step 3: Delete all GameObjects with unknown scripts");
            UnityEngine.Debug.Log("📋 Step 4: Add EmergencySceneFixer component");
            UnityEngine.Debug.Log("📋 Step 5: Press Play to test");
            UnityEngine.Debug.Log("🚨 ================================ 🚨");
        }

        [ContextMenu("Emergency Fix - Stop Play Mode First")]
        public void EmergencyFix()
        {
            UnityEngine.Debug.Log("🚨 === EMERGENCY FIX STARTED === 🚨");
            
            // Check if in play mode
            if (Application.isPlaying)
            {
                UnityEngine.Debug.Log("🚨 WARNING: You are in PLAY MODE!");
                UnityEngine.Debug.Log("🚨 Please STOP PLAY MODE first!");
                UnityEngine.Debug.Log("🚨 Then run this fix again!");
                return;
            }

            // Remove all problematic GameObjects
            RemoveAllProblematicGameObjects();
            
            // Create clean scene
            CreateCleanScene();
            
            UnityEngine.Debug.Log("🚨 === EMERGENCY FIX COMPLETE === 🚨");
        }

        void RemoveAllProblematicGameObjects()
        {
            UnityEngine.Debug.Log("🚨 Removing all problematic GameObjects...");
            
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            int removedCount = 0;

            foreach (GameObject obj in allObjects)
            {
                if (obj != gameObject && HasProblems(obj))
                {
                    DestroyImmediate(obj);
                    removedCount++;
                    UnityEngine.Debug.Log($"🚨 Removed: {obj.name}");
                }
            }

            UnityEngine.Debug.Log($"🚨 Removed {removedCount} problematic GameObjects");
        }

        bool HasProblems(GameObject obj)
        {
            // Check for unknown scripts
            Component[] components = obj.GetComponents<Component>();
            foreach (Component comp in components)
            {
                if (comp == null)
                {
                    return true;
                }
            }

            // Check for problematic names
            string name = obj.name.ToLower();
            return name.Contains("unknown") || 
                   name.Contains("missing") || 
                   name.Contains("broken") || 
                   name.Contains("error") ||
                   name.Contains("(missing script)");
        }

        void CreateCleanScene()
        {
            UnityEngine.Debug.Log("🚨 Creating clean scene...");
            
            // Create main camera
            CreateMainCamera();
            
            // Create Sweet UI Test Controller
            CreateSweetUITestController();
            
            // Create EightBitTheme
            CreateEightBitTheme();
            
            // Create UISceneManager
            CreateUISceneManager();
            
            UnityEngine.Debug.Log("🚨 Clean scene created!");
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
                UnityEngine.Debug.Log("🚨 Main Camera created");
            }
        }

        void CreateSweetUITestController()
        {
            GameObject sweetUIController = new GameObject("Sweet UI Test Controller");
            sweetUIController.AddComponent<SweetUITest>();
            UnityEngine.Debug.Log("🚨 Sweet UI Test Controller created");
        }

        void CreateEightBitTheme()
        {
            if (EightBitTheme.Instance == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                themeGO.AddComponent<EightBitTheme>();
                UnityEngine.Debug.Log("🚨 EightBitTheme created");
            }
        }

        void CreateUISceneManager()
        {
            GameObject uiSceneManagerGO = new GameObject("UISceneManager");
            uiSceneManagerGO.AddComponent<UISceneManager>();
            UnityEngine.Debug.Log("🚨 UISceneManager created");
        }

        [ContextMenu("Force Stop Play Mode")]
        public void ForceStopPlayMode()
        {
            if (Application.isPlaying)
            {
                UnityEngine.Debug.Log("🚨 FORCE STOPPING PLAY MODE...");
                // This will be handled by Unity's stop button
                UnityEngine.Debug.Log("🚨 Please click the STOP button in Unity!");
            }
            else
            {
                UnityEngine.Debug.Log("🚨 Play mode is already stopped!");
            }
        }

        [ContextMenu("Create New Scene")]
        public void CreateNewScene()
        {
            UnityEngine.Debug.Log("🚨 Creating new scene...");
            UnityEngine.Debug.Log("🚨 Please use File > New Scene in Unity!");
            UnityEngine.Debug.Log("🚨 Then add this script to a new GameObject!");
        }
    }
}
