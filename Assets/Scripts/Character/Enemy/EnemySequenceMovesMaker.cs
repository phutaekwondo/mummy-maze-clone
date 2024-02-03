using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public class EnemySequenceMovesMaker
{
    private Enemy controlledEnemy;
    private EnemyMoveFinder enemyMoveFinder;
    private int moveLimitEachTurn = 2;

    public EnemySequenceMovesMaker(Enemy controlledEnemy): base()
    {
        this.controlledEnemy = controlledEnemy;
        this.enemyMoveFinder = new EnemyMoveFinder();
    }

    public void StartSequenceMoves(CellOrdinate playerCellOrdinate, Level level, Action onComplete = null)
    {
        List<EnumMoveDirection> sequenceMoves = this.enemyMoveFinder.GetSequenceMoves(
            this.moveLimitEachTurn, 
            this.controlledEnemy.GetCellOrdinate(), 
            playerCellOrdinate, 
            level);
        
        int moveCount = sequenceMoves.Count;

        Action onMovesComplete = () => {
            if (moveCount < this.moveLimitEachTurn)
            {
                EnumMoveDirection lookToPlayerDirection = this.enemyMoveFinder.GetLookAtPlayerDirection(
                    this.controlledEnemy.GetCellOrdinate(), 
                    playerCellOrdinate
                );
                if (lookToPlayerDirection != EnumMoveDirection.None)
                {
                    this.controlledEnemy.ActBlocked(lookToPlayerDirection);
                }
            }
            onComplete?.Invoke();
        };
        this.RecursiveMakeMove(sequenceMoves, playerCellOrdinate, onMovesComplete);
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
            this.RecursiveMakeMove(leftMoves, playerCellOrdinate, onComplete);
        });
    }
}
