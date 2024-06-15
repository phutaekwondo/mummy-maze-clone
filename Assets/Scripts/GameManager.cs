using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    private GameStateMachine gameStateMachine;
    private ResultChecker resultChecker;

    public GameManager()
    {
        this.gameStateMachine = new GameStateMachine(this);
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
        this.gameStateMachine.Update();
    }

    public void MoveEnemy(Action onComplete)
    {
        this.enemy.MakeBestMove(this.player.GetCellOrdinate(), this.level, onComplete);
    }

    public void MovePlayer(EnumMoveDirection moveDirection, Action onComplete)
    {
        bool isBlocked = this.level.IsBlocked(this.player.GetCellOrdinate(), moveDirection);

        if (isBlocked)
        {
            this.player.ActBlocked(moveDirection);
        }
        else
        {
            this.player.Move(moveDirection, onComplete);
        }
    }

    public ResultType CheckResult()
    {
        return this.resultChecker.CheckResult(this.level, this.player.GetCellOrdinate(), this.enemy.GetCellOrdinate());
    }
}
