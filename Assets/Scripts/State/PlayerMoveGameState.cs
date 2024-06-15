public class PlayerMoveState : CharacterMoveGameState
{
    private EnumMoveDirection playerMoveDirection;
    public PlayerMoveState(GameManager gameManager, EnumMoveDirection playerMoveDirection) : base(gameManager)
    {
        this.playerMoveDirection = playerMoveDirection;
        this.defaultNextState = new EnemyMoveGameState(gameManager);
    }

    public override void Enter()
    {
        base.Enter();
        this.gameManager.MovePlayer(this.playerMoveDirection, this.onMoveComplete);
    }

    public override void Exit()
    {
    }
}
