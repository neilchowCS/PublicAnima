using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUtilities
{
    public static (int x, int y) mirroredCoord((int x, int y) coord)
    {
        return (BattleManager.boardX - 1 - coord.x, BattleManager.boardY - 1 - coord.y);

    }
}
