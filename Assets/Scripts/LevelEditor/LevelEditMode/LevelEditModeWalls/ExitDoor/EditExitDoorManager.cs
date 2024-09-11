using LevelEditor;
using UnityEngine;

public class EditExitDoorManager : MonoBehaviour
{
    ExitDoorTargetsPanel exitDoorTargetsPanel;
    private ExitDoor levelEditorExitDoor;
    private void Awake()
    {
        this.exitDoorTargetsPanel = this.GetComponent<ExitDoorTargetsPanel>();
        this.exitDoorTargetsPanel.SetEnabled(false);
        this.exitDoorTargetsPanel.handleTargetMouseEnter = this.HandleTargetMouseEnter;
    }

    public void ReceiveSpawnedExitDoor(ExitDoor levelEditorExitDoor)
    {
        this.levelEditorExitDoor = levelEditorExitDoor;
        ExitDoorMouseHandler exitDoorMouseHandler = this.levelEditorExitDoor.GetComponent<ExitDoorMouseHandler>();
        exitDoorMouseHandler.onMouseDown = this.HandleExitDoorMouseDown;
        exitDoorMouseHandler.onMouseUp = this.HandleExitDoorMouseUp;
    }

    private void HandleExitDoorMouseDown()
    {
        this.exitDoorTargetsPanel.SetEnabled(true);
    }

    private void HandleExitDoorMouseUp()
    {
        this.exitDoorTargetsPanel.SetEnabled(false);
    }

    private void HandleTargetMouseEnter(BlockedCell blockedCell)
    {
        this.levelEditorExitDoor.SetWall(blockedCell);
    }
}
