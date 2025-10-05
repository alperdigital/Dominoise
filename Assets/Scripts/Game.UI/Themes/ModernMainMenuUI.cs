using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.UI.Themes;

namespace Game.UI.Prefabs
{
    /// <summary>
    /// Modern Main Menu UI - Apple tarzı sade, minimal tasarım
    /// </summary>
    public sealed class ModernMainMenuUI : MonoBehaviour
    {
        [Header("Modern UI References")]
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text subtitleText;
        [SerializeField] private Image backgroundImage;

        private ModernMinimalTheme theme;

        public static GameObject CreateModernMainMenuCanvas()
        {
            GameObject canvasGO = new GameObject("ModernMainMenuCanvas");
            
            // Canvas setup
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
            GraphicRaycaster raycaster = canvasGO.AddComponent<GraphicRaycaster>();
            
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            // Modern background
            CreateModernBackground(canvasGO);
            
            // Modern title
            CreateModernTitle(canvasGO);
            
            // Modern buttons
            CreateModernButtons(canvasGO);
            
            return canvasGO;
        }

        static void CreateModernBackground(GameObject canvas)
        {
            GameObject bgGO = new GameObject("ModernBackground");
            bgGO.transform.SetParent(canvas.transform, false);
            
            RectTransform bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            
            Image bgImage = bgGO.AddComponent<Image>();
            bgImage.color = new Color(0.05f, 0.05f, 0.05f, 1f); // Dark background
            
            // Subtle gradient effect
            ModernGradientEffect gradient = bgGO.AddComponent<ModernGradientEffect>();
            gradient.Initialize(
                new Color(0.05f, 0.05f, 0.05f, 1f), // Top
                new Color(0.1f, 0.1f, 0.1f, 1f)     // Bottom
            );
        }

        static void CreateModernTitle(GameObject canvas)
        {
            // Main title
            GameObject titleGO = new GameObject("ModernTitle");
            titleGO.transform.SetParent(canvas.transform, false);
            
            RectTransform titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.7f);
            titleRect.anchorMax = new Vector2(0.5f, 0.7f);
            titleRect.sizeDelta = new Vector2(600, 120);
            titleRect.anchoredPosition = Vector2.zero;
            
            TMP_Text titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "DOMINOISE";
            titleText.fontSize = 48;
            titleText.color = new Color(0.95f, 0.95f, 0.95f, 1f); // Light text
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.fontStyle = FontStyles.Bold;
            
            // Subtitle
            GameObject subtitleGO = new GameObject("ModernSubtitle");
            subtitleGO.transform.SetParent(canvas.transform, false);
            
            RectTransform subtitleRect = subtitleGO.AddComponent<RectTransform>();
            subtitleRect.anchorMin = new Vector2(0.5f, 0.6f);
            subtitleRect.anchorMax = new Vector2(0.5f, 0.6f);
            subtitleRect.sizeDelta = new Vector2(500, 60);
            subtitleRect.anchoredPosition = Vector2.zero;
            
            TMP_Text subtitleText = subtitleGO.AddComponent<TextMeshProUGUI>();
            subtitleText.text = "Modern Domino Game";
            subtitleText.fontSize = 18;
            subtitleText.color = new Color(0.6f, 0.6f, 0.6f, 1f); // Gray text
            subtitleText.alignment = TextAlignmentOptions.Center;
            subtitleText.fontStyle = FontStyles.Normal;
        }

        static void CreateModernButtons(GameObject canvas)
        {
            ModernMinimalTheme theme = ModernMinimalTheme.Instance;
            if (theme == null)
            {
                GameObject themeGO = new GameObject("ModernMinimalTheme");
                theme = themeGO.AddComponent<ModernMinimalTheme>();
            }
            
            // Play Button
            GameObject playBtn = theme.CreateModernButton("PLAY", new Vector2(0, 100), new Vector2(200, 50), () => {
                UnityEngine.Debug.Log("🎮 Modern Play Button Clicked!");
            });
            playBtn.transform.SetParent(canvas.transform, false);
            
            // Settings Button
            GameObject settingsBtn = theme.CreateModernButton("SETTINGS", new Vector2(0, 30), new Vector2(200, 50), () => {
                UnityEngine.Debug.Log("⚙️ Modern Settings Button Clicked!");
            });
            settingsBtn.transform.SetParent(canvas.transform, false);
            
            // Quit Button
            GameObject quitBtn = theme.CreateModernButton("QUIT", new Vector2(0, -40), new Vector2(200, 50), () => {
                UnityEngine.Debug.Log("🚪 Modern Quit Button Clicked!");
            });
            quitBtn.transform.SetParent(canvas.transform, false);
        }
    }

    /// <summary>
    /// Modern gradient efekti
    /// </summary>
    public sealed class ModernGradientEffect : MonoBehaviour
    {
        private Color topColor;
        private Color bottomColor;
        private Image image;

        public void Initialize(Color top, Color bottom)
        {
            topColor = top;
            bottomColor = bottom;
            image = GetComponent<Image>();
        }

        void Update()
        {
            // Subtle gradient animation
            float time = Time.time * 0.2f;
            Color currentColor = Color.Lerp(topColor, bottomColor, Mathf.Sin(time) * 0.1f + 0.5f);
            image.color = currentColor;
        }
    }
}
