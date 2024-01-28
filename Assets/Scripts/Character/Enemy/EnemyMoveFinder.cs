using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemyMoveFinder
{
    public List<EnumMoveDirection> GetSequenceMoves
    (
        int moveLimitEachTurn,
        CellOrdinate enemyCellOrdinate, 
        CellOrdinate playerCellOrdinate, 
        Level level
    )
    {
        List<List<EnumMoveDirection>> possibleSequences = new List<List<EnumMoveDirection>>();
        this.RecursiveFindMove(moveLimitEachTurn, enemyCellOrdinate, playerCellOrdinate, level, new List<EnumMoveDirection>(), possibleSequences);

        int closestToPlayerIndex = 0;
        float closestDistance = this.GetPlayerDistanceAfterSequenceMoves(possibleSequences[closestToPlayerIndex], enemyCellOrdinate, playerCellOrdinate);
        for (int i = 1; i < possibleSequences.Count; i++)
        {
            if (this.GetPlayerDistanceAfterSequenceMoves(possibleSequences[i], enemyCellOrdinate, playerCellOrdinate) < this.GetPlayerDistanceAfterSequenceMoves(possibleSequences[closestToPlayerIndex], enemyCellOrdinate, playerCellOrdinate))
            {
                closestToPlayerIndex = i;
            }
        }

        return possibleSequences[closestToPlayerIndex];
    }

    private float GetPlayerDistanceAfterSequenceMoves
    (
        List<EnumMoveDirection> sequenceMoves,
        CellOrdinate enemyCellOrdinate,
        CellOrdinate playerCellOrdinate
    )
    {
        CellOrdinate currentCellOrdinate = enemyCellOrdinate;
        for (int i = 0; i < sequenceMoves.Count; i++)
        {
            currentCellOrdinate = currentCellOrdinate.GetDestinateOrdinate(sequenceMoves[i]);
        }

        Vector3 enemyPosition = CellTransformGetter.Instance.GetCellPosition(currentCellOrdinate);
        Vector3 playerPosition = CellTransformGetter.Instance.GetCellPosition(playerCellOrdinate);

        return (enemyPosition - playerPosition).magnitude;
    }

    private void RecursiveFindMove
    (
        int moveLimitEachTurn,
        CellOrdinate enemyCellOrdinate, 
        CellOrdinate playerCellOrdinate, 
        Level level,
        List<EnumMoveDirection> currentSequence,
        List<List<EnumMoveDirection>> possibleSequences
    )
    {
        if (currentSequence.Count == moveLimitEachTurn)
        {
            possibleSequences.Add(currentSequence);
            return;
        }

        List<EnumMoveDirection> nextMoves = this.GetEnemyNextMoves(enemyCellOrdinate, playerCellOrdinate, level);

        if (nextMoves.Count == 0)
        {
            possibleSequences.Add(currentSequence);
            return;
        }

        for (int i = 0; i < nextMoves.Count; i++)
        {
            EnumMoveDirection nextMove = nextMoves[i];

            List<EnumMoveDirection> nextSequence = new List<EnumMoveDirection>(currentSequence);
            nextSequence.Add(nextMove);
            RecursiveFindMove(
                moveLimitEachTurn, 
                enemyCellOrdinate.GetDestinateOrdinate(nextMove), 
                playerCellOrdinate, 
                level, 
                nextSequence, 
                possibleSequences
            );
        }
    }

    private List<EnumMoveDirection> GetEnemyNextMoves(CellOrdinate enemyCellOrdinate, CellOrdinate playerCellOrdinate, Level level)
    {
        List<KeyValuePair<EnumMoveDirection, float>> distanceDiffs = this.GetDistanceDiffAfterMove(enemyCellOrdinate, playerCellOrdinate);

        List<EnumMoveDirection> nextMoves = new List<EnumMoveDirection>();

        for (int i = 0; i < distanceDiffs.Count; i++)
        {
            if (!level.IsBlocked(enemyCellOrdinate, distanceDiffs[i].Key) && distanceDiffs[i].Value < 0)
            {
                nextMoves.Add(distanceDiffs[i].Key);
            }
        }

        return nextMoves;
    }

    public EnumMoveDirection GetLookAtPlayerDirection(CellOrdinate enemyCellOrdinate, CellOrdinate playerCellOrdinate)
    {
        List<KeyValuePair<EnumMoveDirection, float>> distanceDiffs = this.GetDistanceDiffAfterMove(enemyCellOrdinate, playerCellOrdinate);

        return distanceDiffs[0].Key;
    }

    private List<KeyValuePair<EnumMoveDirection, float>> GetDistanceDiffAfterMove(CellOrdinate enemyCellOrdinate, CellOrdinate playerCellOrdinate)
    {
        Dictionary<EnumMoveDirection, float> distanceDiffAfterMove = new Dictionary<EnumMoveDirection, float>();
        Vector3 enemyPosition = CellTransformGetter.Instance.GetCellPosition(enemyCellOrdinate);
        Vector3 playerPosition = CellTransformGetter.Instance.GetCellPosition(playerCellOrdinate);

        float currentDistance = (enemyPosition - playerPosition).magnitude;

        foreach (EnumMoveDirection key in Enum.GetValues(typeof(EnumMoveDirection))) 
        {
            CellOrdinate destinate = enemyCellOrdinate.GetDestinateOrdinate(key);
            Vector3 desPosition = CellTransformGetter.Instance.GetCellPosition(destinate);
            distanceDiffAfterMove[key] = (desPosition - playerPosition).magnitude - currentDistance;
        }

        return this.SortDistanceDiffAfterMove(distanceDiffAfterMove.ToList());
    }

    private List<KeyValuePair<EnumMoveDirection, float>> SortDistanceDiffAfterMove(List<KeyValuePair<EnumMoveDirection, float>> distanceDiffs)
    {
        distanceDiffs.Sort(delegate(KeyValuePair<EnumMoveDirection, float> pair1, KeyValuePair<EnumMoveDirection, float> pair2) {
            return pair1.Value.CompareTo(pair2.Value);
        });

        return distanceDiffs;
    }
}
