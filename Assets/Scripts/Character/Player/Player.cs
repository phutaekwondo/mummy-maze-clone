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
