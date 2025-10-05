using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.UI.Themes;

namespace Game.UI.Prefabs
{
    /// <summary>
    /// 8-bit temalı, tatlı ve Apple oyunları tarzında Oyun UI
    /// </summary>
    public sealed class SweetGameUI : MonoBehaviour
    {
        [Header("Sweet Game UI References")]
        [SerializeField] private GameObject gameCanvas;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_Text livesText;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Image healthBar;
        [SerializeField] private Image energyBar;

        private EightBitTheme theme;

        public static GameObject CreateSweetGameCanvas()
        {
            GameObject canvasGO = new GameObject("SweetGameCanvas");
            
            // Canvas setup
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
            GraphicRaycaster raycaster = canvasGO.AddComponent<GraphicRaycaster>();
            
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            // Sweet HUD
            CreateSweetHUD(canvasGO);
            
            // Sweet controls
            CreateSweetControls(canvasGO);
            
            // Sweet power-ups
            CreateSweetPowerUps(canvasGO);
            
            return canvasGO;
        }

        static void CreateSweetHUD(GameObject canvas)
        {
            EightBitTheme theme = EightBitTheme.Instance;
            if (theme == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                theme = themeGO.AddComponent<EightBitTheme>();
            }
            
            // Sweet score panel
            GameObject scorePanel = theme.CreateSweetPanel("ScorePanel", new Vector2(-400, 800), new Vector2(300, 100), 
                new Color(0.2f, 0.2f, 0.4f, 0.8f));
            scorePanel.transform.SetParent(canvas.transform, false);
            
            // Score text
            TMP_Text scoreText = theme.CreateSweetText("SCORE: 0", new Vector2(0, 0), new Vector2(250, 60), 
                new Color(1f, 0.9f, 0.2f, 1f), 28);
            scoreText.transform.SetParent(scorePanel.transform, false);
            
            // Sweet time panel
            GameObject timePanel = theme.CreateSweetPanel("TimePanel", new Vector2(0, 800), new Vector2(200, 100), 
                new Color(0.2f, 0.4f, 0.2f, 0.8f));
            timePanel.transform.SetParent(canvas.transform, false);
            
            // Time text
            TMP_Text timeText = theme.CreateSweetText("⏰ 60", new Vector2(0, 0), new Vector2(150, 60), 
                new Color(0.2f, 1f, 0.4f, 1f), 32);
            timeText.transform.SetParent(timePanel.transform, false);
            
            // Sweet lives panel
            GameObject livesPanel = theme.CreateSweetPanel("LivesPanel", new Vector2(400, 800), new Vector2(200, 100), 
                new Color(0.4f, 0.2f, 0.2f, 0.8f));
            livesPanel.transform.SetParent(canvas.transform, false);
            
            // Lives text
            TMP_Text livesText = theme.CreateSweetText("❤️ 3", new Vector2(0, 0), new Vector2(150, 60), 
                new Color(1f, 0.2f, 0.3f, 1f), 32);
            livesText.transform.SetParent(livesPanel.transform, false);
        }

        static void CreateSweetControls(GameObject canvas)
        {
            EightBitTheme theme = EightBitTheme.Instance;
            if (theme == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                theme = themeGO.AddComponent<EightBitTheme>();
            }
            
            // Sweet pause button
            GameObject pauseBtn = theme.CreateSweetButton("⏸️ PAUSE", new Vector2(400, -800), new Vector2(150, 60), () => {
                UnityEngine.Debug.Log("⏸️ Sweet Pause Button Clicked!");
            });
            pauseBtn.transform.SetParent(canvas.transform, false);
            
            // Sweet restart button
            GameObject restartBtn = theme.CreateSweetButton("🔄 RESTART", new Vector2(200, -800), new Vector2(150, 60), () => {
                UnityEngine.Debug.Log("🔄 Sweet Restart Button Clicked!");
            });
            restartBtn.transform.SetParent(canvas.transform, false);
            
            // Sweet menu button
            GameObject menuBtn = theme.CreateSweetButton("🏠 MENU", new Vector2(0, -800), new Vector2(150, 60), () => {
                UnityEngine.Debug.Log("🏠 Sweet Menu Button Clicked!");
            });
            menuBtn.transform.SetParent(canvas.transform, false);
        }

        static void CreateSweetPowerUps(GameObject canvas)
        {
            EightBitTheme theme = EightBitTheme.Instance;
            if (theme == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                theme = themeGO.AddComponent<EightBitTheme>();
            }
            
            // Sweet power-up panel
            GameObject powerUpPanel = theme.CreateSweetPanel("PowerUpPanel", new Vector2(-400, -800), new Vector2(300, 100), 
                new Color(0.4f, 0.2f, 0.4f, 0.8f));
            powerUpPanel.transform.SetParent(canvas.transform, false);
            
            // Power-up buttons
            CreateSweetPowerUpButton(powerUpPanel, "⚡ SPEED", new Vector2(-80, 0), new Vector2(120, 60), 
                new Color(1f, 0.9f, 0.2f, 1f));
            CreateSweetPowerUpButton(powerUpPanel, "🛡️ SHIELD", new Vector2(80, 0), new Vector2(120, 60), 
                new Color(0.2f, 1f, 0.4f, 1f));
        }

        static void CreateSweetPowerUpButton(GameObject parent, string text, Vector2 position, Vector2 size, Color color)
        {
            EightBitTheme theme = EightBitTheme.Instance;
            if (theme == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                theme = themeGO.AddComponent<EightBitTheme>();
            }
            
            GameObject buttonGO = theme.CreateSweetButton(text, position, size, () => {
                UnityEngine.Debug.Log($"🍭 Sweet Power-up: {text} Clicked!");
            });
            buttonGO.transform.SetParent(parent.transform, false);
            
            // Sweet power-up animation
            SweetPowerUpAnimator powerUpAnim = buttonGO.AddComponent<SweetPowerUpAnimator>();
            powerUpAnim.Initialize();
        }
    }

    /// <summary>
    /// Tatlı power-up animasyonu
    /// </summary>
    public sealed class SweetPowerUpAnimator : MonoBehaviour
    {
        private float glowSpeed = 2f;
        private float scaleSpeed = 1.5f;
        private Vector3 originalScale;
        private Color originalColor;
        private Image buttonImage;

        public void Initialize()
        {
            originalScale = transform.localScale;
            buttonImage = GetComponent<Image>();
            originalColor = buttonImage.color;
        }

        void Update()
        {
            // Sweet glow effect
            float glow = Mathf.Sin(Time.time * glowSpeed) * 0.3f + 0.7f;
            buttonImage.color = Color.Lerp(originalColor, Color.white, glow);
            
            // Sweet scale animation
            float scale = Mathf.Sin(Time.time * scaleSpeed) * 0.1f + 0.9f;
            transform.localScale = originalScale * scale;
        }
    }
}
