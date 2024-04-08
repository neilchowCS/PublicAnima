using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BasicGameState
{
    public List<int> gamePieceId = new List<int>();
    public List<int> posX = new List<int>();
    public List<int> posY = new List<int>();

    public BasicGameState()
    {

    }
    public BasicGameState(List<int> gamePieceId, List<int> posX, List<int> posY)
    {
        for (int i = 0; i < gamePieceId.Count; i++)
        {
            this.gamePieceId.Add(gamePieceId[i]);
            this.posX.Add(posX[i]);
            this.posY.Add(posY[i]);
        }
    }
}
