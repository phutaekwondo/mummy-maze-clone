using LevelEditor;
using UnityEngine;

public class EditExitDoorManager : MonoBehaviour
{
    [SerializeField] ExitDoorPositionOnTargetSetter exitDoorPositionOnTargetSetter;
    private void Awake()
    {
        this.exitDoorPositionOnTargetSetter.SetEnabled(false);
    }

    public void ReceiveSpawnedExitDoor(ExitDoor levelEditorExitDoor)
    {
        ExitDoorStateMachine stateMachine = levelEditorExitDoor.GetComponent<ExitDoorStateMachine>();
        stateMachine.onStateChange += this.HandleEditDoorState;
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
