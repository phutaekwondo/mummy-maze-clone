using System;
using UnityEngine;

public class PlayerMoveState : CharacterMoveGameState
{
    private EnumMoveDirection playerMoveDirection;
    private bool isPlayerMoveComplete = false;
    public PlayerMoveState(GameManager gameManager, EnumMoveDirection playerMoveDirection): base(gameManager)
    {
        this.playerMoveDirection = playerMoveDirection;
    }

    public override void Enter()
    {
        this.isPlayerMoveComplete = false;
        Action onPlayerMoveComplete = () => this.isPlayerMoveComplete = true;
        this.gameManager.MovePlayer(this.playerMoveDirection, onPlayerMoveComplete);
    }

    public override IGameState Update()
    {
        if (this.isPlayerMoveComplete)
        {
            return new EnemyMoveGameState(this.gameManager);
        }

        return this;
    }

    public override void Exit()
    {
    }
}
