using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.CompilerServices;
using System.Linq;
using Unity.VisualScripting;
using Newtonsoft.Json;

public class PlayerInputModels
{
    public int Id;
}

public class InputHistory
{
    [SerializeField]
    public int order;
    [SerializeField]
    public List<PlayerInputModels> playerInputs;

    public InputHistory() {
        playerInputs = new List<PlayerInputModels>();
            }
    public void addHistory(InputHistory inputHistory)
    {
        playerInputs = playerInputs.Concat(inputHistory.playerInputs).ToList();
    }

    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }

}

[System.Serializable]
public class MovementInput : PlayerInputModels
{
    public int preX;
    public int preY;
    public int postX;
    public int postY;

    public MovementInput(int preX, int preY, int postX, int postY)
    {
        Id = 1;
        this.preX = preX;
        this.preY = preY;
        this.postX = postX;
        this.postY = postY;
    }
}