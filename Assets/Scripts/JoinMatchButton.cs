using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;

public class JoinMatchButton : MonoBehaviour
{
    public ApiConnection apiConnection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryStartMatch()
    {
        List<int> gamePieces = new List<int>() { 0, 1, 1};
        List<int> posX = new List<int>() { 0, 1, 2 };
        List<int> posY = new List<int>() { 0, 1, 2 };
        BasicGameState gameState = new BasicGameState(gamePieces, posX, posY);
        string json = JsonUtility.ToJson(gameState);
        apiConnection.dataTransfer.SelfState = gameState;

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        StartCoroutine(apiConnection.Upload(bodyRaw));
    }
}
