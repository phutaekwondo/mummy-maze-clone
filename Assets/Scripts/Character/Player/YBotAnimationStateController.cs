using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        this.dictTurnRef[ETurnType.Left] = Animator.StringToHash("isTurnLeft");
        this.dictTurnRef[ETurnType.Right] = Animator.StringToHash("isTurnRight");
        this.dictTurnRef[ETurnType.Back] = Animator.StringToHash("isTurnBack");
    }

    public void StartTurnAndWalk(ETurnType turnType)
    {
        this.animator.SetBool(this.dictTurnRef[turnType], true);
        this.animator.SetBool(this.isWalkingRef, true);
    }

    public void StopTurn()
    {
        List<int> refs = this.dictTurnRef.Values.ToList<int>();

        refs.ForEach(delegate(int parameterRef) 
        {
            this.animator.SetBool(parameterRef, false);
        });
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
