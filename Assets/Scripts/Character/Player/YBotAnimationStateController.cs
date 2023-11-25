using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class YBotAnimationStateController : CharacterAnimStateController
{
    private int isWalkingRef;
    private int isBlockedRef;
    private Dictionary<ETurnType, int> dictTurnRef = new Dictionary<ETurnType, int>();
    private Action<ETurnType, float> onEnterTurn2MoveTransitionCallback;

    override protected void Awake()
    {
        base.Awake();

        YBotTurnAnimState.onEnterTransition += this.onTurnAnimEnterTransition;

        this.isWalkingRef = Animator.StringToHash("isWalking");
        this.isBlockedRef = Animator.StringToHash("isBlocked");

        this.dictTurnRef[ETurnType.Left] = Animator.StringToHash("isTurnLeft");
        this.dictTurnRef[ETurnType.Right] = Animator.StringToHash("isTurnRight");
        this.dictTurnRef[ETurnType.Back] = Animator.StringToHash("isTurnBack");
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
        this.animator.SetBool(this.dictTurnRef[turnType], true);
        this.animator.SetBool(this.isWalkingRef, true);
    }

    override public void StopTurnAnim()
    {
        List<int> refs = this.dictTurnRef.Values.ToList<int>();

        refs.ForEach(delegate(int parameterRef) 
        {
            this.animator.SetBool(parameterRef, false);
        });
    }

    override public void StopMoveAnim() 
    {
        this.animator.SetBool(this.isWalkingRef, false);
    }

    override public void StartMoveAnim() 
    {
        this.animator.SetBool(this.isWalkingRef, true);
    }

    override public void StartBlockedAnim()
    {
        this.animator.SetBool(this.isBlockedRef, true);
    }
}
