using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Enemy : Character
{
    private EnemySequenceMovesMaker enemySequenceMovesMaker;

    protected override void Awake()
    {
        base.Awake();
        this.enemySequenceMovesMaker = this.GetComponent<EnemySequenceMovesMaker>();
    }

    public void MakeBestMove(CellOrdinate playerCellOrdinate, Level level, Action onMoveComplete = null) 
    {
        this.enemySequenceMovesMaker.StartSequenceMoves(playerCellOrdinate, level, onMoveComplete); 
    }
}
