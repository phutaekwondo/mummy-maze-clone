using System;
using Unity.VisualScripting;
using UnityEngine;

public class ExitDoorTargetsSpawner : MonoBehaviour
{
    [SerializeField] GameObject exitDoorTargetPrefab;
    [SerializeField] GameObject targetsParent;

    private ExitDoorTargetsPanel exitDoorTargetsPanel;

    private void Awake()
    {
        this.exitDoorTargetsPanel = this.GetComponent<ExitDoorTargetsPanel>();
    }

    private void Clear()
    {
        foreach (Transform child in this.targetsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Spawn(int groundSize)
    {
        this.Clear();
        this.exitDoorTargetsPanel.ClearExitDoorTargets();
        for (int i = 0; i < groundSize; i++)
        {
            //top
            CellOrdinate cell_in = new CellOrdinate(i, 0);
            CellOrdinate cell_out = new CellOrdinate(i, -1);

            this.SpawnATarget(cell_in, cell_out);

            //left
            cell_in = new CellOrdinate(0, i);
            cell_out = new CellOrdinate(-1, i);

            this.SpawnATarget(cell_in, cell_out);

            //right
            cell_in = new CellOrdinate(groundSize - 1, i);
            cell_out = new CellOrdinate(groundSize, i);

            this.SpawnATarget(cell_in, cell_out);

            //bot
            cell_in = new CellOrdinate(i, groundSize - 1);
            cell_out = new CellOrdinate(i, groundSize);

            this.SpawnATarget(cell_in, cell_out);
        }
    }

    private void SpawnATarget(CellOrdinate cell_1, CellOrdinate cell_2)
    {
        ExitDoorTarget newTarget = Instantiate(this.exitDoorTargetPrefab, this.targetsParent.transform).GetComponent<ExitDoorTarget>();
        newTarget.SetWall(new BlockedCell(cell_1, cell_2));
        this.exitDoorTargetsPanel.AddExitDoorTarget(newTarget);
    }
}

