using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState {
        Idle,
        PlayerWalking,
    };

    [SerializeField] private Ground ground;
    [SerializeField] private Player player;
    [SerializeField] private InputHandler inputHanlder;
    [SerializeField] int widthSteps = 5;
    [SerializeField] int heightSteps = 5;

    private GameState state = GameState.Idle;

    private void Awake() 
    {
        this.ground.SetSize(this.widthSteps, this.heightSteps);
        this.player.SetCellPosition(new CellOrdinate());
        this.player.WalkToCell(new CellOrdinate(4,4));
    }

    private void Update()
    {
    }

    private void HandleInput()
    {
        switch(this.state) 
        {
            case GameState.Idle: 
                EnumPlayerInput playerInput = this.inputHanlder.GetPlayerInput();
                this.HandlePlayerInput(playerInput);
                break;
            case GameState.PlayerWalking:
                break;
            default:
                break;
        }
    }

    private void HandlePlayerInput(EnumPlayerInput playerInput)
    {
        if (playerInput == EnumPlayerInput.None) {
            return;
        }
    }
}
