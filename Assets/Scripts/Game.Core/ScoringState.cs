using UnityEngine;
using Game.Core;
using Game.Services;
using Game.SharedSo;

public sealed class ScoringState : IGameState
{
    readonly GameFlow _f;
    int _p1, _p2; static int s1, s2;

    // GameRulesSo erişimini ServiceLocator üzerinden yapıyoruz.
    private GameRulesSo Rules => Service.Get<GameRulesSo>(); 

    public ScoringState(GameFlow f){ _f=f; }

    public void Enter()
    {
        // Gerçek poz benzerlik hesaplama (şimdilik simülasyon)
        var poseDetector = Service.Get<Game.Services.PoseDetection.PoseDetector>();
        if (poseDetector != null)
        {
            _p1 = Mathf.RoundToInt(poseDetector.Player1Similarity * 100);
            _p2 = Mathf.RoundToInt(poseDetector.Player2Similarity * 100);
        }
        else
        {
            // Fallback: rastgele skor
            _p1 = Random.Range(60, 97);
            _p2 = Random.Range(60, 97);
        }
        
        Service.Get<IEventBus>().Publish(new UiEvents.ShowPercents(_p1, _p2));

        // skorboard güncelle - en yüksek benzerlik puan alır
        if (_p1 > _p2) s1++;
        else if (_p2 > _p1) s2++;
        Service.Get<IEventBus>().Publish(new UiEvents.UpdateScoreboard(s1, s2));
        
        UnityEngine.Debug.Log($"🏆 Round Results - P1: {_p1}%, P2: {_p2}% | Score: {s1}-{s2}");
    }

    public void Tick()
    {
        // kısa gecikme sonrası sonuç kontrolü
        // (UI animasyonu için TimerService de kullanılabilir)
        if (s1 >= Rules.targetScore || s2 >= Rules.targetScore) // Rules'u kullanıyoruz
            _f.Change<ResultState>();
        else
            _f.Change<PoseState>(); // yeni tura geç
    }

    public void Exit() { }
}