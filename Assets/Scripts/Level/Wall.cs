using Unity.Mathematics;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] float thickness = 0.2f;

    private BlockedCells blockedCells;

    public void SetWall(CellOrdinate cell_1, CellOrdinate cell_2)
    {
        if (!IsAdjacentCells(cell_1, cell_2))
        {
            Debug.LogError("Creating Wall with not Adjacent cells");
            return;
        }

        this.blockedCells = new BlockedCells(cell_1, cell_2);

        this.SetPosition();
        this.SetRotation();
        this.SetSize();
    }

    private void SetSize()
    {
        float DEFAULT_SIZE = UnityDefaultParameter.DEFAULT_CUBE_SIZE;
        Vector3 cellSize = CellTransformGetter.Instance.GetCellSize();
        float scaleX = this.gameObject.transform.forward.x > this.gameObject.transform.forward.z ?
                        cellSize.z :
                        cellSize.x;

        scaleX = scaleX / DEFAULT_SIZE;
        float scaleY = this.gameObject.transform.localScale.y / DEFAULT_SIZE;
        float scaleZ = this.thickness / DEFAULT_SIZE;

        Vector3 newScale = new Vector3(scaleX, scaleY, scaleZ);

        this.gameObject.transform.localScale = newScale;
    }

    private void SetRotation()
    {
        Vector3 cell1Position = CellTransformGetter.Instance.GetCellPosition(this.blockedCells.cell_1);
        Vector3 cell2Position = CellTransformGetter.Instance.GetCellPosition(this.blockedCells.cell_2);

        this.gameObject.transform.forward = (cell1Position - cell2Position).normalized;
    }

    private void SetPosition()
    {
        Vector3 cell1Position = CellTransformGetter.Instance.GetCellPosition(this.blockedCells.cell_1);
        Vector3 cell2Position = CellTransformGetter.Instance.GetCellPosition(this.blockedCells.cell_2);

        Vector3 centerPosition = (cell1Position + cell2Position) / 2;

        this.gameObject.transform.position = centerPosition;
    }

    private bool IsAdjacentCells(CellOrdinate cell_1, CellOrdinate cell_2)
    {
        return math.abs(cell_1.x - cell_2.x) + math.abs(cell_1.y - cell_2.y) == 1;
    }
}
