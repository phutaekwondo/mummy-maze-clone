using UnityEngine;

public class CharacterAnimStateBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<CharacterAnimController>().CallOnAnimEnd();
    }
}
