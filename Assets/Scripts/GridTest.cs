using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    public BattleManager manager;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < manager.board.width; i++)
        {
            for (int j = 0; j < manager.board.length; j++)
            {
                /*
                Spirit x = Instantiate(manager.spiritPrefabs[0]);
                x.init(manager, i, j, true);
                x.name = j * manager.board.width + i + "";
                x.health = 100;
                x.maxHealth = 100;
                x.energy = Random.Range(0, 100);
                */
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
