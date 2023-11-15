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
    }
}
