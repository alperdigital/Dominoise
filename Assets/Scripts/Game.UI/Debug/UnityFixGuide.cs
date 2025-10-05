using UnityEngine;
using Game.UI.Themes;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    /// <summary>
    /// Unity Fix Guide - Unity'de unknown script hatalarını düzeltme rehberi
    /// </summary>
    public sealed class UnityFixGuide : MonoBehaviour
    {
        [Header("Unity Fix Guide")]
        [SerializeField] private bool showGuideOnStart = true;
        [SerializeField] private bool autoCreateCleanScene = true;

        void Start()
        {
            if (showGuideOnStart)
            {
                ShowUnityFixGuide();
            }

            if (autoCreateCleanScene)
            {
                CreateCleanScene();
            }
        }

        void ShowUnityFixGuide()
        {
            UnityEngine.Debug.Log("🚨 === UNITY FIX GUIDE === 🚨");
            UnityEngine.Debug.Log("📋 Step 1: STOP PLAY MODE (if playing)");
            UnityEngine.Debug.Log("📋 Step 2: File > New Scene");
            UnityEngine.Debug.Log("📋 Step 3: Delete all GameObjects with unknown scripts");
            UnityEngine.Debug.Log("📋 Step 4: Add UnityFixGuide component");
            UnityEngine.Debug.Log("📋 Step 5: Press Play to test");
            UnityEngine.Debug.Log("🚨 ======================== 🚨");
        }

        [ContextMenu("Create Clean Scene")]
        public void CreateCleanScene()
        {
            UnityEngine.Debug.Log("🚨 Creating clean scene...");
            
            // Remove all existing GameObjects except this one
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            
            foreach (GameObject obj in allObjects)
            {
                if (obj != gameObject)
                {
                    DestroyImmediate(obj);
                }
            }
            
            // Create clean scene
            CreateMainCamera();
            CreateSweetUITestController();
            CreateEightBitTheme();
            CreateUISceneManager();
            
            UnityEngine.Debug.Log("🚨 Clean scene created!");
        }

        void CreateMainCamera()
        {
            GameObject cameraGO = new GameObject("Main Camera");
            cameraGO.AddComponent<Camera>();
            cameraGO.AddComponent<AudioListener>();
            cameraGO.transform.position = new Vector3(0, 0, -10);
            cameraGO.tag = "MainCamera";
        }

        void CreateSweetUITestController()
        {
            GameObject sweetUIController = new GameObject("Sweet UI Test Controller");
            sweetUIController.AddComponent<SweetUITest>();
        }

        void CreateEightBitTheme()
        {
            GameObject themeGO = new GameObject("EightBitTheme");
            themeGO.AddComponent<EightBitTheme>();
        }

        void CreateUISceneManager()
        {
            GameObject uiSceneManagerGO = new GameObject("UISceneManager");
            uiSceneManagerGO.AddComponent<UISceneManager>();
        }

        [ContextMenu("Show Current Scene Status")]
        public void ShowCurrentSceneStatus()
        {
            UnityEngine.Debug.Log("🚨 === CURRENT SCENE STATUS === 🚨");
            
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            
            foreach (GameObject obj in allObjects)
            {
                string status = HasProblems(obj) ? "❌ PROBLEMATIC" : "✅ CLEAN";
                UnityEngine.Debug.Log($"🚨 {obj.name}: {status}");
            }
            
            UnityEngine.Debug.Log("🚨 === END SCENE STATUS === 🚨");
        }

        bool HasProblems(GameObject obj)
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

        [ContextMenu("Emergency Clean All")]
        public void EmergencyCleanAll()
        {
            UnityEngine.Debug.Log("🚨 === EMERGENCY CLEAN ALL === 🚨");
            
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
            CreateCleanScene();
            
            UnityEngine.Debug.Log("🚨 Emergency clean complete!");
        }
    }
}
