using UnityEngine;

public class LoseGameState : GameState
{
    public LoseGameState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        Debug.Log("LoseGameState");
    }

    public override IGameState Update()
    {
        return this;
    }

    public override void Exit()
    {
    }
}