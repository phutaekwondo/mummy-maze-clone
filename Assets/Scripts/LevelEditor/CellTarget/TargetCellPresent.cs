using UnityEngine;

public class TargetCellPresent : MonoBehaviour
{
    public void SetSize(Ground ground)
    {
        float DEFAULT_SIZE = UnityDefaultParameter.DEFAULT_CUBE_SIZE;
        Vector3 cellSize = ground.GetCellSize();

        float scaleX = cellSize.x / DEFAULT_SIZE;
        float scalez = cellSize.z / DEFAULT_SIZE;

        Vector3 newScale = new Vector3(scaleX, this.gameObject.transform.localScale.y, scalez);

        this.gameObject.transform.localScale = newScale;
    }

    public void SetCell(CellOrdinate cellOrdinate, Ground ground)
    {
        if (cellOrdinate != null)
        {
            this.SetVisible(true);
            this.SetPosition(cellOrdinate, ground);
        }
        else
        {
            this.SetVisible(false);
        }
    }

    private void SetVisible(bool visible)
    {
        this.gameObject.SetActive(visible);
    }

    private void SetPosition(CellOrdinate cellOrdinate, Ground ground)
    {
        Vector3 cellPosition = ground.GetCellPosition(cellOrdinate);
        this.gameObject.transform.position = cellPosition;
    }
}
