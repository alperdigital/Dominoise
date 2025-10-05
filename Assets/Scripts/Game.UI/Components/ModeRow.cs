using UnityEngine;
using UnityEngine.UI;
using Game.Core;

namespace Game.UI
{
    public sealed class ModeRow : MonoBehaviour
    {
        [Header("Mode Row References")]
        [SerializeField] private Button btnVS;
        [SerializeField] private Button btnCoop;

        private GameFlow _gameFlow;

        public void Initialize(GameFlow gameFlow)
        {
            _gameFlow = gameFlow;
            
            // Setup button listeners
            if (btnVS != null)
                btnVS.onClick.AddListener(OnVSClicked);
            
            if (btnCoop != null)
            {
                btnCoop.onClick.AddListener(OnCoopClicked);
                btnCoop.interactable = false; // Co-op is disabled for now
            }
        }

        void OnVSClicked()
        {
            _gameFlow?.RequestStart();
        }

        void OnCoopClicked()
        {
            // Co-op functionality will be implemented later
            UnityEngine.Debug.Log("Co-op mode clicked - not implemented yet");
        }

        public void SetVSMode(bool active)
        {
            if (btnVS != null)
                btnVS.interactable = active;
        }

        public void SetCoopMode(bool active)
        {
            if (btnCoop != null)
                btnCoop.interactable = active;
        }
    }
}
