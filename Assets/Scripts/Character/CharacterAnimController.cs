using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class CharacterAnimController : MonoBehaviour
{
    private const int IDLE_ANIM_INDEX = 0;
    private const int MOVE_ANIM_INDEX = 1;
    private Dictionary<ETurnType, int> TURN_ANIM_INDEX = new Dictionary<ETurnType, int>();
    private Animator animator;
    private int animIndexRef;

    Action onAnimEnd = null;

    private void Awake() 
    {
        this.animator = this.GetComponent<Animator>();
        this.animIndexRef = Animator.StringToHash("animIndex");
        this.TURN_ANIM_INDEX[ETurnType.Left] = 2;
        this.TURN_ANIM_INDEX[ETurnType.Right] = 3;
        this.TURN_ANIM_INDEX[ETurnType.Back] = 4;
    }

    virtual public void PlayIdle()
    {
        this.animator.SetInteger(this.animIndexRef, IDLE_ANIM_INDEX);
    }

    virtual public void PlayMove()
    {
        this.animator.SetInteger(this.animIndexRef, MOVE_ANIM_INDEX);
    }

    virtual public void PlayTurn(ETurnType turnType, Action onTurnAnimComplete = null)
    {
        this.animator.SetInteger(this.animIndexRef, TURN_ANIM_INDEX[turnType]);
        this.onAnimEnd = onTurnAnimComplete;
    }

    public void CallOnAnimEnd() 
    {
        if (this.onAnimEnd != null)
        {
            this.onAnimEnd();
            this.onAnimEnd = null;
        }
        else 
        {
            this.PlayIdle();
        }
    }
}
