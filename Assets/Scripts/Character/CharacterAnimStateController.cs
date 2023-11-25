using UnityEngine;

[RequireComponent(typeof(Animator))]
abstract public class CharacterAnimStateController : MonoBehaviour
{
    protected Animator animator;

    virtual protected void Awake() 
    {
        this.animator = GetComponent<Animator>();
    }

    abstract public void StartMoveAnim();
    abstract public void StopMoveAnim();
    abstract public void StartTurnAnim(ETurnType turnType);
    abstract public void StopTurnAnim();
    abstract public void StartBlockedAnim();
}
