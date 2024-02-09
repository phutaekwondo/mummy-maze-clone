
using UnityEngine;

public class WinGameState : GameState
{
    public WinGameState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        Debug.Log("WinGameState");
    }

    public override IGameState Update()
    {
        return this;
    }

    public override void Exit()
    {
    }
}