using UnityEngine;
using Game.Services.PoseDetection;

namespace Game.Services.Camera
{
    /// <summary>
    /// Oyuncu alanı - poz tespiti ve kamera yönetimi
    /// </summary>
    public sealed class PlayerArea : MonoBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private int playerId;
        [SerializeField] private UnityEngine.Camera playerCamera;
        
        [Header("Pose Detection")]
        [SerializeField] private bool isPoseDetected = false;
        [SerializeField] private PoseData currentPose;
        
        private float _lastPoseUpdateTime;
        private const float POSE_UPDATE_INTERVAL = 0.1f; // 10 FPS
        
        public int PlayerId => playerId;
        public bool IsPoseDetected => isPoseDetected;
        public PoseData CurrentPose => currentPose;
        
        public void Initialize(UnityEngine.Camera camera, int id)
        {
            playerCamera = camera;
            playerId = id;
            
            // Kamera ayarları
            if (playerCamera != null)
            {
                playerCamera.enabled = false; // Başlangıçta kapalı
                playerCamera.backgroundColor = Color.black;
            }
            
            UnityEngine.Debug.Log($"👤 Player {playerId} area initialized");
        }
        
        public PoseData GetCurrentPose()
        {
            return currentPose;
        }
        
        void Update()
        {
            // Poz tespiti simülasyonu (gerçek implementasyon için MediaPipe kullanılacak)
            if (Time.time - _lastPoseUpdateTime > POSE_UPDATE_INTERVAL)
            {
                SimulatePoseDetection();
                _lastPoseUpdateTime = Time.time;
            }
        }
        
        void SimulatePoseDetection()
        {
            // Gerçek implementasyonda burada MediaPipe pose detection olacak
            // Şimdilik rastgele poz simülasyonu yapıyoruz
            
            if (Random.Range(0f, 1f) > 0.3f) // %70 şansla poz tespit edilir
            {
                currentPose = GenerateRandomPose();
                isPoseDetected = true;
            }
            else
            {
                isPoseDetected = false;
                currentPose = null;
            }
        }
        
        PoseData GenerateRandomPose()
        {
            var pose = new PoseData();
            
            // 17 temel pose landmark'ı (MediaPipe standardı)
            for (int i = 0; i < 17; i++)
            {
                Vector2 position = new Vector2(
                    Random.Range(0.2f, 0.8f), // X: 0.2-0.8 arası
                    Random.Range(0.2f, 0.8f)  // Y: 0.2-0.8 arası
                );
                
                float confidence = Random.Range(0.5f, 1f);
                pose.landmarks.Add(new PoseLandmark(position, confidence, i));
            }
            
            return pose;
        }
        
        public void SetTargetPose(PoseData targetPose)
        {
            // Hedef poz ayarlandığında burada işlemler yapılabilir
            UnityEngine.Debug.Log($"🎯 Player {playerId} target pose set");
        }
        
        public void ResetPose()
        {
            currentPose = null;
            isPoseDetected = false;
            UnityEngine.Debug.Log($"🔄 Player {playerId} pose reset");
        }
        
        void OnDestroy()
        {
            if (playerCamera != null)
            {
                playerCamera.enabled = false;
            }
        }
    }
}
