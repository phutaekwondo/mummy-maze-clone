using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Enemy : Character
{
    private EnemyMoveFinder enemyMoveFinder;

    protected override void Awake()
    {
        base.Awake();
        this.enemyMoveFinder = this.GetComponent<EnemyMoveFinder>();
    }

    public void MakeBestMove(CellOrdinate playerCellOrdinate, Level level, Action onMoveComplete = null) 
    {
        EnumMoveDirection bestMove = this.enemyMoveFinder.GetEnemyBestMove(this.GetCellOrdinate(), playerCellOrdinate, level);
        if (bestMove != EnumMoveDirection.None)
        {
            this.Move(bestMove, onMoveComplete);
        }
    }
}
