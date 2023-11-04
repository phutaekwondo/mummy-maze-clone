using System;
using System.Diagnostics;
using DigitalRuby.Tween;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    enum GameState {
        Idle,
        PlayerWalking,
        EnemyMoving,
    };

    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private InputHandler inputHanlder;
    [SerializeField] private Level level;

    private GameState state = GameState.Idle;

    private void Start() 
    {
        this.level.BuildLevel();
        this.player.SetCellOrdinate(this.level.GetPlayerStartPosition());
        this.enemy.SetCellOrdinate(this.level.GetEnemyStartPosition());
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
            case GameState.EnemyMoving:
                Action<ITween<Vector3>> onEnemyMoveCompleted = (v) =>
                {
                    this.EnterState(GameState.Idle);
                };

                this.enemy.MoveOneCell(EnumMoveDirection.Down, onEnemyMoveCompleted);
                break;
            default:
                break;
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
            this.EnterState(GameState.EnemyMoving);
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
            this.EnterState(GameState.PlayerWalking);
        }
    }
}
