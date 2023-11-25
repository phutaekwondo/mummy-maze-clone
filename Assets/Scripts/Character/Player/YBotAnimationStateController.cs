using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class YBotAnimationStateController : CharacterAnimStateController
{
    private int isWalkingRef;
    private int isBlockedRef;
    private Dictionary<ETurnType, int> dictTurnRef = new Dictionary<ETurnType, int>();

    override protected void Awake()
    {
        base.Awake();

        this.isWalkingRef = Animator.StringToHash("isWalking");
        this.isBlockedRef = Animator.StringToHash("isBlocked");

        this.dictTurnRef[ETurnType.Left] = Animator.StringToHash("isTurnLeft");
        this.dictTurnRef[ETurnType.Right] = Animator.StringToHash("isTurnRight");
        this.dictTurnRef[ETurnType.Back] = Animator.StringToHash("isTurnBack");
    }

    override public void StartTurnAnim(ETurnType turnType)
    {
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
