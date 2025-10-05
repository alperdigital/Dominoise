using UnityEngine;
using Game.Core;
using Game.Services;

public sealed class ResultState : IGameState
{
    readonly GameFlow _f;
    public ResultState(GameFlow f){ _f=f; }
    public void Enter()
    {
        // UI'ye "Kazanan" gösterebilirsin, sonra lobby
        Service.Get<IEventBus>().Publish(new UiEvents.CountdownHide());
    }
    public void Tick()
    {
        // basit: kısa bekleme sonrası lobby
        _f.Change<LobbyState>();
    }
    public void Exit(){}
}