using UnityEngine;
using Game.UI.Themes;

namespace Game.UI.Debug
{
    /// <summary>
    /// Sweet UI Setup Guide - Unity'de eksik script referanslarını düzeltmek için
    /// </summary>
    public sealed class SweetUISetupGuide : MonoBehaviour
    {
        [Header("Sweet UI Setup Guide")]
        [SerializeField] private bool showSetupGuide = true;
        [SerializeField] private bool autoFixMissingScripts = true;

        void Start()
        {
            if (showSetupGuide)
            {
                ShowSetupGuide();
            }

            if (autoFixMissingScripts)
            {
                FixMissingScripts();
            }
        }

        void ShowSetupGuide()
        {
            UnityEngine.Debug.Log("🍭 === SWEET UI SETUP GUIDE === 🍭");
            UnityEngine.Debug.Log("📋 Step 1: Create Empty GameObject");
            UnityEngine.Debug.Log("📋 Step 2: Add SweetUITest component");
            UnityEngine.Debug.Log("📋 Step 3: Press Play to test sweet UI");
            UnityEngine.Debug.Log("📋 Step 4: Use keyboard keys: 1, 2, 3, 0");
            UnityEngine.Debug.Log("🍭 ================================ 🍭");
        }

        void FixMissingScripts()
        {
            // Check if EightBitTheme exists
            if (EightBitTheme.Instance == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                themeGO.AddComponent<EightBitTheme>();
                UnityEngine.Debug.Log("🍭 EightBitTheme created!");
            }

            // Check if SweetUITest exists
            if (GetComponent<SweetUITest>() == null)
            {
                gameObject.AddComponent<SweetUITest>();
                UnityEngine.Debug.Log("🍭 SweetUITest component added!");
            }

            UnityEngine.Debug.Log("🍭 Sweet UI Setup Complete!");
        }
    }
}
