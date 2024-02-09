using System;

public class EnemyMoveGameState : CharacterMoveGameState
{
    public EnemyMoveGameState(GameManager gameManager) : base(gameManager)
    {
        this.defaultNextState = new IdleGameState(gameManager);
    }

    public override void Enter()
    {
        base.Enter();
        this.gameManager.MoveEnemy(this.onMoveComplete);
    }

    public override void Exit()
    {
    }
}
