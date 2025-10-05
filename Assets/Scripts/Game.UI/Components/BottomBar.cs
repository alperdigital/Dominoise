using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Core;

namespace Game.UI
{
    public sealed class BottomBar : MonoBehaviour
    {
        [Header("Bottom Bar References")]
        [SerializeField] private TMP_Text scoreboard;
        [SerializeField] private Button btnStart;

        private GameFlow _gameFlow;

        public void Initialize(GameFlow gameFlow)
        {
            _gameFlow = gameFlow;

            if (btnStart != null)
                btnStart.onClick.AddListener(OnStartClicked);

            UpdateScoreboard(0, 0);
        }

        void OnStartClicked()
        {
            _gameFlow?.RequestStart();
        }

        public void UpdateScoreboard(int p1Score, int p2Score)
        {
            if (scoreboard != null)
                scoreboard.text = $"P1 {p1Score} — {p2Score} P2";
        }

        public void SetStartButtonActive(bool active)
        {
            if (btnStart != null)
                btnStart.interactable = active;
        }
    }
}
