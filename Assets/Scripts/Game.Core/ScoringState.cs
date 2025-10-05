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
        // MVP: rastgele skor, sonra PoseScorer ile değişecek
        _p1 = Random.Range(60, 97);
        _p2 = Random.Range(60, 97);
        Service.Get<IEventBus>().Publish(new UiEvents.ShowPercents(_p1, _p2));

        // skorboard güncelle
        if (_p1 > _p2) s1++;
        else if (_p2 > _p1) s2++;
        Service.Get<IEventBus>().Publish(new UiEvents.UpdateScoreboard(s1, s2));
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