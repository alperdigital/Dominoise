using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Services;

public sealed class UIBindings : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text scoreboard;
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnVS;
    [SerializeField] private Button btnCoop;

    Game.Core.GameFlow _flow;

    void Awake()
    {
        _flow = FindFirstObjectByType<Game.Core.GameFlow>();
        btnCoop.interactable = false;
        btnStart.onClick.AddListener(OnClickStart);
        btnVS.onClick.AddListener(OnClickStart);

        var bus = Service.Get<IEventBus>();
        bus.Subscribe<UiEvents.CountdownShow>(OnCountdownShow);
        bus.Subscribe<UiEvents.CountdownTick>(OnCountdownTick);
        bus.Subscribe<UiEvents.CountdownHide>(_=> countdownText.text = "");
        bus.Subscribe<UiEvents.UpdateScoreboard>(OnScoreboard);
    }

    void OnClickStart() => _flow.RequestStart();

    void OnCountdownShow(UiEvents.CountdownShow e) => countdownText.text = e.seconds.ToString();
    void OnCountdownTick(UiEvents.CountdownTick e) => countdownText.text = e.value.ToString();

    void OnScoreboard(UiEvents.UpdateScoreboard e)
    {
        // e.a/e.b -1 ise sadece "refresh" talebi olabilir; basit tutuyoruz
        // gerçek skoru ScoringState Publish ediyor
        if (e.a >= 0 && e.b >= 0)
            scoreboard.text = $"P1 {e.a} — {e.b} P2";
    }
}