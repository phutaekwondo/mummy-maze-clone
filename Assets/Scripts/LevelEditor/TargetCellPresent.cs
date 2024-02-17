using UnityEngine;

public class TargetCellPresent : MonoBehaviour
{
    [SerializeField] private Ground ground;

    public void SetSize()
    {
        float DEFAULT_SIZE = UnityDefaultParameter.DEFAULT_CUBE_SIZE;
        Vector3 cellSize = this.ground.GetCellSize();

        float scaleX = cellSize.x / DEFAULT_SIZE;
        float scalez = cellSize.z / DEFAULT_SIZE;

        Vector3 newScale = new Vector3(scaleX, this.gameObject.transform.localScale.y, scalez);

        this.gameObject.transform.localScale = newScale;
    }

    public void SetCell(CellOrdinate cellOrdinate)
    {
    }
}
