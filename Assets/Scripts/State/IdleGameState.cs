public class IdleGameState : GameState
{
    private PlayerInputGetter playerInputGetter;
    public IdleGameState(GameManager gameManager) : base(gameManager)
    {
        this.playerInputGetter = new PlayerInputGetter();
    }

    public override void Enter()
    {
    }

    public override IGameState Update()
    {
        EnumPlayerInput playerInput = playerInputGetter.GetPlayerInput();
        if (playerInput != EnumPlayerInput.None)
        {
            EnumMoveDirection playMoveDirection = this.GetMoveDirection(playerInput);
            IGameState playerMoveState = new PlayerMoveState(this.gameManager, playMoveDirection);
            return playerMoveState;
        }

        return this;
    }

    public override void Exit()
    {
    }

    private EnumMoveDirection GetMoveDirection(EnumPlayerInput playerInput)
    {
        if (playerInput == EnumPlayerInput.None)
        {
            throw new System.Exception("Can't get move direction from none input");
        }

        EnumMoveDirection moveDirection = EnumMoveDirection.None;

        switch (playerInput)
        {
            case EnumPlayerInput.MoveUp:
                moveDirection = EnumMoveDirection.Up;
                break;
            case EnumPlayerInput.MoveLeft:
                moveDirection = EnumMoveDirection.Left;
                break;
            case EnumPlayerInput.MoveRight:
                moveDirection = EnumMoveDirection.Right;
                break;
            case EnumPlayerInput.MoveDown:
                moveDirection = EnumMoveDirection.Down;
                break;
            default:
                break;
        }

        return moveDirection;
    }
}