using UnityEngine;
using System.Collections.Generic;

namespace Game.Services.PoseDetection
{
    /// <summary>
    /// Poz tespiti ve benzerlik hesaplama sistemi
    /// </summary>
    public sealed class PoseDetector : MonoBehaviour
    {
        [Header("Pose Detection Settings")]
        [SerializeField] private float similarityThreshold = 0.7f;
        
        private PoseData _targetPose;
        private PoseData _player1Pose;
        private PoseData _player2Pose;
        
        public bool IsPoseDetected { get; private set; }
        public float Player1Similarity { get; private set; }
        public float Player2Similarity { get; private set; }
        
        public void SetTargetPose(PoseData targetPose)
        {
            _targetPose = targetPose;
            UnityEngine.Debug.Log($"🎯 Target pose set: {targetPose.landmarks.Count} landmarks");
        }
        
        public void UpdatePlayerPoses(PoseData player1, PoseData player2)
        {
            _player1Pose = player1;
            _player2Pose = player2;
            
            if (_targetPose != null)
            {
                CalculateSimilarities();
            }
        }
        
        void CalculateSimilarities()
        {
            if (_targetPose == null || _player1Pose == null || _player2Pose == null)
                return;
                
            Player1Similarity = CalculatePoseSimilarity(_targetPose, _player1Pose);
            Player2Similarity = CalculatePoseSimilarity(_targetPose, _player2Pose);
            
            UnityEngine.Debug.Log($"📊 Similarities - P1: {Player1Similarity:F2}, P2: {Player2Similarity:F2}");
        }
        
        float CalculatePoseSimilarity(PoseData target, PoseData player)
        {
            if (target.landmarks.Count != player.landmarks.Count)
                return 0f;
                
            float totalDistance = 0f;
            int validPoints = 0;
            
            for (int i = 0; i < target.landmarks.Count; i++)
            {
                if (target.landmarks[i].confidence > 0.5f && player.landmarks[i].confidence > 0.5f)
                {
                    float distance = Vector2.Distance(target.landmarks[i].position, player.landmarks[i].position);
                    totalDistance += distance;
                    validPoints++;
                }
            }
            
            if (validPoints == 0) return 0f;
            
            float avgDistance = totalDistance / validPoints;
            float similarity = Mathf.Clamp01(1f - (avgDistance / 0.5f)); // 0.5f = max expected distance
            
            return similarity;
        }
        
        public int GetWinner()
        {
            if (Player1Similarity > Player2Similarity)
                return 1;
            else if (Player2Similarity > Player1Similarity)
                return 2;
            else
                return 0; // Tie
        }
        
        public bool IsValidPose()
        {
            return Player1Similarity > similarityThreshold || Player2Similarity > similarityThreshold;
        }
    }
    
    [System.Serializable]
    public class PoseData
    {
        public List<PoseLandmark> landmarks = new List<PoseLandmark>();
        public float timestamp;
        
        public PoseData()
        {
            landmarks = new List<PoseLandmark>();
            timestamp = Time.time;
        }
    }
    
    [System.Serializable]
    public class PoseLandmark
    {
        public Vector2 position;
        public float confidence;
        public int id;
        
        public PoseLandmark(Vector2 pos, float conf, int landmarkId)
        {
            position = pos;
            confidence = conf;
            id = landmarkId;
        }
    }
}
