using UnityEngine;


public class Ground : MonoBehaviour
{
    const float PLANE_SIZE = 10;
    [SerializeField] private Material groundMaterial;
    private int widthSize = 5;
    private int heightSize = 5;

    public Vector3 GetCellPosition(CellOrdinate cellOrdinate) 
    {
        return GetCellPosition(cellOrdinate.x, cellOrdinate.y);
    }
    
    public Vector3 GetCellPosition(int x, int y)
    {
        float scaledPlaneSizeX = PLANE_SIZE * this.gameObject.transform.localScale.x;
        float scaledPlaneSizeZ = PLANE_SIZE * this.gameObject.transform.localScale.z;

        float horizontalStepSize = scaledPlaneSizeX / this.widthSize;
        float verticalStepSize   = scaledPlaneSizeZ / this.heightSize;

        float leftX = this.gameObject.transform.position.x - (scaledPlaneSizeX / 2) + (horizontalStepSize / 2);
        float topZ = this.gameObject.transform.position.z + (scaledPlaneSizeZ / 2) - (verticalStepSize / 2);

        return new Vector3(
            leftX + horizontalStepSize * x,
            this.gameObject.transform.position.y,
            topZ - y * verticalStepSize
        );
    }
    
    public void SetSize(int width, int height)
    {
        this.widthSize = width;
        this.heightSize = height;
        this.SetMatrialStepsSize();
    }

    private void SetMatrialStepsSize()
    {
        this.groundMaterial.SetFloat("_HorizontalSteps", this.widthSize);
        this.groundMaterial.SetFloat("_VerticalSteps", this.heightSize);
    }
}
