using LevelEditor;
using UnityEngine;

public class EditExitDoorManager : MonoBehaviour
{
    [SerializeField] ExitDoorTargetsPanel exitDoorTargetsPanel;
    private ExitDoor levelEditorExitDoor;
    private void Awake()
    {
        this.exitDoorTargetsPanel.SetEnabled(false);
        this.exitDoorTargetsPanel.handleTargetMouseEnter = this.HandleTargetMouseEnter;
    }

    public void ReceiveSpawnedExitDoor(ExitDoor levelEditorExitDoor)
    {
        this.levelEditorExitDoor = levelEditorExitDoor;
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

        this.exitDoorTargetsPanel.SetEnabled(enableTarget);
    }

    private void HandleTargetMouseEnter(BlockedCell blockedCell)
    {
        this.levelEditorExitDoor.SetWall(blockedCell);
    }
}
