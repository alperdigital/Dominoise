using UnityEngine;
using Game.Core;

public sealed class LobbyState : IGameState
{
    readonly GameFlow _f;
    public LobbyState(GameFlow f){ _f = f; }
    public void Enter() { /* UI'de lobby görünümü kalabilir */ }
    public void Tick() { }
    public void Exit() { }
}
