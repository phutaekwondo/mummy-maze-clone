using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState {
        Idle,
        PlayerMoving,
        EnemyMoving,
    };

    [SerializeField] private Level level;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    private GameState state = GameState.Idle;
    private ResultChecker resultChecker;
    private InputHandler inputHandler;

    public GameManager()
    {
        this.inputHandler = new InputHandler();
        this.resultChecker = new ResultChecker();
    }

    private void Start() 
    {
        this.level.BuildLevel();
        this.player.SetCellOrdinate(this.level.GetPlayerStartPosition());
        this.enemy.SetCellOrdinate(this.level.GetEnemyStartPosition());
    }

    private void Update()
    {
        this.HandleInput();

        //test
        //TODO: implement the flow to check the result
        //after each move of player and enemy
        ResultType resultType = this.resultChecker.CheckResult(this.level, this.player.GetCellOrdinate(), this.enemy.GetCellOrdinate());
        if (resultType == ResultType.Lose)
        {
            Debug.Log("You lose");
        }
        if (resultType == ResultType.Win)
        {
            Debug.Log("You win");
        }
    }

    private void EnterState(GameState state)
    {
        this.state = state;
        switch(state)
        {
            case GameState.EnemyMoving:
                this.enemy.MakeBestMove(this.player.GetCellOrdinate(), this.level, () => {
                    this.EnterState(GameState.Idle);
                });
                break;
        }
    }

    private void HandleInput()
    {
        switch(this.state) 
        {
            case GameState.Idle: 
                EnumPlayerInput playerInput = this.inputHandler.GetPlayerInput();
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

        this.MovePlayer(moveDirection);
    }

    private void MovePlayer(EnumMoveDirection moveDirection)
    {
        Action onPlayerMoveCompleted = () =>
        {
            this.EnterState(GameState.EnemyMoving);
        };

        bool isBlocked = this.level.IsBlocked(this.player.GetCellOrdinate(), moveDirection);

        if (isBlocked)
        {
            this.player.ActBlocked(moveDirection);
        }
        else
        {
            this.player.Move(moveDirection, onPlayerMoveCompleted);
            this.EnterState(GameState.PlayerMoving);
        }
    }
}
