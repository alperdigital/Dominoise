using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.UI.Themes;

namespace Game.UI.Prefabs
{
    /// <summary>
    /// 8-bit temalı, tatlı ve Apple oyunları tarzında Ana Menü UI
    /// </summary>
    public sealed class SweetMainMenuUI : MonoBehaviour
    {
        [Header("Sweet UI References")]
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text subtitleText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image logoImage;

        private EightBitTheme theme;

        public static GameObject CreateSweetMainMenuCanvas()
        {
            GameObject canvasGO = new GameObject("SweetMainMenuCanvas");
            
            // Canvas setup
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
            GraphicRaycaster raycaster = canvasGO.AddComponent<GraphicRaycaster>();
            
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            // Sweet background
            CreateSweetBackground(canvasGO);
            
            // Sweet title
            CreateSweetTitle(canvasGO);
            
            // Sweet buttons
            CreateSweetButtons(canvasGO);
            
            // Sweet decorations
            CreateSweetDecorations(canvasGO);
            
            return canvasGO;
        }

        static void CreateSweetBackground(GameObject canvas)
        {
            GameObject bgGO = new GameObject("SweetBackground");
            bgGO.transform.SetParent(canvas.transform, false);
            
            RectTransform bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            
            Image bgImage = bgGO.AddComponent<Image>();
            bgImage.color = new Color(0.1f, 0.2f, 0.4f, 1f); // Ocean blue background
            
            // Sweet gradient effect
            SweetGradientEffect gradient = bgGO.AddComponent<SweetGradientEffect>();
            gradient.Initialize(
                new Color(0.1f, 0.2f, 0.4f, 1f), // Top
                new Color(0.2f, 0.1f, 0.3f, 1f)  // Bottom
            );
        }

        static void CreateSweetTitle(GameObject canvas)
        {
            // Main title
            GameObject titleGO = new GameObject("SweetTitle");
            titleGO.transform.SetParent(canvas.transform, false);
            
            RectTransform titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.8f);
            titleRect.anchorMax = new Vector2(0.5f, 0.8f);
            titleRect.sizeDelta = new Vector2(600, 120);
            titleRect.anchoredPosition = Vector2.zero;
            
            TMP_Text titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "🍭 DOMINOISE 🍭";
            titleText.fontSize = 64;
            titleText.color = new Color(1f, 0.4f, 0.8f, 1f); // Sweet pink
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.fontStyle = FontStyles.Bold;
            
            // Sweet title shadow
            CreateTextShadow(titleGO, new Vector2(4, -4), new Color(0f, 0f, 0f, 0.5f));
            
            // Sweet title animation
            SweetTitleAnimator titleAnim = titleGO.AddComponent<SweetTitleAnimator>();
            titleAnim.Initialize();
            
            // Subtitle
            GameObject subtitleGO = new GameObject("SweetSubtitle");
            subtitleGO.transform.SetParent(canvas.transform, false);
            
            RectTransform subtitleRect = subtitleGO.AddComponent<RectTransform>();
            subtitleRect.anchorMin = new Vector2(0.5f, 0.7f);
            subtitleRect.anchorMax = new Vector2(0.5f, 0.7f);
            subtitleRect.sizeDelta = new Vector2(500, 60);
            subtitleRect.anchoredPosition = Vector2.zero;
            
            TMP_Text subtitleText = subtitleGO.AddComponent<TextMeshProUGUI>();
            subtitleText.text = "✨ Sweet 8-bit Adventure ✨";
            subtitleText.fontSize = 28;
            subtitleText.color = new Color(1f, 0.9f, 0.2f, 1f); // Sunshine yellow
            subtitleText.alignment = TextAlignmentOptions.Center;
            subtitleText.fontStyle = FontStyles.Italic;
            
            // Subtitle shadow
            CreateTextShadow(subtitleGO, new Vector2(2, -2), new Color(0f, 0f, 0f, 0.3f));
        }

        static void CreateSweetButtons(GameObject canvas)
        {
            EightBitTheme theme = EightBitTheme.Instance;
            if (theme == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                theme = themeGO.AddComponent<EightBitTheme>();
            }
            
            // Play Button
            GameObject playBtn = theme.CreateSweetButton("🎮 PLAY", new Vector2(0, 100), new Vector2(300, 80), () => {
                UnityEngine.Debug.Log("🎮 Sweet Play Button Clicked!");
            });
            playBtn.transform.SetParent(canvas.transform, false);
            
            // Settings Button
            GameObject settingsBtn = theme.CreateSweetButton("⚙️ SETTINGS", new Vector2(0, 0), new Vector2(300, 80), () => {
                UnityEngine.Debug.Log("⚙️ Sweet Settings Button Clicked!");
            });
            settingsBtn.transform.SetParent(canvas.transform, false);
            
            // Shop Button
            GameObject shopBtn = theme.CreateSweetButton("🛍️ SHOP", new Vector2(0, -100), new Vector2(300, 80), () => {
                UnityEngine.Debug.Log("🛍️ Sweet Shop Button Clicked!");
            });
            shopBtn.transform.SetParent(canvas.transform, false);
        }

        static void CreateSweetDecorations(GameObject canvas)
        {
            // Sweet stars decoration
            for (int i = 0; i < 20; i++)
            {
                CreateSweetStar(canvas, new Vector2(
                    Random.Range(-400, 400),
                    Random.Range(-800, 800)
                ));
            }
            
            // Sweet hearts decoration
            for (int i = 0; i < 10; i++)
            {
                CreateSweetHeart(canvas, new Vector2(
                    Random.Range(-500, 500),
                    Random.Range(-900, 900)
                ));
            }
        }

        static void CreateSweetStar(GameObject canvas, Vector2 position)
        {
            GameObject starGO = new GameObject("SweetStar");
            starGO.transform.SetParent(canvas.transform, false);
            
            RectTransform starRect = starGO.AddComponent<RectTransform>();
            starRect.anchoredPosition = position;
            starRect.sizeDelta = new Vector2(20, 20);
            
            Image starImage = starGO.AddComponent<Image>();
            starImage.color = new Color(1f, 1f, 0f, 0.8f); // Golden yellow
            
            // Sweet star animation
            SweetStarAnimator starAnim = starGO.AddComponent<SweetStarAnimator>();
            starAnim.Initialize();
        }

        static void CreateSweetHeart(GameObject canvas, Vector2 position)
        {
            GameObject heartGO = new GameObject("SweetHeart");
            heartGO.transform.SetParent(canvas.transform, false);
            
            RectTransform heartRect = heartGO.AddComponent<RectTransform>();
            heartRect.anchoredPosition = position;
            heartRect.sizeDelta = new Vector2(30, 30);
            
            Image heartImage = heartGO.AddComponent<Image>();
            heartImage.color = new Color(1f, 0.4f, 0.8f, 0.6f); // Sweet pink
            
            // Sweet heart animation
            SweetHeartAnimator heartAnim = heartGO.AddComponent<SweetHeartAnimator>();
            heartAnim.Initialize();
        }

        static void CreateTextShadow(GameObject parent, Vector2 offset, Color shadowColor)
        {
            GameObject shadowGO = new GameObject("TextShadow");
            shadowGO.transform.SetParent(parent.transform, false);
            
            RectTransform shadowRect = shadowGO.AddComponent<RectTransform>();
            shadowRect.anchorMin = Vector2.zero;
            shadowRect.anchorMax = Vector2.one;
            shadowRect.offsetMin = Vector2.zero;
            shadowRect.offsetMax = Vector2.zero;
            shadowRect.anchoredPosition = offset;
            
            TMP_Text shadowText = shadowGO.AddComponent<TextMeshProUGUI>();
            shadowText.text = parent.GetComponent<TMP_Text>().text;
            shadowText.fontSize = parent.GetComponent<TMP_Text>().fontSize;
            shadowText.color = shadowColor;
            shadowText.alignment = parent.GetComponent<TMP_Text>().alignment;
            shadowText.fontStyle = parent.GetComponent<TMP_Text>().fontStyle;
            
            shadowGO.transform.SetAsFirstSibling();
        }
    }

    /// <summary>
    /// Tatlı gradient efekti
    /// </summary>
    public sealed class SweetGradientEffect : MonoBehaviour
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
            // Sweet gradient animation
            float time = Time.time * 0.5f;
            Color currentColor = Color.Lerp(topColor, bottomColor, Mathf.Sin(time) * 0.5f + 0.5f);
            image.color = currentColor;
        }
    }

    /// <summary>
    /// Tatlı başlık animasyonu
    /// </summary>
    public sealed class SweetTitleAnimator : MonoBehaviour
    {
        private Vector3 originalScale;
        private float animationSpeed = 2f;

        public void Initialize()
        {
            originalScale = transform.localScale;
        }

        void Update()
        {
            // Sweet bounce animation
            float bounce = Mathf.Sin(Time.time * animationSpeed) * 0.1f;
            transform.localScale = originalScale * (1f + bounce);
        }
    }

    /// <summary>
    /// Tatlı yıldız animasyonu
    /// </summary>
    public sealed class SweetStarAnimator : MonoBehaviour
    {
        private float rotationSpeed;
        private float scaleSpeed;

        public void Initialize()
        {
            rotationSpeed = Random.Range(30f, 60f);
            scaleSpeed = Random.Range(1f, 3f);
        }

        void Update()
        {
            // Sweet rotation
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            
            // Sweet scale animation
            float scale = Mathf.Sin(Time.time * scaleSpeed) * 0.3f + 0.7f;
            transform.localScale = Vector3.one * scale;
        }
    }

    /// <summary>
    /// Tatlı kalp animasyonu
    /// </summary>
    public sealed class SweetHeartAnimator : MonoBehaviour
    {
        private float floatSpeed;
        private float scaleSpeed;
        private Vector3 originalPosition;

        public void Initialize()
        {
            floatSpeed = Random.Range(0.5f, 2f);
            scaleSpeed = Random.Range(1f, 2f);
            originalPosition = transform.localPosition;
        }

        void Update()
        {
            // Sweet floating animation
            float floatY = Mathf.Sin(Time.time * floatSpeed) * 10f;
            transform.localPosition = originalPosition + new Vector3(0, floatY, 0);
            
            // Sweet scale animation
            float scale = Mathf.Sin(Time.time * scaleSpeed) * 0.2f + 0.8f;
            transform.localScale = Vector3.one * scale;
        }
    }
}
