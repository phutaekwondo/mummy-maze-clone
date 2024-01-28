using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Enemy : Character
{
    private EnemySequenceMovesMaker enemySequenceMovesMaker;

    Enemy(): base()
    {
        this.enemySequenceMovesMaker = new EnemySequenceMovesMaker(this);
    }

    public void MakeBestMove(CellOrdinate playerCellOrdinate, Level level, Action onMoveComplete = null) 
    {
        this.enemySequenceMovesMaker.StartSequenceMoves(playerCellOrdinate, level, onMoveComplete); 
    }
}
