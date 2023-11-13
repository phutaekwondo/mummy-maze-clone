using System.Collections.Generic;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class YBotAnimationStateController : MonoBehaviour
{
    private Animator animator;

    private int isWalkingRef;
    private int isBlockedRef;
    private Dictionary<ETurnType, int> dictTurnRef = new Dictionary<ETurnType, int>();

    private void Awake() 
    {
        this.animator = GetComponent<Animator>();
        this.isWalkingRef = Animator.StringToHash("isWalking");
        this.isBlockedRef = Animator.StringToHash("isBlocked");

        dictTurnRef[ETurnType.Left] = Animator.StringToHash("isTurnLeft");
        dictTurnRef[ETurnType.Right] = Animator.StringToHash("isTurnRight");
        dictTurnRef[ETurnType.Back] = Animator.StringToHash("isTurnBack");
    }

    public void StartTurnAndWalk(ETurnType turnType)
    {
        this.animator.SetBool(this.dictTurnRef[turnType], true);
        this.animator.SetBool(this.isWalkingRef, true);
    }

    public void StopTurn(ETurnType turnType)
    {
        this.animator.SetBool(this.dictTurnRef[turnType], false);
    }

    public void StartIdle() 
    {
        this.animator.SetBool(this.isWalkingRef, false);
    }

    public void StartWalk() 
    {
        this.animator.SetBool(this.isWalkingRef, true);
    }

    public void StartBlocked()
    {
        this.animator.SetBool(this.isBlockedRef, true);
    }
}
