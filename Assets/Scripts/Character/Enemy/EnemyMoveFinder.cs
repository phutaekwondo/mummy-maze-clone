using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

public class EnemyMoveFinder : MonoBehaviour
{
    public EnumMoveDirection GetEnemyBestMove(CellOrdinate enemyCellOrdinate, CellOrdinate playerCellOrdinate, Level level)
    {
        List<KeyValuePair<EnumMoveDirection, float>> distanceDiffs = this.GetDistanceDiffAfterMove(enemyCellOrdinate, playerCellOrdinate);

        for (int i = 0; i < distanceDiffs.Count; i++)
        {
            if (!level.IsBlocked(enemyCellOrdinate, distanceDiffs[i].Key))
            {
                return distanceDiffs[i].Key;
            }
        }

        return EnumMoveDirection.None;
    }

    public EnumMoveDirection GetEnemyEnemyNoWallBestMove(CellOrdinate enemyCellOrdinate, CellOrdinate playerCellOrdinate)
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

        List<KeyValuePair<EnumMoveDirection, float>> distanceDiffs = distanceDiffAfterMove.ToList();
        distanceDiffs.Sort(delegate(KeyValuePair<EnumMoveDirection, float> pair1, KeyValuePair<EnumMoveDirection, float> pair2) {
            return pair1.Value.CompareTo(pair2.Value);
        });

        return distanceDiffs;
    }
}
