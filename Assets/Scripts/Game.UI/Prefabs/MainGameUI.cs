using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.UI;

namespace Game.UI.Prefabs
{
    public static class MainGameUI
    {
        public static GameObject CreateMainGameCanvas()
        {
            // Create main canvas
            GameObject canvasGO = new GameObject("MainGameCanvas");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
            GraphicRaycaster raycaster = canvasGO.AddComponent<GraphicRaycaster>();

            // Setup canvas for mobile
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920); // Portrait mobile
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            // Create background
            CreateBackground(canvasGO);

            // Create UI sections
            CreateTopBar(canvasGO);
            CreateModeSelector(canvasGO);
            CreatePlayerPreviewArea(canvasGO);
            CreateCenterGameArea(canvasGO);
            CreateBottomControls(canvasGO);
            CreateEconomyUI(canvasGO);
            CreatePopupSystem(canvasGO);

            return canvasGO;
        }

        static void CreateBackground(GameObject parent)
        {
            GameObject bgGO = new GameObject("Background");
            bgGO.transform.SetParent(parent.transform, false);
            
            RectTransform bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            
            Image bgImage = bgGO.AddComponent<Image>();
            bgImage.color = new Color(0.1f, 0.1f, 0.2f, 1f); // Dark blue background
        }

        static void CreateTopBar(GameObject parent)
        {
            GameObject topBarGO = new GameObject("TopBar");
            topBarGO.transform.SetParent(parent.transform, false);
            
            RectTransform topBarRect = topBarGO.AddComponent<RectTransform>();
            topBarRect.anchorMin = new Vector2(0, 0.9f);
            topBarRect.anchorMax = new Vector2(1, 1);
            topBarRect.offsetMin = Vector2.zero;
            topBarRect.offsetMax = Vector2.zero;

            // Logo
            GameObject logoGO = new GameObject("Logo");
            logoGO.transform.SetParent(topBarGO.transform, false);
            
            RectTransform logoRect = logoGO.AddComponent<RectTransform>();
            logoRect.anchorMin = new Vector2(0.5f, 0.5f);
            logoRect.anchorMax = new Vector2(0.5f, 0.5f);
            logoRect.sizeDelta = new Vector2(200, 60);
            logoRect.anchoredPosition = Vector2.zero;
            
            Image logoImage = logoGO.AddComponent<Image>();
            logoImage.color = Color.white;
            logoImage.sprite = CreateSimpleSprite("LOGO");

            // Gold display
            GameObject goldGO = new GameObject("GoldDisplay");
            goldGO.transform.SetParent(topBarGO.transform, false);
            
            RectTransform goldRect = goldGO.AddComponent<RectTransform>();
            goldRect.anchorMin = new Vector2(0.8f, 0.2f);
            goldRect.anchorMax = new Vector2(0.95f, 0.8f);
            goldRect.offsetMin = Vector2.zero;
            goldRect.offsetMax = Vector2.zero;
            
            Image goldBg = goldGO.AddComponent<Image>();
            goldBg.color = new Color(1f, 0.8f, 0f, 0.8f);
            
            // Gold text
            GameObject goldTextGO = new GameObject("GoldText");
            goldTextGO.transform.SetParent(goldGO.transform, false);
            
            RectTransform goldTextRect = goldTextGO.AddComponent<RectTransform>();
            goldTextRect.anchorMin = Vector2.zero;
            goldTextRect.anchorMax = Vector2.one;
            goldTextRect.offsetMin = Vector2.zero;
            goldTextRect.offsetMax = Vector2.zero;
            
            TMP_Text goldText = goldTextGO.AddComponent<TextMeshProUGUI>();
            goldText.text = "5🪙";
            goldText.fontSize = 24;
            goldText.color = Color.white;
            goldText.alignment = TextAlignmentOptions.Center;
        }

        static void CreateModeSelector(GameObject parent)
        {
            GameObject modeSelectorGO = new GameObject("ModeSelector");
            modeSelectorGO.transform.SetParent(parent.transform, false);
            
            RectTransform modeRect = modeSelectorGO.AddComponent<RectTransform>();
            modeRect.anchorMin = new Vector2(0, 0.8f);
            modeRect.anchorMax = new Vector2(1, 0.9f);
            modeRect.offsetMin = Vector2.zero;
            modeRect.offsetMax = Vector2.zero;

            // VS Button
            GameObject vsButtonGO = CreateModernButton("VS", "VS Mode", modeSelectorGO.transform, 
                new Vector2(-150, 0), new Vector2(200, 80), new Color(0.2f, 0.6f, 1f, 1f));

            // Co-op Button (Disabled)
            GameObject coopButtonGO = CreateModernButton("Co-op", "Co-op Mode", modeSelectorGO.transform, 
                new Vector2(150, 0), new Vector2(200, 80), new Color(0.4f, 0.4f, 0.4f, 1f));
            
            Button coopButton = coopButtonGO.GetComponent<Button>();
            coopButton.interactable = false;
        }

        static void CreatePlayerPreviewArea(GameObject parent)
        {
            GameObject playerAreaGO = new GameObject("PlayerPreviewArea");
            playerAreaGO.transform.SetParent(parent.transform, false);
            
            RectTransform playerRect = playerAreaGO.AddComponent<RectTransform>();
            playerRect.anchorMin = new Vector2(0, 0.4f);
            playerRect.anchorMax = new Vector2(1, 0.8f);
            playerRect.offsetMin = Vector2.zero;
            playerRect.offsetMax = Vector2.zero;

            // Left Player Preview
            GameObject leftPreviewGO = CreatePlayerPreview("LeftPlayer", playerAreaGO.transform, 
                new Vector2(-200, 0), new Vector2(300, 300), "P1");

            // Right Player Preview
            GameObject rightPreviewGO = CreatePlayerPreview("RightPlayer", playerAreaGO.transform, 
                new Vector2(200, 0), new Vector2(300, 300), "P2");
        }

        static void CreateCenterGameArea(GameObject parent)
        {
            GameObject centerAreaGO = new GameObject("CenterGameArea");
            centerAreaGO.transform.SetParent(parent.transform, false);
            
            RectTransform centerRect = centerAreaGO.AddComponent<RectTransform>();
            centerRect.anchorMin = new Vector2(0.5f, 0.5f);
            centerRect.anchorMax = new Vector2(0.5f, 0.5f);
            centerRect.sizeDelta = new Vector2(400, 400);
            centerRect.anchoredPosition = Vector2.zero;

            // Object Icon
            GameObject objectIconGO = new GameObject("ObjectIcon");
            objectIconGO.transform.SetParent(centerAreaGO.transform, false);
            
            RectTransform objectIconRect = objectIconGO.AddComponent<RectTransform>();
            objectIconRect.anchorMin = new Vector2(0.5f, 0.6f);
            objectIconRect.anchorMax = new Vector2(0.5f, 0.6f);
            objectIconRect.sizeDelta = new Vector2(200, 200);
            objectIconRect.anchoredPosition = Vector2.zero;
            
            Image objectIcon = objectIconGO.AddComponent<Image>();
            objectIcon.color = Color.white;
            objectIcon.sprite = CreateSimpleSprite("OBJECT");

            // Countdown Text
            GameObject countdownGO = new GameObject("CountdownText");
            countdownGO.transform.SetParent(centerAreaGO.transform, false);
            
            RectTransform countdownRect = countdownGO.AddComponent<RectTransform>();
            countdownRect.anchorMin = new Vector2(0.5f, 0.4f);
            countdownRect.anchorMax = new Vector2(0.5f, 0.4f);
            countdownRect.sizeDelta = new Vector2(100, 80);
            countdownRect.anchoredPosition = Vector2.zero;
            
            TMP_Text countdownText = countdownGO.AddComponent<TextMeshProUGUI>();
            countdownText.text = "3";
            countdownText.fontSize = 64;
            countdownText.color = Color.white;
            countdownText.alignment = TextAlignmentOptions.Center;
            countdownText.fontStyle = FontStyles.Bold;

            // Results Group
            GameObject resultsGroupGO = new GameObject("ResultsGroup");
            resultsGroupGO.transform.SetParent(centerAreaGO.transform, false);
            
            RectTransform resultsRect = resultsGroupGO.AddComponent<RectTransform>();
            resultsRect.anchorMin = new Vector2(0.5f, 0.1f);
            resultsRect.anchorMax = new Vector2(0.5f, 0.1f);
            resultsRect.sizeDelta = new Vector2(350, 100);
            resultsRect.anchoredPosition = Vector2.zero;

            // P1 Result
            GameObject p1ResultGO = CreateResultDisplay("P1Result", resultsGroupGO.transform, 
                new Vector2(-100, 0), new Vector2(120, 80), "P1: 85%");

            // P2 Result
            GameObject p2ResultGO = CreateResultDisplay("P2Result", resultsGroupGO.transform, 
                new Vector2(100, 0), new Vector2(120, 80), "P2: 92%");
        }

        static void CreateBottomControls(GameObject parent)
        {
            GameObject bottomControlsGO = new GameObject("BottomControls");
            bottomControlsGO.transform.SetParent(parent.transform, false);
            
            RectTransform bottomRect = bottomControlsGO.AddComponent<RectTransform>();
            bottomRect.anchorMin = new Vector2(0, 0);
            bottomRect.anchorMax = new Vector2(1, 0.4f);
            bottomRect.offsetMin = Vector2.zero;
            bottomRect.offsetMax = Vector2.zero;

            // Scoreboard
            GameObject scoreboardGO = new GameObject("Scoreboard");
            scoreboardGO.transform.SetParent(bottomControlsGO.transform, false);
            
            RectTransform scoreboardRect = scoreboardGO.AddComponent<RectTransform>();
            scoreboardRect.anchorMin = new Vector2(0.5f, 0.7f);
            scoreboardRect.anchorMax = new Vector2(0.5f, 0.7f);
            scoreboardRect.sizeDelta = new Vector2(300, 60);
            scoreboardRect.anchoredPosition = Vector2.zero;
            
            TMP_Text scoreboardText = scoreboardGO.AddComponent<TextMeshProUGUI>();
            scoreboardText.text = "P1 0 — 0 P2";
            scoreboardText.fontSize = 32;
            scoreboardText.color = Color.white;
            scoreboardText.alignment = TextAlignmentOptions.Center;
            scoreboardText.fontStyle = FontStyles.Bold;

            // Start Button
            GameObject startButtonGO = CreateModernButton("Start", "Start Game", bottomControlsGO.transform, 
                new Vector2(0, 0.3f), new Vector2(250, 80), new Color(0.2f, 0.8f, 0.2f, 1f));
        }

        static void CreateEconomyUI(GameObject parent)
        {
            GameObject economyGO = new GameObject("EconomyUI");
            economyGO.transform.SetParent(parent.transform, false);
            
            RectTransform economyRect = economyGO.AddComponent<RectTransform>();
            economyRect.anchorMin = Vector2.zero;
            economyRect.anchorMax = Vector2.one;
            economyRect.offsetMin = Vector2.zero;
            economyRect.offsetMax = Vector2.zero;

            // Gold indicator (top right)
            GameObject goldIndicatorGO = new GameObject("GoldIndicator");
            goldIndicatorGO.transform.SetParent(economyGO.transform, false);
            
            RectTransform goldIndicatorRect = goldIndicatorGO.AddComponent<RectTransform>();
            goldIndicatorRect.anchorMin = new Vector2(0.85f, 0.9f);
            goldIndicatorRect.anchorMax = new Vector2(0.95f, 0.98f);
            goldIndicatorRect.offsetMin = Vector2.zero;
            goldIndicatorRect.offsetMax = Vector2.zero;
            
            Image goldIndicatorBg = goldIndicatorGO.AddComponent<Image>();
            goldIndicatorBg.color = new Color(1f, 0.8f, 0f, 0.9f);
            
            TMP_Text goldText = goldIndicatorGO.AddComponent<TextMeshProUGUI>();
            goldText.text = "5🪙";
            goldText.fontSize = 20;
            goldText.color = Color.white;
            goldText.alignment = TextAlignmentOptions.Center;
        }

        static void CreatePopupSystem(GameObject parent)
        {
            GameObject popupSystemGO = new GameObject("PopupSystem");
            popupSystemGO.transform.SetParent(parent.transform, false);
            
            RectTransform popupRect = popupSystemGO.AddComponent<RectTransform>();
            popupRect.anchorMin = Vector2.zero;
            popupRect.anchorMax = Vector2.one;
            popupRect.offsetMin = Vector2.zero;
            popupRect.offsetMax = Vector2.zero;

            // Insufficient Gold Panel
            GameObject insufficientGoldPanelGO = CreateInsufficientGoldPanel(popupSystemGO.transform);
        }

        static GameObject CreateModernButton(string name, string text, Transform parent, Vector2 position, Vector2 size, Color color)
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

        static GameObject CreatePlayerPreview(string name, Transform parent, Vector2 position, Vector2 size, string playerLabel)
        {
            GameObject previewGO = new GameObject(name);
            previewGO.transform.SetParent(parent, false);
            
            RectTransform previewRect = previewGO.AddComponent<RectTransform>();
            previewRect.anchorMin = new Vector2(0.5f, 0.5f);
            previewRect.anchorMax = new Vector2(0.5f, 0.5f);
            previewRect.sizeDelta = size;
            previewRect.anchoredPosition = position;
            
            // Preview background
            Image previewBg = previewGO.AddComponent<Image>();
            previewBg.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
            
            // Player label
            GameObject labelGO = new GameObject("PlayerLabel");
            labelGO.transform.SetParent(previewGO.transform, false);
            
            RectTransform labelRect = labelGO.AddComponent<RectTransform>();
            labelRect.anchorMin = new Vector2(0, 0.8f);
            labelRect.anchorMax = new Vector2(1, 1);
            labelRect.offsetMin = Vector2.zero;
            labelRect.offsetMax = Vector2.zero;
            
            TMP_Text labelText = labelGO.AddComponent<TextMeshProUGUI>();
            labelText.text = playerLabel;
            labelText.fontSize = 20;
            labelText.color = Color.white;
            labelText.alignment = TextAlignmentOptions.Center;
            labelText.fontStyle = FontStyles.Bold;

            return previewGO;
        }

        static GameObject CreateResultDisplay(string name, Transform parent, Vector2 position, Vector2 size, string text)
        {
            GameObject resultGO = new GameObject(name);
            resultGO.transform.SetParent(parent, false);
            
            RectTransform resultRect = resultGO.AddComponent<RectTransform>();
            resultRect.anchorMin = new Vector2(0.5f, 0.5f);
            resultRect.anchorMax = new Vector2(0.5f, 0.5f);
            resultRect.sizeDelta = size;
            resultRect.anchoredPosition = position;
            
            Image resultBg = resultGO.AddComponent<Image>();
            resultBg.color = new Color(0f, 0f, 0f, 0.7f);
            
            TMP_Text resultText = resultGO.AddComponent<TextMeshProUGUI>();
            resultText.text = text;
            resultText.fontSize = 24;
            resultText.color = Color.white;
            resultText.alignment = TextAlignmentOptions.Center;
            resultText.fontStyle = FontStyles.Bold;

            return resultGO;
        }

        static GameObject CreateInsufficientGoldPanel(Transform parent)
        {
            GameObject panelGO = new GameObject("InsufficientGoldPanel");
            panelGO.transform.SetParent(parent, false);
            
            RectTransform panelRect = panelGO.AddComponent<RectTransform>();
            panelRect.anchorMin = Vector2.zero;
            panelRect.anchorMax = Vector2.one;
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;
            
            Image panelBg = panelGO.AddComponent<Image>();
            panelBg.color = new Color(0f, 0f, 0f, 0.8f);
            
            // Panel content
            GameObject contentGO = new GameObject("Content");
            contentGO.transform.SetParent(panelGO.transform, false);
            
            RectTransform contentRect = contentGO.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0.5f, 0.5f);
            contentRect.anchorMax = new Vector2(0.5f, 0.5f);
            contentRect.sizeDelta = new Vector2(400, 300);
            contentRect.anchoredPosition = Vector2.zero;
            
            Image contentBg = contentGO.AddComponent<Image>();
            contentBg.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
            
            // Title
            GameObject titleGO = new GameObject("Title");
            titleGO.transform.SetParent(contentGO.transform, false);
            
            RectTransform titleRect = titleGO.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.7f);
            titleRect.anchorMax = new Vector2(0.5f, 0.7f);
            titleRect.sizeDelta = new Vector2(350, 60);
            titleRect.anchoredPosition = Vector2.zero;
            
            TMP_Text titleText = titleGO.AddComponent<TextMeshProUGUI>();
            titleText.text = "Yetersiz Altın!";
            titleText.fontSize = 28;
            titleText.color = Color.red;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.fontStyle = FontStyles.Bold;
            
            // Message
            GameObject messageGO = new GameObject("Message");
            messageGO.transform.SetParent(contentGO.transform, false);
            
            RectTransform messageRect = messageGO.AddComponent<RectTransform>();
            messageRect.anchorMin = new Vector2(0.5f, 0.5f);
            messageRect.anchorMax = new Vector2(0.5f, 0.5f);
            messageRect.sizeDelta = new Vector2(350, 80);
            messageRect.anchoredPosition = Vector2.zero;
            
            TMP_Text messageText = messageGO.AddComponent<TextMeshProUGUI>();
            messageText.text = "Oyunu oynamak için altın gerekiyor.\nReklam izleyerek +5🪙 kazanabilirsiniz!";
            messageText.fontSize = 18;
            messageText.color = Color.white;
            messageText.alignment = TextAlignmentOptions.Center;
            
            // Watch Ad Button
            GameObject watchAdButtonGO = CreateModernButton("WatchAdButton", "Reklam İzle (+5🪙)", contentGO.transform, 
                new Vector2(0, -50), new Vector2(250, 60), new Color(0.2f, 0.8f, 0.2f, 1f));
            
            // Close Button
            GameObject closeButtonGO = CreateModernButton("CloseButton", "Kapat", contentGO.transform, 
                new Vector2(0, -120), new Vector2(150, 50), new Color(0.6f, 0.6f, 0.6f, 1f));
            
            // Initially hidden
            panelGO.SetActive(false);
            
            return panelGO;
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
