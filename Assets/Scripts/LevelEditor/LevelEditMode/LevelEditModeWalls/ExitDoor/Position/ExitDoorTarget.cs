
using System;
using UnityEngine;

public class ExitDoorTarget : Wall
{
    public Action<BlockedCell> mouseEnterHandler;

    private void OnMouseEnter()
    {
        if (this.mouseEnterHandler != null)
        {
            this.mouseEnterHandler(this.blockedCell);
        }
    }
}
