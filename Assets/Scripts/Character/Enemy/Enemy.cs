using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class Enemy : Character
{
    DoctorAnimStateController animStateController;

    private void Awake() 
    {
        this.animStateController = this.GetComponent<DoctorAnimStateController>();
    }

    public EnumMoveDirection FindNextMove(Level level, CellOrdinate playerOrdinate)
    {
        List<KeyValuePair<EnumMoveDirection, float>> distanceDiffs = this.GetTopBestMoves(level, playerOrdinate);

        for (int i = 0; i < distanceDiffs.Count; i++)
        {
            if (!level.IsBlocked(this.cellOrdinate, distanceDiffs[i].Key))
            {
                return distanceDiffs[i].Key;
            }
        }

        return EnumMoveDirection.None;
    }

    private List<KeyValuePair<EnumMoveDirection, float>> GetTopBestMoves(Level level, CellOrdinate playerOrdinate) 
    {
        Dictionary<EnumMoveDirection, float> distanceDiffAfterMove = new Dictionary<EnumMoveDirection, float>();
        Vector3 enemyPosition = CellTransformGetter.Instance.GetCellPosition(this.cellOrdinate);
        Vector3 playerPosition = CellTransformGetter.Instance.GetCellPosition(playerOrdinate);

        float currentDistance = (enemyPosition - playerPosition).magnitude;

        foreach (EnumMoveDirection key in Enum.GetValues(typeof(EnumMoveDirection))) 
        {
            CellOrdinate destinate = this.cellOrdinate.GetDestinateOrdinate(key);
            Vector3 desPosition = CellTransformGetter.Instance.GetCellPosition(destinate);
            distanceDiffAfterMove[key] = (desPosition - playerPosition).magnitude - currentDistance;
        }

        List<KeyValuePair<EnumMoveDirection, float>> distanceDiffs = distanceDiffAfterMove.ToList();
        distanceDiffs.Sort(delegate(KeyValuePair<EnumMoveDirection, float> pair1, KeyValuePair<EnumMoveDirection, float> pair2) {
            return pair1.Value.CompareTo(pair2.Value);
        });

        return distanceDiffs;
    }

    protected override void PlayMovementAnimation()
    {
        this.animStateController.StartRun();
    }

    protected override void StopMovementAnimation()
    {
        this.animStateController.StartIdle();
    }
}
