public interface IGameState
{
    void Enter();
    IGameState Update();
    void Exit();
}