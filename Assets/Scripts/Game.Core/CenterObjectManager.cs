using UnityEngine;
using Game.Services.PoseDetection;
using Game.Core;
using Game.Services;

namespace Game.Core
{
    /// <summary>
    /// Ortadaki nesne yönetimi - poz örnekleri ve hedef belirleme
    /// </summary>
    public sealed class CenterObjectManager : MonoBehaviour
    {
        [Header("Center Object Settings")]
        [SerializeField] private GameObject centerObjectPrefab;
        [SerializeField] private Transform centerObjectParent;
        [SerializeField] private float objectScale = 1f;
        
        [Header("Pose Library")]
        [SerializeField] private PoseData[] availablePoses;
        
        private GameObject _currentCenterObject;
        private PoseData _currentTargetPose;
        private int _currentPoseIndex = 0;
        
        public PoseData CurrentTargetPose => _currentTargetPose;
        public GameObject CurrentCenterObject => _currentCenterObject;
        
        void Start()
        {
            InitializePoseLibrary();
            CreateCenterObject();
        }
        
        void InitializePoseLibrary()
        {
            // Poz kütüphanesi oluştur (gerçek implementasyonda asset'lerden yüklenecek)
            availablePoses = new PoseData[]
            {
                CreatePoseData("Arms Up", new Vector2(0.5f, 0.8f), new Vector2(0.5f, 0.6f)),
                CreatePoseData("T-Pose", new Vector2(0.3f, 0.7f), new Vector2(0.7f, 0.7f)),
                CreatePoseData("Star Jump", new Vector2(0.4f, 0.8f), new Vector2(0.6f, 0.8f)),
                CreatePoseData("Squat", new Vector2(0.5f, 0.4f), new Vector2(0.5f, 0.3f)),
                CreatePoseData("Lunge", new Vector2(0.4f, 0.5f), new Vector2(0.6f, 0.5f))
            };
            
            UnityEngine.Debug.Log($"📚 Pose library initialized: {availablePoses.Length} poses");
        }
        
        PoseData CreatePoseData(string name, Vector2 leftHand, Vector2 rightHand)
        {
            var pose = new PoseData();
            
            // Basit 2 nokta poz (gerçek implementasyonda 17 landmark olacak)
            pose.landmarks.Add(new PoseLandmark(leftHand, 1f, 0));   // Sol el
            pose.landmarks.Add(new PoseLandmark(rightHand, 1f, 1));  // Sağ el
            
            return pose;
        }
        
        void CreateCenterObject()
        {
            if (centerObjectParent == null)
            {
                GameObject parentGO = new GameObject("CenterObjectParent");
                centerObjectParent = parentGO.transform;
                centerObjectParent.position = Vector3.zero;
            }
            
            if (centerObjectPrefab == null)
            {
                // Varsayılan nesne oluştur
                _currentCenterObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _currentCenterObject.name = "CenterObject";
                _currentCenterObject.transform.SetParent(centerObjectParent);
                _currentCenterObject.transform.localPosition = Vector3.zero;
                _currentCenterObject.transform.localScale = Vector3.one * objectScale;
                
                // Renk ayarla
                var renderer = _currentCenterObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.yellow;
                }
            }
            else
            {
                _currentCenterObject = Instantiate(centerObjectPrefab, centerObjectParent);
            }
            
            UnityEngine.Debug.Log("🎯 Center object created");
        }
        
        public void SetRandomTargetPose()
        {
            if (availablePoses == null || availablePoses.Length == 0)
            {
                UnityEngine.Debug.LogError("❌ No poses available!");
                return;
            }
            
            _currentPoseIndex = Random.Range(0, availablePoses.Length);
            _currentTargetPose = availablePoses[_currentPoseIndex];
            
            // UI'ye hedef poz bildir
            Service.Get<IEventBus>().Publish(new UiEvents.SetCenterIcon());
            
            UnityEngine.Debug.Log($"🎯 Target pose set: Index {_currentPoseIndex}");
        }
        
        public void SetSpecificTargetPose(int poseIndex)
        {
            if (availablePoses == null || poseIndex < 0 || poseIndex >= availablePoses.Length)
            {
                UnityEngine.Debug.LogError($"❌ Invalid pose index: {poseIndex}");
                return;
            }
            
            _currentPoseIndex = poseIndex;
            _currentTargetPose = availablePoses[_currentPoseIndex];
            
            Service.Get<IEventBus>().Publish(new UiEvents.SetCenterIcon());
            
            UnityEngine.Debug.Log($"🎯 Specific target pose set: Index {poseIndex}");
        }
        
        public void UpdateCenterObject()
        {
            if (_currentCenterObject != null)
            {
                // Nesneyi döndür (animasyon efekti)
                _currentCenterObject.transform.Rotate(0, 30f * Time.deltaTime, 0);
            }
        }
        
        public void HideCenterObject()
        {
            if (_currentCenterObject != null)
            {
                _currentCenterObject.SetActive(false);
            }
        }
        
        public void ShowCenterObject()
        {
            if (_currentCenterObject != null)
            {
                _currentCenterObject.SetActive(true);
            }
        }
        
        void Update()
        {
            UpdateCenterObject();
        }
        
        void OnDestroy()
        {
            if (_currentCenterObject != null)
            {
                Destroy(_currentCenterObject);
            }
        }
    }
}
