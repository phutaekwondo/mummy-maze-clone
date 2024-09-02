using LevelEditor;
using UnityEngine;

public class EditExitDoorManager : MonoBehaviour
{
    ExitDoorTargetsPanel exitDoorTargetsPanel;
    private ExitDoor levelEditorExitDoor;
    private void Awake()
    {
        this.exitDoorTargetsPanel = this.GetComponent<ExitDoorTargetsPanel>();
        this.exitDoorTargetsPanel.SetEnabled(true);
        this.exitDoorTargetsPanel.handleTargetMouseEnter = this.HandleTargetMouseEnter;
    }

    public void ReceiveSpawnedExitDoor(ExitDoor levelEditorExitDoor)
    {
        this.levelEditorExitDoor = levelEditorExitDoor;
    }

    public void HandleTargetMouseEnter(BlockedCell blockedCell)
    {
        this.levelEditorExitDoor.SetWall(blockedCell);
    }
}
