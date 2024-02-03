using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public class EnemySequenceMovesMaker
{
    private Enemy controlledEnemy;
    private EnemyMoveFinder enemyMoveFinder;
    private int moveLimitEachTurn = 2;
    private int moveCount = 0;

    public EnemySequenceMovesMaker(Enemy controlledEnemy): base()
    {
        this.controlledEnemy = controlledEnemy;
        this.enemyMoveFinder = new EnemyMoveFinder();
    }

    public void StartSequenceMoves(CellOrdinate playerCellOrdinate, Level level, Action onComplete = null)
    {
        this.moveCount = 0;
        List<EnumMoveDirection> sequenceMoves = this.enemyMoveFinder.GetSequenceMoves(
            this.moveLimitEachTurn, 
            this.controlledEnemy.GetCellOrdinate(), 
            playerCellOrdinate, 
            level);
        this.RecursiveMakeMove(sequenceMoves, playerCellOrdinate, onComplete);
    }

    public void RecursiveMakeMove(List<EnumMoveDirection> leftMoves, CellOrdinate playerCellOrdinate, Action onComplete = null)
    {
        if (leftMoves.Count == 0)
        {
            onComplete?.Invoke();
            return;
        }

        EnumMoveDirection nextMove = leftMoves[0];
        leftMoves.RemoveAt(0);

        this.controlledEnemy.Move(nextMove, () => {
            this.moveCount++;
            this.RecursiveMakeMove(leftMoves, playerCellOrdinate, onComplete);
        });
    }
}
