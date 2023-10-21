using System.Numerics;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Material groundMaterial;
    [SerializeField] int widthSize = 5;
    [SerializeField] int heightSize = 5;
    private void Awake() 
    {
        this.groundMaterial.SetFloat("_HorizontalSteps", this.widthSize);
        this.groundMaterial.SetFloat("_VerticalSteps", this.heightSize);
    }
}
