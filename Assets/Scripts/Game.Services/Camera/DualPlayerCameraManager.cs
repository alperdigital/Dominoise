using UnityEngine;
using Game.Services.PoseDetection;

namespace Game.Services.Camera
{
    /// <summary>
    /// 2 oyuncu için kamera yönetimi - sol ve sağ çerçeveler
    /// </summary>
    public sealed class DualPlayerCameraManager : MonoBehaviour
    {
        [Header("Camera Setup")]
        [SerializeField] private UnityEngine.Camera leftCamera;   // Oyuncu 1 (Sol)
        [SerializeField] private UnityEngine.Camera rightCamera;  // Oyuncu 2 (Sağ)
        [SerializeField] private UnityEngine.Camera mainCamera;    // Ana kamera
        
        [Header("Player Areas")]
        [SerializeField] private PlayerArea leftPlayerArea;
        [SerializeField] private PlayerArea rightPlayerArea;
        
        [Header("Pose Detection")]
        [SerializeField] private PoseDetector poseDetector;
        
        private bool _isGameActive = false;
        
        public UnityEngine.Camera LeftCamera => leftCamera;
        public UnityEngine.Camera RightCamera => rightCamera;
        public UnityEngine.Camera MainCamera => mainCamera;
        
        void Start()
        {
            SetupCameras();
            SetupPlayerAreas();
        }
        
        void SetupCameras()
        {
            // Sol kamera - Oyuncu 1 için
            if (leftCamera == null)
            {
                GameObject leftCamGO = new GameObject("LeftPlayerCamera");
                leftCamera = leftCamGO.AddComponent<UnityEngine.Camera>();
                leftCamera.rect = new Rect(0f, 0f, 0.5f, 1f); // Sol yarı
                leftCamera.backgroundColor = Color.black;
            }
            
            // Sağ kamera - Oyuncu 2 için
            if (rightCamera == null)
            {
                GameObject rightCamGO = new GameObject("RightPlayerCamera");
                rightCamera = rightCamGO.AddComponent<UnityEngine.Camera>();
                rightCamera.rect = new Rect(0.5f, 0f, 0.5f, 1f); // Sağ yarı
                rightCamera.backgroundColor = Color.black;
            }
            
            // Ana kamera - Ortadaki nesne için
            if (mainCamera == null)
            {
                GameObject mainCamGO = new GameObject("MainCamera");
                mainCamera = mainCamGO.AddComponent<UnityEngine.Camera>();
                mainCamera.rect = new Rect(0.25f, 0.25f, 0.5f, 0.5f); // Ortada küçük
                mainCamera.backgroundColor = Color.gray;
            }
            
            UnityEngine.Debug.Log("📷 Dual camera setup complete");
        }
        
        void SetupPlayerAreas()
        {
            // Sol oyuncu alanı
            if (leftPlayerArea == null)
            {
                GameObject leftAreaGO = new GameObject("LeftPlayerArea");
                leftPlayerArea = leftAreaGO.AddComponent<PlayerArea>();
                leftPlayerArea.Initialize(leftCamera, 1); // Oyuncu 1
            }
            
            // Sağ oyuncu alanı
            if (rightPlayerArea == null)
            {
                GameObject rightAreaGO = new GameObject("RightPlayerArea");
                rightPlayerArea = rightAreaGO.AddComponent<PlayerArea>();
                rightPlayerArea.Initialize(rightCamera, 2); // Oyuncu 2
            }
            
            UnityEngine.Debug.Log("👥 Player areas setup complete");
        }
        
        public void StartGame()
        {
            _isGameActive = true;
            EnablePlayerCameras(true);
            UnityEngine.Debug.Log("🎮 Game started - Player cameras active");
        }
        
        public void EndGame()
        {
            _isGameActive = false;
            EnablePlayerCameras(false);
            SwitchToMainCamera();
            UnityEngine.Debug.Log("🏁 Game ended - Switching to main camera");
        }
        
        public void EnablePlayerCameras(bool enable)
        {
            if (leftCamera != null) leftCamera.enabled = enable;
            if (rightCamera != null) rightCamera.enabled = enable;
            if (mainCamera != null) mainCamera.enabled = !enable; // Ana kamera tersine
            
            UnityEngine.Debug.Log($"📷 Player cameras: {(enable ? "ON" : "OFF")}");
        }
        
        public void SwitchToMainCamera()
        {
            EnablePlayerCameras(false);
            if (mainCamera != null) mainCamera.enabled = true;
            UnityEngine.Debug.Log("📷 Switched to main camera");
        }
        
        public PoseData GetPlayer1Pose()
        {
            return leftPlayerArea?.GetCurrentPose();
        }
        
        public PoseData GetPlayer2Pose()
        {
            return rightPlayerArea?.GetCurrentPose();
        }
        
        void Update()
        {
            if (_isGameActive && poseDetector != null)
            {
                var player1Pose = GetPlayer1Pose();
                var player2Pose = GetPlayer2Pose();
                
                if (player1Pose != null && player2Pose != null)
                {
                    poseDetector.UpdatePlayerPoses(player1Pose, player2Pose);
                }
            }
        }
    }
}
