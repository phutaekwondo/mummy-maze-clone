using UnityEngine;

public class CellOrdinateCalculator
{
    public CellOrdinate FromPosition(Ground ground, Vector3 position)
    {
        int groundWidth = (int)ground.GetWidthSize();
        int groundLength = (int)ground.GetHeightSize();
        Vector3 cellSize = ground.GetCellSize();

        float PLANE_SIZE = UnityDefaultParameter.DEFAULT_PLANE_SIZE;

        float cornerX = ground.transform.position.x - (PLANE_SIZE * ground.transform.localScale.x) / 2;
        float cornerZ = ground.transform.position.z + (PLANE_SIZE * ground.transform.localScale.z) / 2;

        float x = (position.x - cornerX) / cellSize.x;
        float y = (cornerZ - position.z) / cellSize.z;

        if (x < 0 || x >= groundWidth || y < 0 || y >= groundLength)
        {
            return null;
        }

        return new CellOrdinate((int)x, (int)y);
    }
}