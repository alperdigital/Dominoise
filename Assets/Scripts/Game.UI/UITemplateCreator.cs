using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public static class UITemplateCreator
    {
        public static GameObject CreateMainUICanvas()
        {
            // Create main canvas
            GameObject canvasGO = new GameObject("Canvas");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
            GraphicRaycaster raycaster = canvasGO.AddComponent<GraphicRaycaster>();

            // Setup canvas
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            // Create UI Manager
            UIManager uiManager = canvasGO.AddComponent<UIManager>();

            // Create UI hierarchy
            CreateTopBar(canvasGO, uiManager);
            CreateModeRow(canvasGO, uiManager);
            CreatePlayerArea(canvasGO, uiManager);
            CreateCenterCard(canvasGO, uiManager);
            CreateBottomBar(canvasGO, uiManager);
            CreateAudio(canvasGO, uiManager);

            return canvasGO;
        }

        static void CreateTopBar(GameObject parent, UIManager uiManager)
        {
            GameObject topBarGO = new GameObject("TopBar");
            topBarGO.transform.SetParent(parent.transform, false);
            
            RectTransform topBarRect = topBarGO.AddComponent<RectTransform>();
            topBarRect.anchorMin = new Vector2(0, 0.9f);
            topBarRect.anchorMax = new Vector2(1, 1);
            topBarRect.offsetMin = Vector2.zero;
            topBarRect.offsetMax = Vector2.zero;

            TopBar topBar = topBarGO.AddComponent<TopBar>();
            
            // Create logo image
            GameObject logoGO = new GameObject("Logo");
            logoGO.transform.SetParent(topBarGO.transform, false);
            
            RectTransform logoRect = logoGO.AddComponent<RectTransform>();
            logoRect.anchorMin = new Vector2(0.5f, 0.5f);
            logoRect.anchorMax = new Vector2(0.5f, 0.5f);
            logoRect.sizeDelta = new Vector2(200, 80);
            logoRect.anchoredPosition = Vector2.zero;
            
            Image logoImage = logoGO.AddComponent<Image>();
            logoImage.color = Color.white;

            // Set references
            var topBarField = typeof(TopBar).GetField("logoImage", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            topBarField?.SetValue(topBar, logoImage);

            var uiManagerField = typeof(UIManager).GetField("topBar", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            uiManagerField?.SetValue(uiManager, topBar);
        }

        static void CreateModeRow(GameObject parent, UIManager uiManager)
        {
            GameObject modeRowGO = new GameObject("ModeRow");
            modeRowGO.transform.SetParent(parent.transform, false);
            
            RectTransform modeRowRect = modeRowGO.AddComponent<RectTransform>();
            modeRowRect.anchorMin = new Vector2(0, 0.8f);
            modeRowRect.anchorMax = new Vector2(1, 0.9f);
            modeRowRect.offsetMin = Vector2.zero;
            modeRowRect.offsetMax = Vector2.zero;

            ModeRow modeRow = modeRowGO.AddComponent<ModeRow>();

            // Create VS Button
            GameObject vsButtonGO = CreateButton("VS", modeRowGO.transform, new Vector2(-100, 0), new Vector2(150, 60));
            Button vsButton = vsButtonGO.GetComponent<Button>();

            // Create Co-op Button
            GameObject coopButtonGO = CreateButton("Co-op", modeRowGO.transform, new Vector2(100, 0), new Vector2(150, 60));
            Button coopButton = coopButtonGO.GetComponent<Button>();

            // Set references
            var vsButtonField = typeof(ModeRow).GetField("btnVS", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            vsButtonField?.SetValue(modeRow, vsButton);

            var coopButtonField = typeof(ModeRow).GetField("btnCoop", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            coopButtonField?.SetValue(modeRow, coopButton);

            var uiManagerField = typeof(UIManager).GetField("modeRow", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            uiManagerField?.SetValue(uiManager, modeRow);
        }

        static void CreatePlayerArea(GameObject parent, UIManager uiManager)
        {
            GameObject playerAreaGO = new GameObject("PlayerArea");
            playerAreaGO.transform.SetParent(parent.transform, false);
            
            RectTransform playerAreaRect = playerAreaGO.AddComponent<RectTransform>();
            playerAreaRect.anchorMin = new Vector2(0, 0.3f);
            playerAreaRect.anchorMax = new Vector2(1, 0.8f);
            playerAreaRect.offsetMin = Vector2.zero;
            playerAreaRect.offsetMax = Vector2.zero;

            PlayerArea playerArea = playerAreaGO.AddComponent<PlayerArea>();

            // Create Left Preview
            GameObject leftPreviewGO = CreateRawImage("PreviewLeft", playerAreaGO.transform, 
                new Vector2(-200, 0), new Vector2(300, 300));
            RawImage leftPreview = leftPreviewGO.GetComponent<RawImage>();

            // Create Right Preview
            GameObject rightPreviewGO = CreateRawImage("PreviewRight", playerAreaGO.transform, 
                new Vector2(200, 0), new Vector2(300, 300));
            RawImage rightPreview = rightPreviewGO.GetComponent<RawImage>();

            // Set references
            var leftPreviewField = typeof(PlayerArea).GetField("previewLeft", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            leftPreviewField?.SetValue(playerArea, leftPreview);

            var rightPreviewField = typeof(PlayerArea).GetField("previewRight", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            rightPreviewField?.SetValue(playerArea, rightPreview);

            var uiManagerField = typeof(UIManager).GetField("playerArea", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            uiManagerField?.SetValue(uiManager, playerArea);
        }

        static void CreateCenterCard(GameObject parent, UIManager uiManager)
        {
            GameObject centerCardGO = new GameObject("CenterCard");
            centerCardGO.transform.SetParent(parent.transform, false);
            
            RectTransform centerCardRect = centerCardGO.AddComponent<RectTransform>();
            centerCardRect.anchorMin = new Vector2(0.5f, 0.5f);
            centerCardRect.anchorMax = new Vector2(0.5f, 0.5f);
            centerCardRect.sizeDelta = new Vector2(400, 400);
            centerCardRect.anchoredPosition = Vector2.zero;

            CenterCard centerCard = centerCardGO.AddComponent<CenterCard>();

            // Create Object Icon
            GameObject objectIconGO = new GameObject("ObjectIcon");
            objectIconGO.transform.SetParent(centerCardGO.transform, false);
            
            RectTransform objectIconRect = objectIconGO.AddComponent<RectTransform>();
            objectIconRect.anchorMin = new Vector2(0.5f, 0.5f);
            objectIconRect.anchorMax = new Vector2(0.5f, 0.5f);
            objectIconRect.sizeDelta = new Vector2(200, 200);
            objectIconRect.anchoredPosition = Vector2.zero;
            
            Image objectIcon = objectIconGO.AddComponent<Image>();
            objectIcon.color = Color.white;

            // Create Countdown Text
            GameObject countdownGO = new GameObject("CountdownText");
            countdownGO.transform.SetParent(centerCardGO.transform, false);
            
            RectTransform countdownRect = countdownGO.AddComponent<RectTransform>();
            countdownRect.anchorMin = new Vector2(0.5f, 0.3f);
            countdownRect.anchorMax = new Vector2(0.5f, 0.3f);
            countdownRect.sizeDelta = new Vector2(100, 50);
            countdownRect.anchoredPosition = Vector2.zero;
            
            TMP_Text countdownText = countdownGO.AddComponent<TextMeshProUGUI>();
            countdownText.text = "3";
            countdownText.fontSize = 48;
            countdownText.color = Color.white;
            countdownText.alignment = TextAlignmentOptions.Center;

            // Create Percent Group
            GameObject percentGroupGO = new GameObject("PercentGroup");
            percentGroupGO.transform.SetParent(centerCardGO.transform, false);
            
            RectTransform percentGroupRect = percentGroupGO.AddComponent<RectTransform>();
            percentGroupRect.anchorMin = new Vector2(0.5f, 0.1f);
            percentGroupRect.anchorMax = new Vector2(0.5f, 0.1f);
            percentGroupRect.sizeDelta = new Vector2(300, 100);
            percentGroupRect.anchoredPosition = Vector2.zero;

            // Create P1 Percent
            GameObject p1PercentGO = CreateText("P1Percent", percentGroupGO.transform, 
                new Vector2(-75, 0), new Vector2(100, 50), "P1: 85%");

            // Create P2 Percent
            GameObject p2PercentGO = CreateText("P2Percent", percentGroupGO.transform, 
                new Vector2(75, 0), new Vector2(100, 50), "P2: 92%");

            // Set references
            var objectIconField = typeof(CenterCard).GetField("objectIcon", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            objectIconField?.SetValue(centerCard, objectIcon);

            var countdownField = typeof(CenterCard).GetField("countdownText", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            countdownField?.SetValue(centerCard, countdownText);

            var percentGroupField = typeof(CenterCard).GetField("percentGroup", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            percentGroupField?.SetValue(centerCard, percentGroupGO);

            var p1PercentField = typeof(CenterCard).GetField("p1Percent", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            p1PercentField?.SetValue(centerCard, p1PercentGO.GetComponent<TMP_Text>());

            var p2PercentField = typeof(CenterCard).GetField("p2Percent", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            p2PercentField?.SetValue(centerCard, p2PercentGO.GetComponent<TMP_Text>());

            var uiManagerField = typeof(UIManager).GetField("centerCard", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            uiManagerField?.SetValue(uiManager, centerCard);
        }

        static void CreateBottomBar(GameObject parent, UIManager uiManager)
        {
            GameObject bottomBarGO = new GameObject("BottomBar");
            bottomBarGO.transform.SetParent(parent.transform, false);
            
            RectTransform bottomBarRect = bottomBarGO.AddComponent<RectTransform>();
            bottomBarRect.anchorMin = new Vector2(0, 0);
            bottomBarRect.anchorMax = new Vector2(1, 0.3f);
            bottomBarRect.offsetMin = Vector2.zero;
            bottomBarRect.offsetMax = Vector2.zero;

            BottomBar bottomBar = bottomBarGO.AddComponent<BottomBar>();

            // Create Scoreboard
            GameObject scoreboardGO = CreateText("Scoreboard", bottomBarGO.transform, 
                new Vector2(0, 50), new Vector2(300, 50), "P1 0 — 0 P2");

            // Create Start Button
            GameObject startButtonGO = CreateButton("Start", bottomBarGO.transform, 
                new Vector2(0, -50), new Vector2(200, 60));

            // Set references
            var scoreboardField = typeof(BottomBar).GetField("scoreboard", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            scoreboardField?.SetValue(bottomBar, scoreboardGO.GetComponent<TMP_Text>());

            var startButtonField = typeof(BottomBar).GetField("btnStart", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            startButtonField?.SetValue(bottomBar, startButtonGO.GetComponent<Button>());

            var uiManagerField = typeof(UIManager).GetField("bottomBar", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            uiManagerField?.SetValue(uiManager, bottomBar);
        }

        static void CreateAudio(GameObject parent, UIManager uiManager)
        {
            GameObject audioGO = new GameObject("Audio");
            audioGO.transform.SetParent(parent.transform, false);
            
            AudioSource audioSource = audioGO.AddComponent<AudioSource>();
            Audio audio = audioGO.AddComponent<Audio>();

            // Set references
            var audioSourceField = typeof(Audio).GetField("shutterAudioSource", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            audioSourceField?.SetValue(audio, audioSource);

            var uiManagerField = typeof(UIManager).GetField("audioManager", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            uiManagerField?.SetValue(uiManager, audio);
        }

        static GameObject CreateButton(string name, Transform parent, Vector2 position, Vector2 size)
        {
            GameObject buttonGO = new GameObject(name);
            buttonGO.transform.SetParent(parent, false);
            
            RectTransform buttonRect = buttonGO.AddComponent<RectTransform>();
            buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
            buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
            buttonRect.sizeDelta = size;
            buttonRect.anchoredPosition = position;
            
            Image buttonImage = buttonGO.AddComponent<Image>();
            buttonImage.color = new Color(0.2f, 0.6f, 1f, 1f);
            
            Button button = buttonGO.AddComponent<Button>();
            button.targetGraphic = buttonImage;

            // Create button text
            GameObject textGO = new GameObject("Text");
            textGO.transform.SetParent(buttonGO.transform, false);
            
            RectTransform textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            
            TMP_Text buttonText = textGO.AddComponent<TextMeshProUGUI>();
            buttonText.text = name;
            buttonText.fontSize = 24;
            buttonText.color = Color.white;
            buttonText.alignment = TextAlignmentOptions.Center;

            return buttonGO;
        }

        static GameObject CreateText(string name, Transform parent, Vector2 position, Vector2 size, string text)
        {
            GameObject textGO = new GameObject(name);
            textGO.transform.SetParent(parent, false);
            
            RectTransform textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.sizeDelta = size;
            textRect.anchoredPosition = position;
            
            TMP_Text tmpText = textGO.AddComponent<TextMeshProUGUI>();
            tmpText.text = text;
            tmpText.fontSize = 24;
            tmpText.color = Color.white;
            tmpText.alignment = TextAlignmentOptions.Center;

            return textGO;
        }

        static GameObject CreateRawImage(string name, Transform parent, Vector2 position, Vector2 size)
        {
            GameObject rawImageGO = new GameObject(name);
            rawImageGO.transform.SetParent(parent, false);
            
            RectTransform rawImageRect = rawImageGO.AddComponent<RectTransform>();
            rawImageRect.anchorMin = new Vector2(0.5f, 0.5f);
            rawImageRect.anchorMax = new Vector2(0.5f, 0.5f);
            rawImageRect.sizeDelta = size;
            rawImageRect.anchoredPosition = position;
            
            RawImage rawImage = rawImageGO.AddComponent<RawImage>();
            rawImage.color = Color.gray;

            return rawImageGO;
        }
    }
}
