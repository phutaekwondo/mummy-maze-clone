using UnityEngine;

[RequireComponent(typeof(Animator))]
public class YBotAnimationStateController : MonoBehaviour
{
    private Animator animator;
    private int isWalkingParameterRef;

    private void Awake() 
    {
        this.animator = GetComponent<Animator>();
        this.isWalkingParameterRef = Animator.StringToHash("isWalking");
    }

    private void Update() 
    {
        bool isForwardPressed = Input.GetKey("w");
        bool isWakingAlready = this.animator.GetBool(this.isWalkingParameterRef);

        if (!isWakingAlready && isForwardPressed) 
        {
            this.animator.SetBool(this.isWalkingParameterRef, true);
        }
        else if (isWakingAlready && !isForwardPressed) 
        {
            this.animator.SetBool(this.isWalkingParameterRef, false);
        }
    }
}
