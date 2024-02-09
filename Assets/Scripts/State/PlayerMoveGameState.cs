using UnityEngine;

public class PlayerMoveState : CharacterMoveGameState
{
    private EnumMoveDirection playerMoveDirection;
    public PlayerMoveState(EnumMoveDirection playerMoveDirection)
    {
        this.playerMoveDirection = playerMoveDirection;
    }

    public override void Enter()
    {
        Debug.Log("PlayerMoveState Enter with " + playerMoveDirection.ToString() + " direction");
    }

    public override IGameState Update()
    {
        return this;
    }

    public override void Exit()
    {
    }
}
