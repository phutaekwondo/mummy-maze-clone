public class GameStateMachine
{
    private GameManager gameManager;
    private IGameState currentState;

    public GameStateMachine(GameManager gameManager)
    {
        this.gameManager = gameManager;
        this.currentState = new IdleGameState(gameManager);
    }

    public void TransitionTo(IGameState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        IGameState nextState = currentState.Update();

        if (nextState != currentState)
        {
            TransitionTo(nextState);
        }
    }
}
