using UnityEngine;

public delegate void OnEnterTransition(AnimatorTransitionInfo transitionInfo, ETurnType turnType);

public class YBotTurnAnimState : StateMachineBehaviour
{
    public static event OnEnterTransition onEnterTransition;
    [SerializeField] private ETurnType turnType;
    private bool isEnteredTransition = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        this.isEnteredTransition = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (animator.IsInTransition(layerIndex) && animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash && !this.isEnteredTransition) 
        {
            this.isEnteredTransition = true;
            YBotTurnAnimState.onEnterTransition.Invoke(animator.GetAnimatorTransitionInfo(layerIndex), this.turnType);
        }
    }
}
