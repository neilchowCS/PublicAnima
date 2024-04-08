using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int pieceId;
    public string pieceName;
    public bool isPlayer1;
    public int x;
    public int y;
    public BattleManager manager;
    public MeshRenderer meshRenderer;
    public int energy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void init(BattleManager manager, string pieceName, bool isPlayer1, int x, int y, int energy)
    {
        this.manager = manager;
        this.pieceName = pieceName;
        this.isPlayer1 = isPlayer1;
        this.energy = energy;
        if (isPlayer1)
        {
            manager.p1Pieces.Add(this);
        }
        else
        {
            manager.p2Pieces.Add(this);
        }
        SetPosition(x, y);
    }

    public void SetPosition(int x, int y)
    {
        if (manager.gamePieces[this.x][this.y] == this)
        {
            manager.gamePieces[this.x][this.y] = null;
        }
        this.x = x;
        this.y = y;
        manager.gamePieces[x][y] = this;
        this.transform.position = new Vector3(manager.board.xCenter[x], 
            this.transform.position.y, manager.board.yCenter[y]);
    }

    public virtual void PerformAction()
    {

    }
    public virtual void TakeDamage(int amount)
    {
        energy -= amount;
        //healthBar.Refresh();
        SetProfile();
        if (energy <= 0)
        {
            HandleDeath();
        }
    }
    public virtual void TakeDamage(float amount)
    {
        TakeDamage((int)amount);
    }

    public virtual void HandleDeath()
    {

    }

    public virtual void SetProfile()
    {

    }
}
