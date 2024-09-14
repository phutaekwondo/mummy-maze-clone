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
        ExitDoorMouseEventPanel eventPanel = this.levelEditorExitDoor.GetComponent<ExitDoorMouseEventPanel>();
        eventPanel.onMouseDown += this.HandleExitDoorMouseDown;
        eventPanel.onMouseUp += this.HandleExitDoorMouseUp;
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
