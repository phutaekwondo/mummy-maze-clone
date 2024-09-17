using LevelEditor;
using UnityEngine;

public class EditExitDoorManager : MonoBehaviour
{
    [SerializeField] ExitDoorPositionOnTargetSetter exitDoorPositionOnTargetSetter;
    private ExitDoorStateMachine exitDoorStateMachine;
    private void Awake()
    {
        this.exitDoorPositionOnTargetSetter.SetEnabled(false);
    }

    public void SetEnabled(bool enabled)
    {
        this.exitDoorStateMachine.SetIsActive(enabled);
    }

    public void ReceiveSpawnedExitDoor(ExitDoor levelEditorExitDoor)
    {
        this.exitDoorStateMachine = levelEditorExitDoor.GetComponent<ExitDoorStateMachine>();
        this.exitDoorStateMachine.onStateChange += this.HandleEditDoorState;
    }

    public void HandleEditDoorState(ExitDoorState state)
    {
        bool enableTarget = false;
        switch (state)
        {
            case ExitDoorState.Hold:
                enableTarget = true;
                break;
            case ExitDoorState.Default:
            case ExitDoorState.Hover:
                enableTarget = false;
                break;
        }

        this.exitDoorPositionOnTargetSetter.SetEnabled(enableTarget);
    }

}
