using System;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorTargetsPanel : MonoBehaviour
{
    GameObject exitDoorTargetsParent;

    private void Awake()
    {
        this.exitDoorTargetsParent = this.gameObject;
    }

    public void SetEnabled(bool enabled)
    {
        this.exitDoorTargetsParent.SetActive(enabled);
    }

    private List<ExitDoorTarget> exitDoorTargets = new List<ExitDoorTarget>();
    public Action<BlockedCell> handleTargetMouseEnter;
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
