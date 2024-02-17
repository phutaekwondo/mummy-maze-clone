using UnityEngine;

public class GroundMouseDetect : MonoBehaviour
{
    [SerializeField] private Ground ground;
    private CellOrdinateCalculator cellOrdinateCalculator;
    private CellOrdinate currentCellOrdinate;

    GroundMouseDetect()
    {
        this.cellOrdinateCalculator = new CellOrdinateCalculator();
    }

    private void Update() 
    {
        this.CheckMouseOnGround();
    }

    private void CheckMouseOnGround()
    {
        Vector3 mouseGroundPosition = this.GetMouseOnGround();
        CellOrdinate cellOrdinate = this.cellOrdinateCalculator.FromPosition(this.ground, mouseGroundPosition);

        if (cellOrdinate == null)
        {
            return;
        }

        if (this.currentCellOrdinate?.Equals(cellOrdinate) == true)
        {
            return;
        }

        this.OnCellOrdinateChanged(cellOrdinate);
    }

    private void OnCellOrdinateChanged(CellOrdinate cellOrdinate)
    {
        this.currentCellOrdinate = cellOrdinate;

        Debug.Log("Cell Ordinate: " + cellOrdinate.x + ", " + cellOrdinate.y);
    }

    private Vector3 GetMouseOnGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, this.ground.transform.position);

        plane.Raycast(ray, out float distance);

        return ray.GetPoint(distance);
    }
}
