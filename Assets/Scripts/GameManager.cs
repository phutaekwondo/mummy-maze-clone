using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ground ground;
    [SerializeField] private Player player;
    [SerializeField] int widthSteps = 5;
    [SerializeField] int heightSteps = 5;

    private void Awake() 
    {
        this.ground.SetSize(this.widthSteps, this.heightSteps);
        this.player.SetPosition(this.ground.GetPosition(0,0));
    }
}
