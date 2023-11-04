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
    [SerializeField] private Level level;

    private GameState state = GameState.Idle;

    private void Start() 
    {
        this.level.BuildLevel();
        this.player.SetCellOrdinate(this.level.GetPlayerStartPosition());
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

        EnumMoveDirection moveDirection = EnumMoveDirection.None;

        switch(playerInput)
        {
            case EnumPlayerInput.MoveUp:
                moveDirection = EnumMoveDirection.Up;
                break;
            case EnumPlayerInput.MoveLeft:
                moveDirection = EnumMoveDirection.Left;
                break;
            case EnumPlayerInput.MoveRight:
                moveDirection = EnumMoveDirection.Right;
                break;
            case EnumPlayerInput.MoveDown:
                moveDirection = EnumMoveDirection.Down;
                break;
            default: 
                break;
        }

        if (moveDirection != EnumMoveDirection.None && !this.level.IsBlocked(this.player.GetCellOrdinate(), moveDirection))
        {
            this.player.MoveOneCell(moveDirection, onPlayerMoveCompleted);
            this.state = GameState.PlayerWalking;
        }
    }
}
