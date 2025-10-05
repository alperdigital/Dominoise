using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Game.UI.Themes
{
    /// <summary>
    /// 8-bit temalı, tatlı ve Apple oyunları tarzında UI tema sistemi
    /// </summary>
    public sealed class EightBitTheme : MonoBehaviour
    {
        [Header("8-bit Color Palette")]
        [SerializeField] private Color primaryPink = new Color(1f, 0.4f, 0.8f, 1f);      // Sweet Pink
        [SerializeField] private Color secondaryPurple = new Color(0.6f, 0.2f, 1f, 1f); // Royal Purple
        [SerializeField] private Color accentYellow = new Color(1f, 0.9f, 0.2f, 1f);   // Sunshine Yellow
        [SerializeField] private Color successGreen = new Color(0.2f, 1f, 0.4f, 1f);    // Mint Green
        [SerializeField] private Color dangerRed = new Color(1f, 0.2f, 0.3f, 1f);      // Cherry Red
        [SerializeField] private Color backgroundBlue = new Color(0.1f, 0.3f, 0.8f, 1f); // Ocean Blue
        
        [Header("8-bit Effects")]
        [SerializeField] private bool enableRetroShadows = true;
        [SerializeField] private bool enableSweetAnimations = true;
        
        [Header("Sweet Animations")]
        [SerializeField] private float bounceIntensity = 0.1f;
        [SerializeField] private float glowIntensity = 1.5f;
        [SerializeField] private float pulseSpeed = 2f;

        public static EightBitTheme Instance { get; private set; }

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
            UnityEngine.Debug.Log("🎮 8-bit Sweet Theme Initialized!");
            UnityEngine.Debug.Log("🍭 Apple-style UI Ready!");
        }

        /// <summary>
        /// 8-bit tarzında buton oluştur
        /// </summary>
        public GameObject CreateSweetButton(string text, Vector2 position, Vector2 size, System.Action onClick = null)
        {
            GameObject buttonGO = new GameObject($"SweetButton_{text}");
            
            // RectTransform
            RectTransform rectTransform = buttonGO.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = position;
            rectTransform.sizeDelta = size;
            
            // Image (8-bit style background)
            Image buttonImage = buttonGO.AddComponent<Image>();
            buttonImage.color = primaryPink;
            buttonImage.sprite = CreatePixelSprite("BUTTON");
            
            // Button component
            Button button = buttonGO.AddComponent<Button>();
            button.targetGraphic = buttonImage;
            
            // 8-bit shadow effect
            if (enableRetroShadows)
            {
                CreateRetroShadow(buttonGO, new Vector2(4, -4));
            }
            
            // Sweet text
            GameObject textGO = new GameObject("ButtonText");
            textGO.transform.SetParent(buttonGO.transform, false);
            
            RectTransform textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            
            TMP_Text buttonText = textGO.AddComponent<TextMeshProUGUI>();
            buttonText.text = text;
            buttonText.fontSize = 24;
            buttonText.color = Color.white;
            buttonText.alignment = TextAlignmentOptions.Center;
            buttonText.fontStyle = FontStyles.Bold;
            
            // Sweet animations
            if (enableSweetAnimations)
            {
                SweetButtonAnimator animator = buttonGO.AddComponent<SweetButtonAnimator>();
                animator.Initialize(bounceIntensity, glowIntensity, pulseSpeed);
            }
            
            // Click event
            if (onClick != null)
            {
                button.onClick.AddListener(() => onClick());
            }
            
            return buttonGO;
        }

        /// <summary>
        /// Tatlı 8-bit panel oluştur
        /// </summary>
        public GameObject CreateSweetPanel(string name, Vector2 position, Vector2 size, Color backgroundColor)
        {
            GameObject panelGO = new GameObject($"SweetPanel_{name}");
            
            RectTransform rectTransform = panelGO.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = position;
            rectTransform.sizeDelta = size;
            
            Image panelImage = panelGO.AddComponent<Image>();
            panelImage.color = backgroundColor;
            panelImage.sprite = CreatePixelSprite("PANEL");
            
            // 8-bit border effect
            CreatePixelBorder(panelGO, new Color(1f, 1f, 1f, 0.8f), 2f);
            
            // Retro shadow
            if (enableRetroShadows)
            {
                CreateRetroShadow(panelGO, new Vector2(6, -6));
            }
            
            return panelGO;
        }

        /// <summary>
        /// 8-bit tarzında tatlı text oluştur
        /// </summary>
        public TMP_Text CreateSweetText(string text, Vector2 position, Vector2 size, Color textColor, int fontSize = 32)
        {
            GameObject textGO = new GameObject($"SweetText_{text}");
            
            RectTransform rectTransform = textGO.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = position;
            rectTransform.sizeDelta = size;
            
            TMP_Text sweetText = textGO.AddComponent<TextMeshProUGUI>();
            sweetText.text = text;
            sweetText.fontSize = fontSize;
            sweetText.color = textColor;
            sweetText.alignment = TextAlignmentOptions.Center;
            sweetText.fontStyle = FontStyles.Bold;
            
            // 8-bit text shadow
            CreateTextShadow(textGO, new Vector2(2, -2), new Color(0f, 0f, 0f, 0.5f));
            
            return sweetText;
        }

        /// <summary>
        /// Retro shadow efekti oluştur
        /// </summary>
        void CreateRetroShadow(GameObject parent, Vector2 offset)
        {
            GameObject shadowGO = new GameObject("RetroShadow");
            shadowGO.transform.SetParent(parent.transform, false);
            
            RectTransform shadowRect = shadowGO.AddComponent<RectTransform>();
            shadowRect.anchorMin = Vector2.zero;
            shadowRect.anchorMax = Vector2.one;
            shadowRect.offsetMin = Vector2.zero;
            shadowRect.offsetMax = Vector2.zero;
            shadowRect.anchoredPosition = offset;
            
            Image shadowImage = shadowGO.AddComponent<Image>();
            shadowImage.color = new Color(0f, 0f, 0f, 0.3f);
            shadowImage.sprite = parent.GetComponent<Image>()?.sprite;
            
            // Shadow'u arkaya gönder
            shadowGO.transform.SetAsFirstSibling();
        }

        /// <summary>
        /// Text shadow efekti oluştur
        /// </summary>
        void CreateTextShadow(GameObject parent, Vector2 offset, Color shadowColor)
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

        /// <summary>
        /// Pixel border efekti oluştur
        /// </summary>
        void CreatePixelBorder(GameObject parent, Color borderColor, float borderWidth)
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
        /// Pixel sprite oluştur
        /// </summary>
        Sprite CreatePixelSprite(string name)
        {
            Texture2D texture = new Texture2D(64, 64);
            texture.filterMode = FilterMode.Point; // Pixel perfect
            
            Color[] pixels = new Color[64 * 64];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.white;
            }
            
            texture.SetPixels(pixels);
            texture.Apply();
            
            return Sprite.Create(texture, new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f));
        }

        /// <summary>
        /// Renk paletini al
        /// </summary>
        public Color GetColor(string colorName)
        {
            switch (colorName.ToLower())
            {
                case "primary": return primaryPink;
                case "secondary": return secondaryPurple;
                case "accent": return accentYellow;
                case "success": return successGreen;
                case "danger": return dangerRed;
                case "background": return backgroundBlue;
                default: return Color.white;
            }
        }
    }

    /// <summary>
    /// Tatlı buton animasyonları
    /// </summary>
    public sealed class SweetButtonAnimator : MonoBehaviour
    {
        private float bounceIntensity;
        private float glowIntensity;
        private float pulseSpeed;
        private Vector3 originalScale;
        private Color originalColor;
        private Image buttonImage;

        public void Initialize(float bounce, float glow, float pulse)
        {
            bounceIntensity = bounce;
            glowIntensity = glow;
            pulseSpeed = pulse;
            originalScale = transform.localScale;
            buttonImage = GetComponent<Image>();
            originalColor = buttonImage.color;
        }

        void Start()
        {
            StartCoroutine(SweetPulseAnimation());
        }

        IEnumerator SweetPulseAnimation()
        {
            while (true)
            {
                // Glow effect
                float glow = Mathf.Sin(Time.time * pulseSpeed) * glowIntensity;
                buttonImage.color = Color.Lerp(originalColor, Color.white, glow * 0.3f);
                
                yield return null;
            }
        }

        public void OnButtonHover()
        {
            transform.localScale = originalScale * (1f + bounceIntensity);
        }

        public void OnButtonExit()
        {
            transform.localScale = originalScale;
        }

        public void OnButtonClick()
        {
            StartCoroutine(ClickBounce());
        }

        IEnumerator ClickBounce()
        {
            Vector3 clickScale = originalScale * 0.9f;
            transform.localScale = clickScale;
            
            yield return new WaitForSeconds(0.1f);
            
            transform.localScale = originalScale;
        }
    }
}
