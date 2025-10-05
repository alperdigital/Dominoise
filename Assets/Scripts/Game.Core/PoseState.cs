using UnityEngine;
using Game.Core;
using Game.Services;
using Game.SharedSo;

public sealed class PoseState : IGameState
{
    readonly GameFlow _f;
    float _t; int _last;

    // GameRulesSo erişimini ServiceLocator üzerinden yapıyoruz.
    private GameRulesSo Rules => Service.Get<GameRulesSo>();

    public PoseState(GameFlow f){ _f=f; }

    public void Enter()
    {
        _t = Rules.poseSeconds; // Rules'u doğrudan kullanıyoruz
        _last = Mathf.CeilToInt(_t);
        Service.Get<IEventBus>().Publish(new UiEvents.CountdownShow(_last));
        // burada ikon seçimi yapılacak (ileride)
        Service.Get<IEventBus>().Publish(new UiEvents.SetCenterIcon());
    }

    public void Tick()
    {
        _t -= Time.deltaTime;
        int cur = Mathf.CeilToInt(_t);
        if (cur != _last && cur >= 0)
        {
            _last = cur;
            Service.Get<IEventBus>().Publish(new UiEvents.CountdownTick(cur));
        }
        if (_t <= 0f) _f.Change<ScoringState>();
    }

    public void Exit()
    {
        Service.Get<IEventBus>().Publish(new UiEvents.CountdownHide());
    }
}