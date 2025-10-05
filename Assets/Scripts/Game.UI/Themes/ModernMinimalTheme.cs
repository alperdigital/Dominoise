using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI.Themes
{
    /// <summary>
    /// Modern Minimal Theme - Apple tarzı sade, siyah-beyaz-gri renk paleti
    /// </summary>
    public sealed class ModernMinimalTheme : MonoBehaviour
    {
        [Header("Modern Minimal Color Palette")]
        [SerializeField] private Color primaryBlack = new Color(0.1f, 0.1f, 0.1f, 1f);      // #1A1A1A
        [SerializeField] private Color secondaryGray = new Color(0.3f, 0.3f, 0.3f, 1f);   // #4D4D4D
        [SerializeField] private Color accentWhite = new Color(0.95f, 0.95f, 0.95f, 1f);    // #F2F2F2
        [SerializeField] private Color backgroundDark = new Color(0.05f, 0.05f, 0.05f, 1f); // #0D0D0D
        [SerializeField] private Color textLight = new Color(0.9f, 0.9f, 0.9f, 1f);         // #E6E6E6
        [SerializeField] private Color textDark = new Color(0.2f, 0.2f, 0.2f, 1f);         // #333333
        
        [Header("Modern Effects")]
        [SerializeField] private bool enableSubtleShadows = true;
        [SerializeField] private bool enableSmoothAnimations = true;
        [SerializeField] private bool enableMinimalBorders = true;
        
        [Header("Animation Settings")]
        [SerializeField] private float animationDuration = 0.3f;
        [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public static ModernMinimalTheme Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeTheme();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void InitializeTheme()
        {
            UnityEngine.Debug.Log("🎨 Modern Minimal Theme Initialized!");
            UnityEngine.Debug.Log("⚫ Apple-style UI Ready!");
        }

        /// <summary>
        /// Modern minimal button oluştur
        /// </summary>
        public GameObject CreateModernButton(string text, Vector2 position, Vector2 size, System.Action onClick = null)
        {
            GameObject buttonGO = new GameObject($"ModernButton_{text}");
            
            // RectTransform
            RectTransform rectTransform = buttonGO.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = position;
            rectTransform.sizeDelta = size;
            
            // Image (Modern style background)
            Image buttonImage = buttonGO.AddComponent<Image>();
            buttonImage.color = primaryBlack;
            
            // Button component
            Button button = buttonGO.AddComponent<Button>();
            button.targetGraphic = buttonImage;
            
            // Modern shadow effect
            if (enableSubtleShadows)
            {
                CreateSubtleShadow(buttonGO, new Vector2(0, -2));
            }
            
            // Modern text
            GameObject textGO = new GameObject("ButtonText");
            textGO.transform.SetParent(buttonGO.transform, false);
            
            RectTransform textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            
            TMP_Text buttonText = textGO.AddComponent<TextMeshProUGUI>();
            buttonText.text = text;
            buttonText.fontSize = 16;
            buttonText.color = accentWhite;
            buttonText.alignment = TextAlignmentOptions.Center;
            buttonText.fontStyle = FontStyles.Normal;
            
            // Modern animations
            if (enableSmoothAnimations)
            {
                ModernButtonAnimator animator = buttonGO.AddComponent<ModernButtonAnimator>();
                animator.Initialize(animationDuration, easeCurve);
            }
            
            // Click event
            if (onClick != null)
            {
                button.onClick.AddListener(() => onClick());
            }
            
            return buttonGO;
        }

        /// <summary>
        /// Modern minimal panel oluştur
        /// </summary>
        public GameObject CreateModernPanel(string name, Vector2 position, Vector2 size, Color backgroundColor)
        {
            GameObject panelGO = new GameObject($"ModernPanel_{name}");
            
            RectTransform rectTransform = panelGO.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = position;
            rectTransform.sizeDelta = size;
            
            Image panelImage = panelGO.AddComponent<Image>();
            panelImage.color = backgroundColor;
            
            // Modern border effect
            if (enableMinimalBorders)
            {
                CreateMinimalBorder(panelGO, secondaryGray, 1f);
            }
            
            // Subtle shadow
            if (enableSubtleShadows)
            {
                CreateSubtleShadow(panelGO, new Vector2(0, -2));
            }
            
            return panelGO;
        }

        /// <summary>
        /// Modern minimal text oluştur
        /// </summary>
        public TMP_Text CreateModernText(string text, Vector2 position, Vector2 size, Color textColor, int fontSize = 14)
        {
            GameObject textGO = new GameObject($"ModernText_{text}");
            
            RectTransform rectTransform = textGO.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = position;
            rectTransform.sizeDelta = size;
            
            TMP_Text modernText = textGO.AddComponent<TextMeshProUGUI>();
            modernText.text = text;
            modernText.fontSize = fontSize;
            modernText.color = textColor;
            modernText.alignment = TextAlignmentOptions.Center;
            modernText.fontStyle = FontStyles.Normal;
            
            return modernText;
        }

        /// <summary>
        /// Subtle shadow efekti oluştur
        /// </summary>
        void CreateSubtleShadow(GameObject parent, Vector2 offset)
        {
            GameObject shadowGO = new GameObject("SubtleShadow");
            shadowGO.transform.SetParent(parent.transform, false);
            
            RectTransform shadowRect = shadowGO.AddComponent<RectTransform>();
            shadowRect.anchorMin = Vector2.zero;
            shadowRect.anchorMax = Vector2.one;
            shadowRect.offsetMin = Vector2.zero;
            shadowRect.offsetMax = Vector2.zero;
            shadowRect.anchoredPosition = offset;
            
            Image shadowImage = shadowGO.AddComponent<Image>();
            shadowImage.color = new Color(0f, 0f, 0f, 0.1f);
            shadowImage.sprite = parent.GetComponent<Image>()?.sprite;
            
            // Shadow'u arkaya gönder
            shadowGO.transform.SetAsFirstSibling();
        }

        /// <summary>
        /// Minimal border efekti oluştur
        /// </summary>
        void CreateMinimalBorder(GameObject parent, Color borderColor, float borderWidth)
        {
            // Top border
            CreateBorderLine(parent, "TopBorder", new Vector2(0, parent.GetComponent<RectTransform>().sizeDelta.y / 2), 
                           new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, borderWidth), borderColor);
            
            // Bottom border
            CreateBorderLine(parent, "BottomBorder", new Vector2(0, -parent.GetComponent<RectTransform>().sizeDelta.y / 2), 
                           new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, borderWidth), borderColor);
            
            // Left border
            CreateBorderLine(parent, "LeftBorder", new Vector2(-parent.GetComponent<RectTransform>().sizeDelta.x / 2, 0), 
                           new Vector2(borderWidth, parent.GetComponent<RectTransform>().sizeDelta.y), borderColor);
            
            // Right border
            CreateBorderLine(parent, "RightBorder", new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x / 2, 0), 
                           new Vector2(borderWidth, parent.GetComponent<RectTransform>().sizeDelta.y), borderColor);
        }

        void CreateBorderLine(GameObject parent, string name, Vector2 position, Vector2 size, Color color)
        {
            GameObject borderGO = new GameObject(name);
            borderGO.transform.SetParent(parent.transform, false);
            
            RectTransform borderRect = borderGO.AddComponent<RectTransform>();
            borderRect.anchoredPosition = position;
            borderRect.sizeDelta = size;
            
            Image borderImage = borderGO.AddComponent<Image>();
            borderImage.color = color;
        }

        /// <summary>
        /// Renk paletini al
        /// </summary>
        public Color GetColor(string colorName)
        {
            switch (colorName.ToLower())
            {
                case "primary": return primaryBlack;
                case "secondary": return secondaryGray;
                case "accent": return accentWhite;
                case "background": return backgroundDark;
                case "textlight": return textLight;
                case "textdark": return textDark;
                default: return Color.white;
            }
        }
    }

    /// <summary>
    /// Modern button animasyonları
    /// </summary>
    public sealed class ModernButtonAnimator : MonoBehaviour
    {
        private float animationDuration;
        private AnimationCurve easeCurve;
        private Vector3 originalScale;
        private Color originalColor;
        private Image buttonImage;

        public void Initialize(float duration, AnimationCurve curve)
        {
            animationDuration = duration;
            easeCurve = curve;
            originalScale = transform.localScale;
            buttonImage = GetComponent<Image>();
            originalColor = buttonImage.color;
        }

        public void OnButtonHover()
        {
            StartCoroutine(ScaleAnimation(originalScale * 1.05f));
        }

        public void OnButtonExit()
        {
            StartCoroutine(ScaleAnimation(originalScale));
        }

        public void OnButtonClick()
        {
            StartCoroutine(ClickAnimation());
        }

        System.Collections.IEnumerator ScaleAnimation(Vector3 targetScale)
        {
            Vector3 startScale = transform.localScale;
            float elapsed = 0f;

            while (elapsed < animationDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / animationDuration;
                float curveValue = easeCurve.Evaluate(t);
                
                transform.localScale = Vector3.Lerp(startScale, targetScale, curveValue);
                yield return null;
            }

            transform.localScale = targetScale;
        }

        System.Collections.IEnumerator ClickAnimation()
        {
            Vector3 clickScale = originalScale * 0.95f;
            yield return StartCoroutine(ScaleAnimation(clickScale));
            yield return StartCoroutine(ScaleAnimation(originalScale));
        }
    }
}
