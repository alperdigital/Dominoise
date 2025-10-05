using UnityEngine;
using Game.Services;

namespace Game.Services
{
    public sealed class CameraManager : MonoBehaviour, ICameraManager
    {
        [Header("Camera References")]
        [SerializeField] private Camera leftCamera;
        [SerializeField] private Camera rightCamera;
        [SerializeField] private Camera mainCamera;

        [Header("Camera Settings")]
        [SerializeField] private float cameraSwitchDuration = 1f;
        [SerializeField] private AnimationCurve switchCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private bool _isSwitching = false;

        public Camera LeftCamera => leftCamera;
        public Camera RightCamera => rightCamera;
        public Camera MainCamera => mainCamera;

        void Awake()
        {
            InitializeCameras();
        }

        void InitializeCameras()
        {
            // Setup camera properties
            if (leftCamera != null)
            {
                leftCamera.enabled = false;
                leftCamera.depth = 1;
            }

            if (rightCamera != null)
            {
                rightCamera.enabled = false;
                rightCamera.depth = 1;
            }

            if (mainCamera != null)
            {
                mainCamera.enabled = true;
                mainCamera.depth = 0;
            }
        }

        public void EnablePlayerCameras(bool enable)
        {
            if (leftCamera != null)
                leftCamera.enabled = enable;
            if (rightCamera != null)
                rightCamera.enabled = enable;
        }

        public void SwitchToMainCamera()
        {
            if (_isSwitching) return;

            StartCoroutine(SwitchCameraCoroutine(mainCamera));
        }

        public void SwitchToPlayerCameras()
        {
            if (_isSwitching) return;

            StartCoroutine(SwitchToPlayerCamerasCoroutine());
        }

        System.Collections.IEnumerator SwitchCameraCoroutine(Camera targetCamera)
        {
            _isSwitching = true;

            // Disable all cameras first
            if (leftCamera != null) leftCamera.enabled = false;
            if (rightCamera != null) rightCamera.enabled = false;
            if (mainCamera != null) mainCamera.enabled = false;

            // Enable target camera
            if (targetCamera != null)
                targetCamera.enabled = true;

            yield return new WaitForSeconds(cameraSwitchDuration);

            _isSwitching = false;
        }

        System.Collections.IEnumerator SwitchToPlayerCamerasCoroutine()
        {
            _isSwitching = true;

            // Disable main camera
            if (mainCamera != null) mainCamera.enabled = false;

            // Enable player cameras
            if (leftCamera != null) leftCamera.enabled = true;
            if (rightCamera != null) rightCamera.enabled = true;

            yield return new WaitForSeconds(cameraSwitchDuration);

            _isSwitching = false;
        }

        public void SetCameraPositions(Vector3 leftPos, Vector3 rightPos)
        {
            if (leftCamera != null)
                leftCamera.transform.position = leftPos;
            if (rightCamera != null)
                rightCamera.transform.position = rightPos;
        }

        public void SetCameraRotations(Quaternion leftRot, Quaternion rightRot)
        {
            if (leftCamera != null)
                leftCamera.transform.rotation = leftRot;
            if (rightCamera != null)
                rightCamera.transform.rotation = rightRot;
        }
    }
}
