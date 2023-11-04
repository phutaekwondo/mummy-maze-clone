public class Enemy : Character
{
    DoctorAnimStateController animController;

    protected override void PlayMovementAnimation()
    {
        this.animController.StartRun();
    }

    protected override void StopMovementAnimation()
    {
        this.animController.StartIdle();
    }
}
