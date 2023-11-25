using DigitalRuby.Tween;
using UnityEngine;

public class Player : Character
{
    override protected void Awake() 
    {
        base.Awake();
        YBotTurnAnimState.onEnterTransition += this.onTurnAnimEnterTransition;
    }

    private void onTurnAnimEnterTransition(AnimatorTransitionInfo transitionInfo, ETurnType turnType)
    {
    }
}
