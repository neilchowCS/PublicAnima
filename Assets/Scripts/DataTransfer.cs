using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    [field: SerializeField]
    public BasicGameState SelfState { get; set; }
    [field: SerializeField]
    public ClientBattleResponse Response { get; set; }
}
