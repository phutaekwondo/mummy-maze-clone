using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class YBotAnimationStateController : MonoBehaviour
{
    private Animator animator;
    private int isForwardPressRef;
    private int isShiftPressRef;

    private void Awake() 
    {
        this.animator = GetComponent<Animator>();
        this.isForwardPressRef = Animator.StringToHash("isForwardPress");
        this.isShiftPressRef = Animator.StringToHash("isShiftPress");
    }

    private void Update() 
    {
        bool isForward = Input.GetKey(KeyCode.W);
        bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        this.animator.SetBool(isForwardPressRef,isForward);
        this.animator.SetBool(isShiftPressRef,isShift);
    }
}
