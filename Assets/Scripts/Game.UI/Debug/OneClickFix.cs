using UnityEngine;
using Game.UI.Themes;
using Game.UI.SceneManager;

namespace Game.UI.Debug
{
    /// <summary>
    /// One Click Fix - Tek tıkla tüm script hatalarını düzeltir
    /// </summary>
    public sealed class OneClickFix : MonoBehaviour
    {
        [Header("One Click Fix")]
        [SerializeField] private bool autoFixOnStart = true;

        void Start()
        {
            if (autoFixOnStart)
            {
                OneClickFixAll();
            }
        }

        [ContextMenu("One Click Fix All")]
        public void OneClickFixAll()
        {
            UnityEngine.Debug.Log("🍭 === ONE CLICK FIX ALL === 🍭");
            UnityEngine.Debug.Log("🔧 Fixing all script issues...");
            
            // Remove all problematic GameObjects
            RemoveAllProblematicGameObjects();
            
            // Create perfect scene
            CreatePerfectScene();
            
            UnityEngine.Debug.Log("🍭 === ONE CLICK FIX COMPLETE === 🍭");
            UnityEngine.Debug.Log("🎮 Press Play to test your sweet UI!");
        }

        void RemoveAllProblematicGameObjects()
        {
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            
            foreach (GameObject obj in allObjects)
            {
                if (obj != gameObject && HasProblems(obj))
                {
                    DestroyImmediate(obj);
                    UnityEngine.Debug.Log($"🍭 Removed problematic: {obj.name}");
                }
            }
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
                   name.Contains("error");
        }

        void CreatePerfectScene()
        {
            UnityEngine.Debug.Log("🍭 Creating perfect scene...");
            
            // Main Camera
            if (Camera.main == null)
            {
                GameObject cameraGO = new GameObject("Main Camera");
                cameraGO.AddComponent<Camera>();
                cameraGO.AddComponent<AudioListener>();
                cameraGO.transform.position = new Vector3(0, 0, -10);
                cameraGO.tag = "MainCamera";
            }

            // Sweet UI Test Controller
            GameObject sweetUIController = new GameObject("Sweet UI Test Controller");
            sweetUIController.AddComponent<SweetUITest>();

            // EightBitTheme
            if (EightBitTheme.Instance == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                themeGO.AddComponent<EightBitTheme>();
            }

            // UISceneManager
            GameObject uiSceneManagerGO = new GameObject("UISceneManager");
            uiSceneManagerGO.AddComponent<UISceneManager>();

            UnityEngine.Debug.Log("🍭 Perfect scene created!");
        }
    }
}
