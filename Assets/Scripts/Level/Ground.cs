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
        float horizontalStepSize = PLANE_SIZE / this.widthSize;
        float verticalStepSize = PLANE_SIZE / this.heightSize;

        float topZ = this.gameObject.transform.position.z + (PLANE_SIZE / 2) - (verticalStepSize / 2);
        float leftX = this.gameObject.transform.position.x - (PLANE_SIZE / 2) + (horizontalStepSize / 2);

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
