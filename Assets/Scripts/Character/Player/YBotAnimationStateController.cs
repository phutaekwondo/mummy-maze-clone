using UnityEngine;

[RequireComponent(typeof(Animator))]
public class YBotAnimationStateController : MonoBehaviour
{
    private Animator animator;

    private int isWalkingRef;

    private void Awake() 
    {
        this.animator = GetComponent<Animator>();
        this.isWalkingRef = Animator.StringToHash("isWalking");
    }

    public void StartIdle() 
    {
        this.animator.SetBool(this.isWalkingRef, false);
    }

    public void StartWalk() 
    {
        this.animator.SetBool(this.isWalkingRef, true);
    }
}
