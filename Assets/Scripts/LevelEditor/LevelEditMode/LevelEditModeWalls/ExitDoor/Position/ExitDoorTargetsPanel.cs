using System;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorTargetsPanel : MonoBehaviour
{
    [SerializeField] GameObject targetsParent;
    private List<ExitDoorTarget> exitDoorTargets = new List<ExitDoorTarget>();
    public Action<BlockedCell> handleTargetMouseEnter;

    public void SetEnabled(bool enabled)
    {
        this.targetsParent.SetActive(enabled);
    }

    public void AddExitDoorTarget(ExitDoorTarget exitDoorTarget)
    {
        this.exitDoorTargets.Add(exitDoorTarget);
        exitDoorTarget.mouseEnterHandler = this.handleTargetMouseEnter;
    }
    public void ClearExitDoorTargets()
    {
        this.exitDoorTargets.Clear();
    }
}
