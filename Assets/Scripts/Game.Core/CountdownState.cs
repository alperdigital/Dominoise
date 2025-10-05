using UnityEngine;
using Game.Core;
using Game.Services;
using Game.SharedSo;

public sealed class CountdownState : IGameState
{
    readonly GameFlow _f;
    float _t; int _last;
    
    private GameRulesSo Rules => Service.Get<GameRulesSo>(); 
    
    public CountdownState(GameFlow f){ _f=f; }

    public void Enter()
    {
        _t = Rules.prepSeconds; // Rules.prepSeconds yerine Rules.prepSeconds kullandık
        _last = Mathf.CeilToInt(_t);
        Service.Get<IEventBus>().Publish(new UiEvents.CountdownShow(_last));
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
        if (_t <= 0f) _f.Change<PoseState>();
    }

    public void Exit()
    {
        Service.Get<IEventBus>().Publish(new UiEvents.CountdownHide());
    }
}