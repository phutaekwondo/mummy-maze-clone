using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class CharacterAnimController : MonoBehaviour
{
    private const int IDLE_ANIM_INDEX = 0;
    private const int MOVE_ANIM_INDEX = 1;
    private const int BLOCKED_ANIM_INDEX = 5;
    private Dictionary<ETurnType, int> TURN_ANIM_INDEX = new Dictionary<ETurnType, int>();
    private Animator animator;
    private int animIndexRef;
    private int playingAnimIndex = -1;

    private Dictionary<int, Action> onAnimEndDict = new Dictionary<int, Action>();

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
        this.PlayAnimByIndex(IDLE_ANIM_INDEX);
    }

    virtual public void PlayMove()
    {
        this.PlayAnimByIndex(MOVE_ANIM_INDEX);
    }

    virtual public void PlayTurn(ETurnType turnType, Action onTurnAnimComplete = null)
    {
        this.PlayAnimByIndex(this.TURN_ANIM_INDEX[turnType],onTurnAnimComplete);
    }

    virtual public void PlayBlocked(Action onComplete = null)
    {
        this.PlayAnimByIndex(BLOCKED_ANIM_INDEX,onComplete);
    }

    protected void PlayAnimByIndex(int animIndex, Action onComplete = null)
    {
        this.onAnimEndDict[animIndex] = onComplete;
        this.animator.SetInteger(this.animIndexRef, animIndex);
        this.playingAnimIndex = animIndex;
    }

    public void CallOnAnimEnd() 
    {
        if (this.onAnimEndDict.ContainsKey(this.playingAnimIndex))
        {
            int tempAnimIndex = this.playingAnimIndex;
            this.playingAnimIndex = -1;
            this.onAnimEndDict[tempAnimIndex]?.Invoke();
            this.onAnimEndDict[tempAnimIndex] = null;
        }
    }
}
