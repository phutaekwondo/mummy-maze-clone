using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAnimController : MonoBehaviour
{
    private const int IDLE_ANIM_INDEX = 0;
    private const int MOVE_ANIM_INDEX = 1;
    private Animator animator;
    private int animIndexRef;

    private void Awake() 
    {
        this.animator = this.GetComponent<Animator>();
        this.animIndexRef = Animator.StringToHash("animIndex");
    }

    virtual public void PlayIdle()
    {
        this.animator.SetInteger(this.animIndexRef, IDLE_ANIM_INDEX);
    }
}
