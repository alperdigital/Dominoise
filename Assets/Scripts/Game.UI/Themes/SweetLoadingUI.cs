using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.UI.Themes;
using System.Collections;

namespace Game.UI.Prefabs
{
    /// <summary>
    /// 8-bit temalı, tatlı ve Apple oyunları tarzında Yükleme UI
    /// </summary>
    public sealed class SweetLoadingUI : MonoBehaviour
    {
        [Header("Sweet Loading UI References")]
        [SerializeField] private GameObject loadingCanvas;
        [SerializeField] private TMP_Text loadingText;
        [SerializeField] private Image progressBar;
        [SerializeField] private Image spinnerImage;
        [SerializeField] private TMP_Text tipText;

        private EightBitTheme theme;

        public static GameObject CreateSweetLoadingCanvas()
        {
            GameObject canvasGO = new GameObject("SweetLoadingCanvas");
            
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
            CreateSweetLoadingBackground(canvasGO);
            
            // Sweet loading content
            CreateSweetLoadingContent(canvasGO);
            
            // Sweet animations
            CreateSweetLoadingAnimations(canvasGO);
            
            return canvasGO;
        }

        static void CreateSweetLoadingBackground(GameObject canvas)
        {
            GameObject bgGO = new GameObject("SweetLoadingBackground");
            bgGO.transform.SetParent(canvas.transform, false);
            
            RectTransform bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            
            Image bgImage = bgGO.AddComponent<Image>();
            bgImage.color = new Color(0.1f, 0.1f, 0.3f, 1f); // Deep purple background
            
            // Sweet animated background
            SweetLoadingBackgroundAnimator bgAnim = bgGO.AddComponent<SweetLoadingBackgroundAnimator>();
            bgAnim.Initialize();
        }

        static void CreateSweetLoadingContent(GameObject canvas)
        {
            EightBitTheme theme = EightBitTheme.Instance;
            if (theme == null)
            {
                GameObject themeGO = new GameObject("EightBitTheme");
                theme = themeGO.AddComponent<EightBitTheme>();
            }
            
            // Sweet loading panel
            GameObject loadingPanel = theme.CreateSweetPanel("LoadingPanel", new Vector2(0, 0), new Vector2(500, 300), 
                new Color(0.2f, 0.2f, 0.4f, 0.9f));
            loadingPanel.transform.SetParent(canvas.transform, false);
            
            // Sweet loading title
            TMP_Text loadingTitle = theme.CreateSweetText("🍭 LOADING SWEET ADVENTURE 🍭", new Vector2(0, 100), new Vector2(450, 80), 
                new Color(1f, 0.4f, 0.8f, 1f), 36);
            loadingTitle.transform.SetParent(loadingPanel.transform, false);
            
            // Sweet progress bar
            CreateSweetProgressBar(loadingPanel);
            
            // Sweet loading text
            TMP_Text loadingText = theme.CreateSweetText("Preparing your sweet journey...", new Vector2(0, -50), new Vector2(400, 40), 
                new Color(1f, 0.9f, 0.2f, 1f), 24);
            loadingText.transform.SetParent(loadingPanel.transform, false);
            
            // Sweet tip text
            TMP_Text tipText = theme.CreateSweetText("💡 Tip: Collect sweet treats for bonus points!", new Vector2(0, -100), new Vector2(400, 40), 
                new Color(0.2f, 1f, 0.4f, 1f), 20);
            tipText.transform.SetParent(loadingPanel.transform, false);
        }

        static void CreateSweetProgressBar(GameObject parent)
        {
            // Progress bar background
            GameObject progressBgGO = new GameObject("ProgressBarBackground");
            progressBgGO.transform.SetParent(parent.transform, false);
            
            RectTransform progressBgRect = progressBgGO.AddComponent<RectTransform>();
            progressBgRect.anchoredPosition = new Vector2(0, 0);
            progressBgRect.sizeDelta = new Vector2(400, 20);
            
            Image progressBgImage = progressBgGO.AddComponent<Image>();
            progressBgImage.color = new Color(0.1f, 0.1f, 0.2f, 1f);
            
            // Progress bar fill
            GameObject progressFillGO = new GameObject("ProgressBarFill");
            progressFillGO.transform.SetParent(progressBgGO.transform, false);
            
            RectTransform progressFillRect = progressFillGO.AddComponent<RectTransform>();
            progressFillRect.anchorMin = Vector2.zero;
            progressFillRect.anchorMax = Vector2.one;
            progressFillRect.offsetMin = Vector2.zero;
            progressFillRect.offsetMax = Vector2.zero;
            
            Image progressFillImage = progressFillGO.AddComponent<Image>();
            progressFillImage.color = new Color(1f, 0.4f, 0.8f, 1f); // Sweet pink
            
            // Sweet progress bar animation
            SweetProgressBarAnimator progressAnim = progressFillGO.AddComponent<SweetProgressBarAnimator>();
            progressAnim.Initialize();
        }

        static void CreateSweetLoadingAnimations(GameObject canvas)
        {
            // Sweet spinning stars
            for (int i = 0; i < 15; i++)
            {
                CreateSweetSpinningStar(canvas, new Vector2(
                    Random.Range(-500, 500),
                    Random.Range(-800, 800)
                ));
            }
            
            // Sweet floating hearts
            for (int i = 0; i < 8; i++)
            {
                CreateSweetFloatingHeart(canvas, new Vector2(
                    Random.Range(-600, 600),
                    Random.Range(-900, 900)
                ));
            }
        }

        static void CreateSweetSpinningStar(GameObject canvas, Vector2 position)
        {
            GameObject starGO = new GameObject("SweetSpinningStar");
            starGO.transform.SetParent(canvas.transform, false);
            
            RectTransform starRect = starGO.AddComponent<RectTransform>();
            starRect.anchoredPosition = position;
            starRect.sizeDelta = new Vector2(30, 30);
            
            Image starImage = starGO.AddComponent<Image>();
            starImage.color = new Color(1f, 1f, 0f, 0.7f); // Golden yellow
            
            // Sweet spinning animation
            SweetSpinningStarAnimator starAnim = starGO.AddComponent<SweetSpinningStarAnimator>();
            starAnim.Initialize();
        }

        static void CreateSweetFloatingHeart(GameObject canvas, Vector2 position)
        {
            GameObject heartGO = new GameObject("SweetFloatingHeart");
            heartGO.transform.SetParent(canvas.transform, false);
            
            RectTransform heartRect = heartGO.AddComponent<RectTransform>();
            heartRect.anchoredPosition = position;
            heartRect.sizeDelta = new Vector2(25, 25);
            
            Image heartImage = heartGO.AddComponent<Image>();
            heartImage.color = new Color(1f, 0.4f, 0.8f, 0.6f); // Sweet pink
            
            // Sweet floating animation
            SweetFloatingHeartAnimator heartAnim = heartGO.AddComponent<SweetFloatingHeartAnimator>();
            heartAnim.Initialize();
        }
    }

    /// <summary>
    /// Tatlı yükleme arka plan animasyonu
    /// </summary>
    public sealed class SweetLoadingBackgroundAnimator : MonoBehaviour
    {
        private float animationSpeed = 0.5f;
        private Color[] sweetColors = {
            new Color(0.1f, 0.1f, 0.3f, 1f), // Deep purple
            new Color(0.2f, 0.1f, 0.4f, 1f), // Purple
            new Color(0.1f, 0.2f, 0.4f, 1f), // Blue
            new Color(0.1f, 0.3f, 0.2f, 1f)  // Green
        };
        private Image backgroundImage;

        public void Initialize()
        {
            backgroundImage = GetComponent<Image>();
        }

        void Update()
        {
            // Sweet color cycling
            float time = Time.time * animationSpeed;
            int colorIndex = Mathf.FloorToInt(time) % sweetColors.Length;
            int nextColorIndex = (colorIndex + 1) % sweetColors.Length;
            float t = time - Mathf.Floor(time);
            
            Color currentColor = Color.Lerp(sweetColors[colorIndex], sweetColors[nextColorIndex], t);
            backgroundImage.color = currentColor;
        }
    }

    /// <summary>
    /// Tatlı progress bar animasyonu
    /// </summary>
    public sealed class SweetProgressBarAnimator : MonoBehaviour
    {
        private float progress = 0f;
        private float progressSpeed = 0.5f;
        private Image progressImage;

        public void Initialize()
        {
            progressImage = GetComponent<Image>();
        }

        void Update()
        {
            // Sweet progress animation
            progress += progressSpeed * Time.deltaTime;
            progress = Mathf.Clamp01(progress);
            
            // Update fill amount
            progressImage.fillAmount = progress;
            
            // Sweet color animation
            float colorTime = Time.time * 2f;
            Color sweetColor = Color.Lerp(
                new Color(1f, 0.4f, 0.8f, 1f), // Sweet pink
                new Color(1f, 0.9f, 0.2f, 1f), // Sunshine yellow
                Mathf.Sin(colorTime) * 0.5f + 0.5f
            );
            progressImage.color = sweetColor;
        }
    }

    /// <summary>
    /// Tatlı dönen yıldız animasyonu
    /// </summary>
    public sealed class SweetSpinningStarAnimator : MonoBehaviour
    {
        private float rotationSpeed;
        private float scaleSpeed;
        private Vector3 originalScale;

        public void Initialize()
        {
            rotationSpeed = Random.Range(45f, 90f);
            scaleSpeed = Random.Range(1f, 3f);
            originalScale = transform.localScale;
        }

        void Update()
        {
            // Sweet rotation
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            
            // Sweet scale animation
            float scale = Mathf.Sin(Time.time * scaleSpeed) * 0.4f + 0.6f;
            transform.localScale = originalScale * scale;
        }
    }

    /// <summary>
    /// Tatlı yüzen kalp animasyonu
    /// </summary>
    public sealed class SweetFloatingHeartAnimator : MonoBehaviour
    {
        private float floatSpeed;
        private float scaleSpeed;
        private Vector3 originalPosition;
        private Vector3 originalScale;

        public void Initialize()
        {
            floatSpeed = Random.Range(0.8f, 2f);
            scaleSpeed = Random.Range(1f, 2.5f);
            originalPosition = transform.localPosition;
            originalScale = transform.localScale;
        }

        void Update()
        {
            // Sweet floating animation
            float floatY = Mathf.Sin(Time.time * floatSpeed) * 15f;
            float floatX = Mathf.Cos(Time.time * floatSpeed * 0.7f) * 10f;
            transform.localPosition = originalPosition + new Vector3(floatX, floatY, 0);
            
            // Sweet scale animation
            float scale = Mathf.Sin(Time.time * scaleSpeed) * 0.3f + 0.7f;
            transform.localScale = originalScale * scale;
        }
    }
}
