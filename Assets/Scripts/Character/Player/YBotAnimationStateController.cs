using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class YBotAnimationStateController : CharacterAnimStateController
{
    private const string MOVE_STATE_NAME = "Walk"; 
    private const string IDLE_STATE_NAME = "Idle"; 
    private const string TURN_LEFT_STATE_NAME = "TurnLeft"; 
    private const string TURN_RIGHT_STATE_NAME = "TurnRight"; 
    private const string TURN_BACK_STATE_NAME = "TurnBack"; 
    private const string BLOCKED_STATE_NAME = "Annoyed"; 
    private int isWalkingRef;
    private Dictionary<ETurnType, string> dictTurnRef = new Dictionary<ETurnType, string>();
    private Action<ETurnType, float> onEnterTurn2MoveTransitionCallback;

    override protected void Awake()
    {
        base.Awake();

        YBotTurnAnimState.onEnterTransition += this.onTurnAnimEnterTransition;

        this.isWalkingRef = Animator.StringToHash("isWalking");
        this.dictTurnRef[ETurnType.Left] = TURN_LEFT_STATE_NAME;
        this.dictTurnRef[ETurnType.Right] = TURN_RIGHT_STATE_NAME;
        this.dictTurnRef[ETurnType.Back] = TURN_BACK_STATE_NAME;
    }

    private void onTurnAnimEnterTransition(AnimatorTransitionInfo transitionInfo, ETurnType turnType)
    {
        if (this.onEnterTurn2MoveTransitionCallback != null)
        {
            this.onEnterTurn2MoveTransitionCallback(turnType, transitionInfo.duration);
        }
    }

    override public void StartTurnThenMoveAnim(ETurnType turnType, Action<ETurnType, float> onEnterTurn2MoveTransition)
    {
        this.onEnterTurn2MoveTransitionCallback = onEnterTurn2MoveTransition;
        this.animator.SetBool(this.isWalkingRef, true);
        this.animator.Play(this.dictTurnRef[turnType]);
    }

    override public void StopTurnAnim()
    {
    }

    override public void StopMoveAnim() 
    {
        this.animator.SetBool(this.isWalkingRef, false);
    }

    override public void StartMoveAnim() 
    {
        this.animator.Play(MOVE_STATE_NAME);
        this.animator.SetBool(this.isWalkingRef, true);
    }

    override public void StartBlockedAnim()
    {
        this.animator.Play(BLOCKED_STATE_NAME);
    }
}
