using UnityEngine;

namespace Game.Services
{
    public interface ICameraManager
    {
        Camera LeftCamera { get; }
        Camera RightCamera { get; }
        Camera MainCamera { get; }

        void EnablePlayerCameras(bool enable);
        void SwitchToMainCamera();
        void SwitchToPlayerCameras();
        void SetCameraPositions(Vector3 leftPos, Vector3 rightPos);
        void SetCameraRotations(Quaternion leftRot, Quaternion rightRot);
    }
}
