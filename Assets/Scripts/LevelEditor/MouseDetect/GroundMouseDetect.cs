using UnityEngine;

public class GroundMouseDetect : MonoBehaviour
{
    [SerializeField] private Ground ground;
    [SerializeField] private GameObject cellMouseDetectorsParent;
    [SerializeField] private GameObject cellMouseDetectorPrefab;

    private void Start() 
    {
        this.SpawnCellMouseDetectors();
    }

    private void SpawnCellMouseDetectors()
    {
        int widthSize = (int)this.ground.GetWidthSize();
        int heightSize = (int)this.ground.GetHeightSize();

        for (int x = 0; x < widthSize; x++)
        {
            for (int y = 0; y < heightSize; y++)
            {
                GameObject cellMouseDetector = Instantiate(this.cellMouseDetectorPrefab, this.cellMouseDetectorsParent.transform);
                CellMouseDetector cellMouseDetectorComponent = cellMouseDetector.GetComponent<CellMouseDetector>();
                cellMouseDetectorComponent.SetCell(new CellOrdinate(x, y));
            }
        }
    }
}
