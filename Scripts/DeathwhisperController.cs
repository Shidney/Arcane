using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathwhisperController : MonoBehaviour
{
    DataMinion minionDied = null;
    public void Deathwhisper(int ID, string board)
    {
        switch (ID)
        {
            case 11:
                foreach (Transform t in GameObject.Find(board).GetComponentInChildren<Transform>())
                {
                    Destroy(t.gameObject);
                }
                if (board.Equals("TableBot"))
                {
                    GameObject.Find("CardController").GetComponent<CardController>().mesa.Clear();
                    for (int i = 0; i < 7; i++)
                    {
                        if (GameObject.Find("CardController").GetComponent<CardController>().AddTableCard())
                        {
                            Utils.GetLoader().LoadMinion(board, new DataCard(300, "Alma corrupta", "", "Images/Skeleton", CardRarity.GRATIS, CardType.ESBIRRO, CardRaza.NIGROMANTE, 2, 3, 3));
                        }
                    }
                }
                else
                {
                    GameObject.Find("CardController").GetComponent<CardController>().mesaEnemiga.Clear();
                    for (int i = 0; i < 7; i++)
                    {
                        if (GameObject.Find("CardController").GetComponent<CardController>().AddTableEnemyCard())
                        {
                            Utils.GetLoader().LoadMinion(board, new DataCard(300, "Alma corrupta", "", "Images/Skeleton", CardRarity.GRATIS, CardType.ESBIRRO, CardRaza.NIGROMANTE, 2, 3, 3));
                        }
                    }
                }
                break;
        }
    }

    public void LateUpdate()
    {
        StopAllCoroutines();
        StartCoroutine(DeathBoard());
    }

    public IEnumerator DeathBoard()
    {
        yield return new WaitUntil(() => DeathController("TableTop"));

        if (MinionController.GetProperty(minionDied, "DEATHWHISPER"))
        {
            Deathwhisper(minionDied.ID, "TableTop");
        }
        if (minionDied != null)
        {
            Destroy(GameObject.Find(minionDied.Nombre));
            minionDied = null;
        }

        yield return new WaitUntil(() => DeathController("TableBot"));

        if (MinionController.GetProperty(minionDied, "DEATHWHISPER"))
        {
            Deathwhisper(minionDied.ID, "TableBot");
        }
        if (minionDied != null)
        {
            Destroy(GameObject.Find(minionDied.Nombre));
            minionDied = null;
        }
    }

    private bool DeathController(string v)
    {
        foreach (Transform t in GameObject.Find(v).GetComponentInChildren<Transform>())
        {
            if (v == "TableTop")
            {
                if (t.gameObject.GetComponent<Interactable>() != null)
                {
                    Destroy(t.gameObject.GetComponent<Interactable>());
                }
            }
            if (t.gameObject.GetComponent<LoadCardValues>() != null)
            {
                DataMinion minion = t.gameObject.GetComponent<LoadCardValues>().getMinion();
                if (MinionController.isDead(minion))
                {
                    minionDied = minion;
                }
            }
        }

        return (minionDied != null);
    }
}


