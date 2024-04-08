using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public bool isPlayer1;
    public List<int> pieceId = new List<int>();
    public GameState(Animist animist)
    {
        pieceId.Add(animist.pieceId);
        isPlayer1 = animist.isPlayer1;
    }

    public GameState(List<GamePiece> gamePieces)
    {
        foreach (GamePiece x in gamePieces)
        {
            pieceId.Add(x.pieceId);
        }
        isPlayer1 = gamePieces[0].isPlayer1;
    }
}
