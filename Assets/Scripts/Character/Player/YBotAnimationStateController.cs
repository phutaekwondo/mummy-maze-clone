using UnityEngine;

[RequireComponent(typeof(Animator))]
public class YBotAnimationStateController : MonoBehaviour
{
    private Animator animator;

    private int isWalkingRef;
    private int isBlockedRef;

    private void Awake() 
    {
        this.animator = GetComponent<Animator>();
        this.isWalkingRef = Animator.StringToHash("isWalking");
        this.isBlockedRef = Animator.StringToHash("isBlocked");
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
