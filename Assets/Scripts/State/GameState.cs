public abstract class GameState : IGameState
{
    protected GameManager gameManager;
    public GameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public abstract void Enter();
    public abstract IGameState Update();
    public abstract void Exit();
}