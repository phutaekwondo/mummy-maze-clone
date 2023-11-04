using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoctorAnimStateController : MonoBehaviour
{
    private Animator animator;

    private int isRunningRef;

    private void Awake() 
    {
        this.animator = GetComponent<Animator>();
        this.isRunningRef = Animator.StringToHash("isRunning");
    }

    public void StartIdle() 
    {
        this.animator.SetBool(this.isRunningRef, false);
    }

    public void StartRun() 
    {
        this.animator.SetBool(this.isRunningRef, true);
    }
}
