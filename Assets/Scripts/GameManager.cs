using System;
using DigitalRuby.Tween;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState {
        Idle,
        PlayerWalking,
    };

    [SerializeField] private Player player;
    [SerializeField] private InputHandler inputHanlder;

    private GameState state = GameState.Idle;

    private void Awake() 
    {
        this.player.SetCellPosition(new CellOrdinate(0,0));
    }

    private void Update()
    {
        this.HandleInput();
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

        Action<ITween<Vector3>> onPlayerMoveCompleted = (v) =>
        {
            this.state = GameState.Idle;
        };

        switch(playerInput)
        {
            case EnumPlayerInput.MoveUp:
                this.player.MoveOneCell(EnumMoveDirection.Up, onPlayerMoveCompleted);
                break;
            case EnumPlayerInput.MoveLeft:
                this.player.MoveOneCell(EnumMoveDirection.Left, onPlayerMoveCompleted);
                break;
            case EnumPlayerInput.MoveRight:
                this.player.MoveOneCell(EnumMoveDirection.Right, onPlayerMoveCompleted);
                break;
            case EnumPlayerInput.MoveDown:
                this.player.MoveOneCell(EnumMoveDirection.Down, onPlayerMoveCompleted);
                break;
            default: 
                break;
        }

        this.state = GameState.PlayerWalking;
    }
}
