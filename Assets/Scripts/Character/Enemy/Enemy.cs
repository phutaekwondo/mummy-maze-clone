public class Enemy : Character
{
    DoctorAnimStateController animStateController;

    private void Awake() 
    {
        this.animStateController = this.GetComponent<DoctorAnimStateController>();
    }

    protected override void PlayMovementAnimation()
    {
        this.animStateController.StartRun();
    }

    protected override void StopMovementAnimation()
    {
        this.animStateController.StartIdle();
    }
}
