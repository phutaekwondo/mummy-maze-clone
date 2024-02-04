using System;

public class CellOrdinateFactory
{
    private static CellOrdinateFactory instance;
    public static CellOrdinateFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CellOrdinateFactory();
            }
            return instance;
        }
    }

    public CellOrdinate GetCellOrdinateFromCellIndex(Level level, int cellIndex)
    {
        return Parse2CellOrdinate(level.GetGroundSize(), level.GetGroundSize(), cellIndex);
    }

    private CellOrdinate Parse2CellOrdinate(int groundWidth,int groundHeight, int cellIndex) 
    {
        int xOrdinate = cellIndex % Convert.ToInt32(groundWidth);
        int zOrdinate = cellIndex / Convert.ToInt32(groundHeight);
        return new CellOrdinate(xOrdinate, zOrdinate);
    }
}
