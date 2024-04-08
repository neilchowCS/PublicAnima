using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHandler : MonoBehaviour
{
    public BattleManager manager;
    public TextMeshProUGUI battleLogUI;

    [SerializeField]
    private float speed = 1.0f;
    private int p1Pointer = 0;
    private int p2pointer = 0;
    private float timer = 0;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= speed)
        {
            timer -= speed;
            if (p1Pointer < manager.p1Pieces.Count || p2pointer < manager.p2Pieces.Count)
            {
                if (p1Pointer >= manager.p1Pieces.Count)
                {
                    manager.p2Pieces[p2pointer].PerformAction();
                    p2pointer++;
                }
                else if (p2pointer >= manager.p2Pieces.Count)
                {
                    manager.p1Pieces[p1Pointer].PerformAction();
                    p1Pointer++;
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        manager.p1Pieces[p1Pointer].PerformAction();
                        p1Pointer++;
                    }
                    else
                    {
                        manager.p2Pieces[p2pointer].PerformAction();
                        p2pointer++;
                    }
                }
            }
            else
            {
                EndCombat();
            }
        }
    }

    public void StartCombat()
    {
        Shuffle(manager.p1Pieces);
        Shuffle(manager.p2Pieces);

        p1Pointer = 0;
        p2pointer = 0;
        timer = 0;

        this.enabled = true;
        SetBattleLogUI("Battling");
    }

    public void EndCombat()
    {
        manager.isTurnOver = false;
        this.enabled = false;
        battleLogUI.gameObject.SetActive(false);
    }

    public void Shuffle(List<GamePiece> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GamePiece temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    public void SetBattleLogUI(string text)
    {
        battleLogUI.gameObject.SetActive(true);
        battleLogUI.text = text;
    }
}
