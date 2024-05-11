using UnityEngine;

public class TargetCellPresent : MonoBehaviour
{
    public void SetSize(Vector3 cellSize)
    {
        float DEFAULT_SIZE = UnityDefaultParameter.DEFAULT_CUBE_SIZE;

        float scaleX = cellSize.x / DEFAULT_SIZE;
        float scalez = cellSize.z / DEFAULT_SIZE;

        Vector3 newScale = new Vector3(scaleX, this.gameObject.transform.localScale.y, scalez);

        this.gameObject.transform.localScale = newScale;
    }

    public void SetCell(CellOrdinate cellOrdinate, Ground ground)
    {
        if (cellOrdinate == null)
        {
            return;
        }
        this.SetPosition(cellOrdinate, ground);
    }

    public void SetVisible(bool visible)
    {
        this.gameObject.SetActive(visible);
    }

    private void SetPosition(CellOrdinate cellOrdinate, Ground ground)
    {
        Vector3 cellPosition = ground.GetCellPosition(cellOrdinate);
        this.gameObject.transform.position = cellPosition;
    }
}
