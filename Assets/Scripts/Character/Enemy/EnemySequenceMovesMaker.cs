using UnityEngine;
using System;
using Unity.VisualScripting;

public class EnemySequenceMovesMaker : MonoBehaviour
{
    private Enemy controlledEnemy;
    private EnemyMoveFinder enemyMoveFinder;
    private int moveLimitEachTurn = 2;
    private int moveCount = 0;

    protected void Awake()
    {
        this.enemyMoveFinder = this.GetComponent<EnemyMoveFinder>();
        this.controlledEnemy = this.GetComponent<Enemy>();
    }

    public void StartSequenceMoves(CellOrdinate playerCellOrdinate, Level level, Action onComplete = null)
    {
        this.moveCount = 0;
        this.RecursiveMakeMove(playerCellOrdinate, level, onComplete);
    }

    private void RecursiveMakeMove(CellOrdinate playerCellOrdinate, Level level, Action onComplete = null)
    {
        if (this.moveCount >= this.moveLimitEachTurn)
        {
            onComplete?.Invoke();
            return;
        }

        EnumMoveDirection bestMove = this.enemyMoveFinder.GetEnemyBestMove(this.controlledEnemy.GetCellOrdinate(), playerCellOrdinate, level);

        if (bestMove == EnumMoveDirection.None)
        {
            this.controlledEnemy.ActBlocked(this.enemyMoveFinder.GetEnemyEnemyNoWallBestMove(this.controlledEnemy.GetCellOrdinate(), playerCellOrdinate));
            onComplete?.Invoke();
            return;
        }
        else 
        {
            this.controlledEnemy.Move(bestMove, () => {
                this.moveCount++;
                this.RecursiveMakeMove(playerCellOrdinate, level, onComplete);
            });
        }
    }
}
