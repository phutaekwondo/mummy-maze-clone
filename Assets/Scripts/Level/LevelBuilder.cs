using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] int widthSteps = 5;
    [SerializeField] int heightSteps = 5;
    [SerializeField] private Ground ground;

    private void Awake() 
    {
        this.ground.SetSize(this.widthSteps, this.heightSteps);
    }
}
