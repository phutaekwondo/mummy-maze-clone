using System;
using DigitalRuby.Tween;
using UnityEngine;

public class Player : Character
{
    private YBotAnimationStateController animStateController;

    private void Awake() 
    {
        this.animStateController = this.GetComponent<YBotAnimationStateController>();
    }

    override protected void PlayMovementAnimation()
    {
        this.animStateController.StartWalk();
    }

    override protected void StopMovementAnimation()
    {
        this.animStateController.StartIdle();
    }
}
