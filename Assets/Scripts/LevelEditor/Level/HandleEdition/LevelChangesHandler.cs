using System.Collections.Generic;

namespace LevelEditor
{
    public interface LevelChangesHandler
    {
        void ChangeGroundSize(int groundSize);
        void ChangePlayerPosition(CellOrdinate playerPosition);
        void ChangeEnemyPosition(CellOrdinate enemyPosition);
        void ChangeWalls(List<Wall> walls);
        void ChangeExitDoor(BlockedCell exitDoor);
    }
}