using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class ExitDoorTargetsSpawner : MonoBehaviour
{
    [SerializeField] GameObject exitDoorTargetPrefab;
    [SerializeField] GameObject exitDoorTargetsParent;
    private ExitDoorTargetsPanel exitDoorTargetsEventListener;

    private void Awake()
    {
        this.exitDoorTargetsEventListener = this.GetComponent<ExitDoorTargetsPanel>();
    }

    private void Clear()
    {
        foreach (Transform child in this.exitDoorTargetsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Spawn(int groundSize)
    {
        this.Clear();
        this.exitDoorTargetsEventListener.ClearExitDoorTargets();
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
        ExitDoorTarget newTarget = Instantiate(this.exitDoorTargetPrefab, this.exitDoorTargetsParent.transform).GetComponent<ExitDoorTarget>();
        newTarget.SetWall(new BlockedCell(cell_1, cell_2));
        this.exitDoorTargetsEventListener.AddExitDoorTarget(newTarget);
    }
}

