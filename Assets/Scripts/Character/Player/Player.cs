using DigitalRuby.Tween;
using UnityEngine;

public class Player : Character
{
    private YBotAnimationStateController animStateController;

    private void Awake() 
    {
        this.animStateController = this.GetComponent<YBotAnimationStateController>();
        YBotTurnAnimState.onEnterTransition += this.onTurnAnimEnterTransition;
    }

    private void onTurnAnimEnterTransition(AnimatorTransitionInfo transitionInfo, ETurnType turnType)
    {
        this.TweenTurn(turnType, transitionInfo.duration);
        this.MoveTowardOneCell();
        this.animStateController.StopTurn();

        TweenFactory.print(this.gameObject);

        int i = 1;
    }

    protected override void TurnAndMoveOneCell(EnumMoveDirection direction)
    {
        ETurnType turnType = EnumMoveDirectionHelper.GetTurnType(this.lookDirection, direction);
        this.lookDirection = direction;

        Debug.Log("=============");
        Debug.Log("this.look " + this.lookDirection);
        Debug.Log("direction " + direction);
        Debug.Log("turnType " + turnType);
        Debug.Log("=============");
        this.animStateController.StartTurnAndWalk(turnType);
    }

    override protected void PlayMovementAnimation()
    {
        this.animStateController.StartWalk();
    }

    override protected void StopMovementAnimation()
    {
        this.animStateController.StartIdle();
    }

    override public void ActBlocked(EnumMoveDirection direction)
    {
        base.ActBlocked(direction);
        this.animStateController.StartBlocked();
    }
}
