using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.UI;

namespace Game.UI.Prefabs
{
    public static class MainMenuUI
    {
        public static GameObject CreateMainMenuCanvas()
        {
            // Create main menu canvas
            GameObject canvasGO = new GameObject("MainMenuCanvas");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
            GraphicRaycaster raycaster = canvasGO.AddComponent<GraphicRaycaster>();

            // Setup canvas for mobile
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920); // Portrait mobile
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            // Create menu sections
            CreateMenuBackground(canvasGO);
            CreateGameTitle(canvasGO);
            CreateMainButtons(canvasGO);
            CreateSettingsPanel(canvasGO);
            CreateCreditsPanel(canvasGO);

            return canvasGO;
        }

        static void CreateMenuBackground(GameObject parent)
        {
            GameObject bgGO = new GameObject("MenuBackground");
            bgGO.transform.SetParent(parent.transform, false);
            
            RectTransform bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            
            Image bgImage = bgGO.AddComponent<Image>();
            bgImage.color = new Color(0.05f, 0.1f, 0.2f, 1f); // Dark blue gradient background
            
            // Add some visual elements
            CreateBackgroundPattern(bgGO);
        }

        static void CreateBackgroundPattern(GameObject parent)
        {
            // Create floating geometric shapes for visual appeal
            for (int i = 0; i < 8; i++)
            {
                GameObject shapeGO = new GameObject($"BackgroundShape_{i}");
                shapeGO.transform.SetParent(parent.transform, false);
                
                RectTransform shapeRect = shapeGO.AddComponent<RectTransform>();
                shapeRect.anchorMin = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
                shapeRect.anchorMax = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
                shapeRect.sizeDelta = new Vector2(Random.Range(50, 150), Random.Range(50, 150));
                shapeRect.anchoredPosition = Vector2.zero;
                
                Image shapeImage = shapeGO.AddComponent<Image>();
                shapeImage.color = new Color(1f, 1f, 1f, 0.1f);
                shapeImage.sprite = CreateSimpleSprite("SHAPE");
            }
        }

        static void CreateGameTitle(GameObject parent)
        {
            GameObject titleGO = new GameObject("GameTitle");
            titleGO.transform.SetParent(parent.transform, false);
            
            RectTransform titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.7f);
            titleRect.anchorMax = new Vector2(0.5f, 0.7f);
            titleRect.sizeDelta = new Vector2(600, 120);
            titleRect.anchoredPosition = Vector2.zero;
            
            TMP_Text titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "DOMINOISE";
            titleText.fontSize = 72;
            titleText.color = new Color(1f, 0.8f, 0f, 1f); // Gold color
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.fontStyle = FontStyles.Bold;
            
            // Add shadow effect
            GameObject shadowGO = new GameObject("TitleShadow");
            shadowGO.transform.SetParent(titleGO.transform, false);
            
            RectTransform shadowRect = shadowGO.AddComponent<RectTransform>();
            shadowRect.anchorMin = Vector2.zero;
            shadowRect.anchorMax = Vector2.one;
            shadowRect.offsetMin = new Vector2(3, -3);
            shadowRect.offsetMax = new Vector2(3, -3);
            
            TMP_Text shadowText = shadowGO.AddComponent<TextMeshProUGUI>();
            shadowText.text = "DOMINOISE";
            shadowText.fontSize = 72;
            shadowText.color = new Color(0f, 0f, 0f, 0.5f);
            shadowText.alignment = TextAlignmentOptions.Center;
            shadowText.fontStyle = FontStyles.Bold;
            
            // Move shadow behind title
            shadowGO.transform.SetAsFirstSibling();
        }

        static void CreateMainButtons(GameObject parent)
        {
            GameObject buttonsGO = new GameObject("MainButtons");
            buttonsGO.transform.SetParent(parent.transform, false);
            
            RectTransform buttonsRect = buttonsGO.AddComponent<RectTransform>();
            buttonsRect.anchorMin = new Vector2(0.5f, 0.3f);
            buttonsRect.anchorMax = new Vector2(0.5f, 0.3f);
            buttonsRect.sizeDelta = new Vector2(400, 400);
            buttonsRect.anchoredPosition = Vector2.zero;

            // Play Button
            GameObject playButtonGO = CreateMenuButton("PlayButton", "OYNA", buttonsGO.transform, 
                new Vector2(0, 100), new Vector2(300, 80), new Color(0.2f, 0.8f, 0.2f, 1f));

            // VS Mode Button
            GameObject vsButtonGO = CreateMenuButton("VSButton", "VS MOD", buttonsGO.transform, 
                new Vector2(0, 0), new Vector2(300, 80), new Color(0.2f, 0.6f, 1f, 1f));

            // Co-op Button (Disabled)
            GameObject coopButtonGO = CreateMenuButton("CoopButton", "CO-OP MOD", buttonsGO.transform, 
                new Vector2(0, -100), new Vector2(300, 80), new Color(0.4f, 0.4f, 0.4f, 1f));
            
            Button coopButton = coopButtonGO.GetComponent<Button>();
            coopButton.interactable = false;

            // Settings Button
            GameObject settingsButtonGO = CreateMenuButton("SettingsButton", "AYARLAR", buttonsGO.transform, 
                new Vector2(0, -200), new Vector2(300, 80), new Color(0.6f, 0.6f, 0.6f, 1f));
        }

        static void CreateSettingsPanel(GameObject parent)
        {
            GameObject settingsPanelGO = new GameObject("SettingsPanel");
            settingsPanelGO.transform.SetParent(parent.transform, false);
            
            RectTransform settingsRect = settingsPanelGO.AddComponent<RectTransform>();
            settingsRect.anchorMin = Vector2.zero;
            settingsRect.anchorMax = Vector2.one;
            settingsRect.offsetMin = Vector2.zero;
            settingsRect.offsetMax = Vector2.zero;
            
            Image settingsBg = settingsPanelGO.AddComponent<Image>();
            settingsBg.color = new Color(0f, 0f, 0f, 0.8f);
            
            // Settings content
            GameObject contentGO = new GameObject("SettingsContent");
            contentGO.transform.SetParent(settingsPanelGO.transform, false);
            
            RectTransform contentRect = contentGO.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0.5f, 0.5f);
            contentRect.anchorMax = new Vector2(0.5f, 0.5f);
            contentRect.sizeDelta = new Vector2(500, 600);
            contentRect.anchoredPosition = Vector2.zero;
            
            Image contentBg = contentGO.AddComponent<Image>();
            contentBg.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
            
            // Settings title
            GameObject titleGO = new GameObject("SettingsTitle");
            titleGO.transform.SetParent(contentGO.transform, false);
            
            RectTransform titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.8f);
            titleRect.anchorMax = new Vector2(0.5f, 0.8f);
            titleRect.sizeDelta = new Vector2(400, 60);
            titleRect.anchoredPosition = Vector2.zero;
            
            TMP_Text titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "AYARLAR";
            titleText.fontSize = 32;
            titleText.color = Color.white;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.fontStyle = FontStyles.Bold;
            
            // Volume slider
            CreateVolumeSlider(contentGO.transform, new Vector2(0, 100));
            
            // Sound toggle
            CreateSoundToggle(contentGO.transform, new Vector2(0, 0));
            
            // Close button
            GameObject closeButtonGO = CreateMenuButton("CloseSettings", "KAPAT", contentGO.transform, 
                new Vector2(0, -200), new Vector2(200, 60), new Color(0.8f, 0.2f, 0.2f, 1f));
            
            // Initially hidden
            settingsPanelGO.SetActive(false);
        }

        static void CreateCreditsPanel(GameObject parent)
        {
            GameObject creditsPanelGO = new GameObject("CreditsPanel");
            creditsPanelGO.transform.SetParent(parent.transform, false);
            
            RectTransform creditsRect = creditsPanelGO.AddComponent<RectTransform>();
            creditsRect.anchorMin = Vector2.zero;
            creditsRect.anchorMax = Vector2.one;
            creditsRect.offsetMin = Vector2.zero;
            creditsRect.offsetMax = Vector2.zero;
            
            Image creditsBg = creditsPanelGO.AddComponent<Image>();
            creditsBg.color = new Color(0f, 0f, 0f, 0.8f);
            
            // Credits content
            GameObject contentGO = new GameObject("CreditsContent");
            contentGO.transform.SetParent(creditsPanelGO.transform, false);
            
            RectTransform contentRect = contentGO.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0.5f, 0.5f);
            contentRect.anchorMax = new Vector2(0.5f, 0.5f);
            contentRect.sizeDelta = new Vector2(500, 600);
            contentRect.anchoredPosition = Vector2.zero;
            
            Image contentBg = contentGO.AddComponent<Image>();
            contentBg.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
            
            // Credits title
            GameObject titleGO = new GameObject("CreditsTitle");
            titleGO.transform.SetParent(contentGO.transform, false);
            
            RectTransform titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.8f);
            titleRect.anchorMax = new Vector2(0.5f, 0.8f);
            titleRect.sizeDelta = new Vector2(400, 60);
            titleRect.anchoredPosition = Vector2.zero;
            
            TMP_Text titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "HAKKINDA";
            titleText.fontSize = 32;
            titleText.color = Color.white;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.fontStyle = FontStyles.Bold;
            
            // Credits text
            GameObject creditsTextGO = new GameObject("CreditsText");
            creditsTextGO.transform.SetParent(contentGO.transform, false);
            
            RectTransform creditsTextRect = creditsTextGO.AddComponent<RectTransform>();
            creditsTextRect.anchorMin = new Vector2(0.5f, 0.5f);
            creditsTextRect.anchorMax = new Vector2(0.5f, 0.5f);
            creditsTextRect.sizeDelta = new Vector2(450, 400);
            creditsTextRect.anchoredPosition = Vector2.zero;
            
            TMP_Text creditsText = creditsTextGO.AddComponent<TextMeshProUGUI>();
            creditsText.text = "DOMINOISE v1.0\n\n" +
                              "2D Mobil Poz Oyunu\n\n" +
                              "Geliştirici: Unity Team\n" +
                              "Tasarım: Modern UI\n" +
                              "Ses: Unity Audio\n\n" +
                              "© 2024 Dominoise Games";
            creditsText.fontSize = 18;
            creditsText.color = Color.white;
            creditsText.alignment = TextAlignmentOptions.Center;
            
            // Close button
            GameObject closeButtonGO = CreateMenuButton("CloseCredits", "KAPAT", contentGO.transform, 
                new Vector2(0, -250), new Vector2(200, 60), new Color(0.8f, 0.2f, 0.2f, 1f));
            
            // Initially hidden
            creditsPanelGO.SetActive(false);
        }

        static GameObject CreateMenuButton(string name, string text, Transform parent, Vector2 position, Vector2 size, Color color)
        {
            GameObject buttonGO = new GameObject(name);
            buttonGO.transform.SetParent(parent, false);
            
            RectTransform buttonRect = buttonGO.AddComponent<RectTransform>();
            buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
            buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
            buttonRect.sizeDelta = size;
            buttonRect.anchoredPosition = position;
            
            Image buttonImage = buttonGO.AddComponent<Image>();
            buttonImage.color = color;
            
            Button button = buttonGO.AddComponent<Button>();
            button.targetGraphic = buttonImage;

            // Button text
            GameObject textGO = new GameObject("Text");
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

            return buttonGO;
        }

        static void CreateVolumeSlider(Transform parent, Vector2 position)
        {
            GameObject sliderGO = new GameObject("VolumeSlider");
            sliderGO.transform.SetParent(parent, false);
            
            RectTransform sliderRect = sliderGO.AddComponent<RectTransform>();
            sliderRect.anchorMin = new Vector2(0.5f, 0.5f);
            sliderRect.anchorMax = new Vector2(0.5f, 0.5f);
            sliderRect.sizeDelta = new Vector2(300, 30);
            sliderRect.anchoredPosition = position;
            
            Slider slider = sliderGO.AddComponent<Slider>();
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = 0.8f;
            
            // Slider background
            GameObject backgroundGO = new GameObject("Background");
            backgroundGO.transform.SetParent(sliderGO.transform, false);
            
            RectTransform backgroundRect = backgroundGO.AddComponent<RectTransform>();
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.anchorMax = Vector2.one;
            backgroundRect.offsetMin = Vector2.zero;
            backgroundRect.offsetMax = Vector2.zero;
            
            Image backgroundImage = backgroundGO.AddComponent<Image>();
            backgroundImage.color = new Color(0.3f, 0.3f, 0.3f, 1f);
            
            // Slider fill
            GameObject fillGO = new GameObject("Fill");
            fillGO.transform.SetParent(sliderGO.transform, false);
            
            RectTransform fillRect = fillGO.AddComponent<RectTransform>();
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = Vector2.one;
            fillRect.offsetMin = Vector2.zero;
            fillRect.offsetMax = Vector2.zero;
            
            Image fillImage = fillGO.AddComponent<Image>();
            fillImage.color = new Color(0.2f, 0.6f, 1f, 1f);
            
            // Slider handle
            GameObject handleGO = new GameObject("Handle");
            handleGO.transform.SetParent(sliderGO.transform, false);
            
            RectTransform handleRect = handleGO.AddComponent<RectTransform>();
            handleRect.anchorMin = new Vector2(0.5f, 0.5f);
            handleRect.anchorMax = new Vector2(0.5f, 0.5f);
            handleRect.sizeDelta = new Vector2(20, 20);
            handleRect.anchoredPosition = Vector2.zero;
            
            Image handleImage = handleGO.AddComponent<Image>();
            handleImage.color = Color.white;
            
            slider.targetGraphic = handleImage;
            slider.fillRect = fillRect;
            slider.handleRect = handleRect;
        }

        static void CreateSoundToggle(Transform parent, Vector2 position)
        {
            GameObject toggleGO = new GameObject("SoundToggle");
            toggleGO.transform.SetParent(parent, false);
            
            RectTransform toggleRect = toggleGO.AddComponent<RectTransform>();
            toggleRect.anchorMin = new Vector2(0.5f, 0.5f);
            toggleRect.anchorMax = new Vector2(0.5f, 0.5f);
            toggleRect.sizeDelta = new Vector2(200, 50);
            toggleRect.anchoredPosition = position;
            
            Toggle toggle = toggleGO.AddComponent<Toggle>();
            toggle.isOn = true;
            
            // Toggle background
            GameObject backgroundGO = new GameObject("Background");
            backgroundGO.transform.SetParent(toggleGO.transform, false);
            
            RectTransform backgroundRect = backgroundGO.AddComponent<RectTransform>();
            backgroundRect.anchorMin = new Vector2(0, 0.2f);
            backgroundRect.anchorMax = new Vector2(0.3f, 0.8f);
            backgroundRect.offsetMin = Vector2.zero;
            backgroundRect.offsetMax = Vector2.zero;
            
            Image backgroundImage = backgroundGO.AddComponent<Image>();
            backgroundImage.color = new Color(0.3f, 0.3f, 0.3f, 1f);
            
            // Toggle checkmark
            GameObject checkmarkGO = new GameObject("Checkmark");
            checkmarkGO.transform.SetParent(backgroundGO.transform, false);
            
            RectTransform checkmarkRect = checkmarkGO.AddComponent<RectTransform>();
            checkmarkRect.anchorMin = Vector2.zero;
            checkmarkRect.anchorMax = Vector2.one;
            checkmarkRect.offsetMin = Vector2.zero;
            checkmarkRect.offsetMax = Vector2.zero;
            
            Image checkmarkImage = checkmarkGO.AddComponent<Image>();
            checkmarkImage.color = Color.white;
            
            // Toggle label
            GameObject labelGO = new GameObject("Label");
            labelGO.transform.SetParent(toggleGO.transform, false);
            
            RectTransform labelRect = labelGO.AddComponent<RectTransform>();
            labelRect.anchorMin = new Vector2(0.4f, 0);
            labelRect.anchorMax = new Vector2(1, 1);
            labelRect.offsetMin = Vector2.zero;
            labelRect.offsetMax = Vector2.zero;
            
            TMP_Text labelText = labelGO.AddComponent<TextMeshProUGUI>();
            labelText.text = "Ses Açık";
            labelText.fontSize = 18;
            labelText.color = Color.white;
            labelText.alignment = TextAlignmentOptions.Left;
            
            toggle.targetGraphic = backgroundImage;
            toggle.graphic = checkmarkImage;
        }

        static Sprite CreateSimpleSprite(string name)
        {
            // Create a simple colored sprite
            Texture2D texture = new Texture2D(64, 64);
            Color[] pixels = new Color[64 * 64];
            
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.white;
            }
            
            texture.SetPixels(pixels);
            texture.Apply();
            
            return Sprite.Create(texture, new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f));
        }
    }
}
