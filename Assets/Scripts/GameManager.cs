using System;
using DigitalRuby.Tween;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState {
        Idle,
        PlayerMoving,
        EnemyMoving,
    };

    [SerializeField] private InputHandler inputHanlder;
    [SerializeField] private Level level;
    [SerializeField] private Player player;

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

    private void EnterState(GameState state)
    {
        this.state = state;
        switch(state)
        {
        }
    }

    private void HandleInput()
    {
        switch(this.state) 
        {
            case GameState.Idle: 
                EnumPlayerInput playerInput = this.inputHanlder.GetPlayerInput();
                this.HandlePlayerInput(playerInput);
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

        Action onPlayerMoveCompleted = () =>
        {
            this.EnterState(GameState.Idle);
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

        this.player.Move(moveDirection, onPlayerMoveCompleted);
        this.EnterState(GameState.PlayerMoving);
    }
}
