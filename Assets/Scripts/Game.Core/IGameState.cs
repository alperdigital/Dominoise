namespace Game.Core
{
    public interface IGameState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}