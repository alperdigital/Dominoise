using UnityEngine;
using UnityEngine.UI;
using Game.Services;

namespace Game.UI
{
    public sealed class PlayerArea : MonoBehaviour
    {
        [Header("Player Area References")]
        [SerializeField] private RawImage previewLeft;
        [SerializeField] private RawImage previewRight;
        [SerializeField] private Camera leftCamera;
        [SerializeField] private Camera rightCamera;

        private RenderTexture _leftRenderTexture;
        private RenderTexture _rightRenderTexture;

        public void Initialize()
        {
            SetupCameras();
            SetupPreviewImages();
        }

        void SetupCameras()
        {
            // Create render textures for camera previews
            _leftRenderTexture = new RenderTexture(512, 512, 24);
            _rightRenderTexture = new RenderTexture(512, 512, 24);

            if (leftCamera != null)
            {
                leftCamera.targetTexture = _leftRenderTexture;
                leftCamera.enabled = true;
            }

            if (rightCamera != null)
            {
                rightCamera.targetTexture = _rightRenderTexture;
                rightCamera.enabled = true;
            }
        }

        void SetupPreviewImages()
        {
            if (previewLeft != null)
            {
                previewLeft.texture = _leftRenderTexture;
                previewLeft.color = Color.white;
            }

            if (previewRight != null)
            {
                previewRight.texture = _rightRenderTexture;
                previewRight.color = Color.white;
            }
        }

        public void SetLeftCamera(Camera camera)
        {
            leftCamera = camera;
            if (leftCamera != null)
            {
                leftCamera.targetTexture = _leftRenderTexture;
                leftCamera.enabled = true;
            }
        }

        public void SetRightCamera(Camera camera)
        {
            rightCamera = camera;
            if (rightCamera != null)
            {
                rightCamera.targetTexture = _rightRenderTexture;
                rightCamera.enabled = true;
            }
        }

        public void EnableCameras(bool enable)
        {
            if (leftCamera != null)
                leftCamera.enabled = enable;
            if (rightCamera != null)
                rightCamera.enabled = enable;
        }

        void OnDestroy()
        {
            if (_leftRenderTexture != null)
            {
                _leftRenderTexture.Release();
                Destroy(_leftRenderTexture);
            }

            if (_rightRenderTexture != null)
            {
                _rightRenderTexture.Release();
                Destroy(_rightRenderTexture);
            }
        }
    }
}
