using System;

public class EnemyMoveGameState : GameState
{
    private bool isEnemyMoveComplete = false;
    public EnemyMoveGameState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        this.isEnemyMoveComplete = false;
        Action onEnemyMoveComplete = () => this.isEnemyMoveComplete = true;
        this.gameManager.MoveEnemy(onEnemyMoveComplete);
    }

    public override IGameState Update()
    {
        if (this.isEnemyMoveComplete)
        {
            return new IdleGameState(this.gameManager);
        }

        return this;
    }

    public override void Exit()
    {
    }
}
