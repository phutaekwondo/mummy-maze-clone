using UnityEngine;

namespace LevelEditor
{
    public class ExitDoorPositionOnTargetSetter : MonoBehaviour
    {
        [SerializeField] ExitDoorTargetsPanel exitDoorTargetsPanel;
        [SerializeField] AroundWallsManager aroundWallsManager;

        private void Awake()
        {
            this.exitDoorTargetsPanel.SetEnabled(false);
            this.exitDoorTargetsPanel.handleTargetMouseEnter = this.HandleTargetMouseEnter;
        }

        public void SetEnabled(bool enabled)
        {
            this.exitDoorTargetsPanel.SetEnabled(enabled);
        }

        private void HandleTargetMouseEnter(BlockedCell blockedCell)
        {
            this.aroundWallsManager.SetExitDoor(blockedCell);
        }
    }
}
