using System;
using UnityEngine;

public class GroundMouseDetect : MonoBehaviour
{
    [SerializeField] private Ground ground;
    private CellOrdinateCalculator cellOrdinateCalculator;
    private CellOrdinate currentCellOrdinate;
    public bool enable { private get; set;} = false;
    Action<CellOrdinate> onCellOrdinateChanged;

    GroundMouseDetect()
    {
        this.cellOrdinateCalculator = new CellOrdinateCalculator();
    }

    public void SetOnCellOrdinateChanged(Action<CellOrdinate> onCellOrdinateChanged)
    {
        this.onCellOrdinateChanged = onCellOrdinateChanged;
    }

    private void Update() 
    {
        if (this.enable)
        {
            this.CheckMouseOnGround();
        }
    }

    private void CheckMouseOnGround()
    {
        Vector3 mouseGroundPosition = this.GetMouseOnGround();
        CellOrdinate cellOrdinate = this.cellOrdinateCalculator.FromPosition(this.ground, mouseGroundPosition);

        if (cellOrdinate == null && this.currentCellOrdinate == null)
        {
            return;
        }

        if (cellOrdinate != null && this.currentCellOrdinate?.Equals(cellOrdinate) == true)
        {
            return;
        }

        this.OnCellOrdinateChanged(cellOrdinate);
    }

    private void OnCellOrdinateChanged(CellOrdinate cellOrdinate)
    {
        this.currentCellOrdinate = cellOrdinate;
        this.onCellOrdinateChanged?.Invoke(cellOrdinate);
    }

    private Vector3 GetMouseOnGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, this.ground.transform.position);

        plane.Raycast(ray, out float distance);

        return ray.GetPoint(distance);
    }
}
