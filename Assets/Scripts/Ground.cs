using System.Numerics;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Material groundMaterial;
    int widthSize = 5;
    int heightSize = 5;
    
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
