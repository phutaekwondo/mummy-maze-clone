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
        Debug.Log("Exit door mouse down");
        this.exitDoorTargetsPanel.SetEnabled(true);
    }

    private void HandleExitDoorMouseUp()
    {
        Debug.Log("Exit door mouse up");
        this.exitDoorTargetsPanel.SetEnabled(false);
    }

    private void HandleTargetMouseEnter(BlockedCell blockedCell)
    {
        this.levelEditorExitDoor.SetWall(blockedCell);
    }
}
