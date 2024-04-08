using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animist : GamePiece
{
    public bool hasHarvested;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(BattleManager manager, bool isPlayer1, int x, int y, int health)
    {
        base.init(manager, "Animist", isPlayer1, x, y, health);
        this.hasHarvested = false;
        this.pieceId = 0;
    }

    public override void HandleDeath()
    {
        manager.battleHandler.EndCombat();
        if (isPlayer1)
        {
            Debug.Log("p2 win");
            manager.battleHandler.SetBattleLogUI("P2 wins!");
        }
        else
        {
            Debug.Log("p1 win");
            manager.battleHandler.SetBattleLogUI("P1 wins!");
        }
    }

    public override void SetProfile()
    {
        if (manager.selectedAnimist.selectedAnimist == this)
        {
            manager.selectedAnimist.SetText();
        }
    }
}