using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Game.UI.Prefabs;
using Game.UI.Themes;

namespace Game.UI.SceneManager
{
    public sealed class UISceneManager : MonoBehaviour
    {
        [Header("UI Scenes")]
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private GameObject gameCanvas;
        [SerializeField] private GameObject loadingCanvas;

        [Header("Scene Settings")]
        [SerializeField] private float loadingDuration = 2f;

        private GameObject _currentCanvas;
        private bool _isLoading = false;

        void Awake()
        {
            InitializeUI();
        }

        void InitializeUI()
        {
            // Create sweet UI canvases if not assigned
            if (mainMenuCanvas == null)
                mainMenuCanvas = SweetMainMenuUI.CreateSweetMainMenuCanvas();
            
            if (gameCanvas == null)
                gameCanvas = SweetGameUI.CreateSweetGameCanvas();
            
            if (loadingCanvas == null)
                loadingCanvas = SweetLoadingUI.CreateSweetLoadingCanvas();

            // Start with sweet main menu
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            HideAllCanvases();
            mainMenuCanvas.SetActive(true);
            _currentCanvas = mainMenuCanvas;
        }

        public void ShowGame()
        {
            HideAllCanvases();
            gameCanvas.SetActive(true);
            _currentCanvas = gameCanvas;
        }

        public void ShowLoading()
        {
            HideAllCanvases();
            loadingCanvas.SetActive(true);
            _currentCanvas = loadingCanvas;
        }

        public void LoadGameScene()
        {
            if (_isLoading) return;

            StartCoroutine(LoadGameSceneCoroutine());
        }

        System.Collections.IEnumerator LoadGameSceneCoroutine()
        {
            _isLoading = true;
            
            // Show loading screen
            ShowLoading();
            
            // Simulate loading time
            yield return new WaitForSeconds(loadingDuration);
            
            // Load game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
            
            _isLoading = false;
        }

        public void HideAll()
        {
            HideAllCanvases();
            _currentCanvas = null;
        }

        void HideAllCanvases()
        {
            if (mainMenuCanvas != null)
                mainMenuCanvas.SetActive(false);
            if (gameCanvas != null)
                gameCanvas.SetActive(false);
            if (loadingCanvas != null)
                loadingCanvas.SetActive(false);
        }

        GameObject CreateLoadingCanvas()
        {
            GameObject loadingGO = new GameObject("LoadingCanvas");
            Canvas canvas = loadingGO.AddComponent<Canvas>();
            CanvasScaler scaler = loadingGO.AddComponent<CanvasScaler>();
            GraphicRaycaster raycaster = loadingGO.AddComponent<GraphicRaycaster>();

            // Setup canvas
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            // Background
            GameObject bgGO = new GameObject("Background");
            bgGO.transform.SetParent(loadingGO.transform, false);
            
            RectTransform bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            
            Image bgImage = bgGO.AddComponent<Image>();
            bgImage.color = new Color(0.1f, 0.1f, 0.2f, 1f);

            // Loading text
            GameObject loadingTextGO = new GameObject("LoadingText");
            loadingTextGO.transform.SetParent(loadingGO.transform, false);
            
            RectTransform loadingTextRect = loadingTextGO.AddComponent<RectTransform>();
            loadingTextRect.anchorMin = new Vector2(0.5f, 0.5f);
            loadingTextRect.anchorMax = new Vector2(0.5f, 0.5f);
            loadingTextRect.sizeDelta = new Vector2(400, 100);
            loadingTextRect.anchoredPosition = Vector2.zero;
            
            TMP_Text loadingText = loadingTextGO.AddComponent<TextMeshProUGUI>();
            loadingText.text = "YÜKLENİYOR...";
            loadingText.fontSize = 48;
            loadingText.color = Color.white;
            loadingText.alignment = TextAlignmentOptions.Center;
            loadingText.fontStyle = FontStyles.Bold;

            // Loading spinner
            GameObject spinnerGO = new GameObject("Spinner");
            spinnerGO.transform.SetParent(loadingGO.transform, false);
            
            RectTransform spinnerRect = spinnerGO.AddComponent<RectTransform>();
            spinnerRect.anchorMin = new Vector2(0.5f, 0.4f);
            spinnerRect.anchorMax = new Vector2(0.5f, 0.4f);
            spinnerRect.sizeDelta = new Vector2(80, 80);
            spinnerRect.anchoredPosition = Vector2.zero;
            
            Image spinnerImage = spinnerGO.AddComponent<Image>();
            spinnerImage.color = new Color(1f, 0.8f, 0f, 1f);
            spinnerImage.sprite = CreateSimpleSprite("SPINNER");

            // Add rotation animation
            SpinnerAnimation spinnerAnim = spinnerGO.AddComponent<SpinnerAnimation>();

            return loadingGO;
        }

        static Sprite CreateSimpleSprite(string name)
        {
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

    public sealed class SpinnerAnimation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 360f;

        void Update()
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}
