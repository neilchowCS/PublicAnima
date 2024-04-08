using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : GamePiece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void init(BattleManager manager, string pieceName, bool isPlayer1, int x, int y, int energy)
    {
        base.init(manager, pieceName, isPlayer1, x, y, energy);
        this.pieceId = 1;
    }

    public override void PerformAction()
    {
        GamePiece target;
        if (isPlayer1)
        {
            target = manager.p2Pieces[Random.Range(0, manager.p2Pieces.Count)];
        }
        else
        {
            target = manager.p1Pieces[Random.Range(0, manager.p1Pieces.Count)];
        }

        manager.battleHandler.SetBattleLogUI($"{(isPlayer1 ? "Ally" : "Enemy")} {x},{y} hit {target.x},{target.y} {target.pieceName} for {(int)(energy * 0.2f)} damage");
        target.TakeDamage(energy * 0.2f);
    }

    public override void HandleDeath()
    {
        manager.gamePieces[x][y] = null;
        if (isPlayer1)
        {
            manager.p1Pieces.Remove(this);
        }
        else
        {
            manager.p2Pieces.Remove(this);
        }
        this.gameObject.SetActive(false);
    }

    public override void SetProfile()
    {
        if (manager.selectedSpirit.selectedSpirit == this)
        {
            manager.selectedSpirit.SetText();
        }
    }
}