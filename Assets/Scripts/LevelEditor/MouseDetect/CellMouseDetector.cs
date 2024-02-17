using UnityEngine;

public class CellMouseDetector : MonoBehaviour
{
    [SerializeField] private CellOrdinate cellOrdinate;

    public void SetCell(CellOrdinate cellOrdinate)
    {
        this.cellOrdinate = cellOrdinate;
        this.SetPosition(cellOrdinate);
        this.SetSize();
    }

    private void SetSize()
    {
        Vector3 cellSize = CellTransformGetter.Instance.GetCellSize();
        float PLANE_SIZE = UnityDefaultParameter.DEFAULT_PLANE_SIZE;

        this.transform.localScale = new Vector3(
            cellSize.x / PLANE_SIZE,
            this.transform.localScale.y,
            cellSize.z / PLANE_SIZE
        );
    }

    private void SetPosition(CellOrdinate ordinate)
    {
        Vector3 cellPosition = CellTransformGetter.Instance.GetCellPosition(ordinate);
        this.transform.position = cellPosition;
    }
}
