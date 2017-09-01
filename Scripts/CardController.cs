using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    GameObject instance;
    GameObject BattlecryObject;
    BattlecryController Battlecry;
    public int CountHand;
    public int CountTable;
    public int CountTableEnemiga;
    public ArrayList mano = new ArrayList();
    public ArrayList mesa = new ArrayList();
    public ArrayList mesaEnemiga = new ArrayList();
    public List<DataCard> deck = new List<DataCard>();
    public int capacityHand = 10;//IMPORTANTE MODIFICARLO A GUSTO
    public int capacityTable = 7;//IMPORTANTE MODIFICARLO A GUSTO  

    public void Awake()
    {
        instance = this.gameObject;
        BattlecryObject = GameObject.Find("BattlecryController");
        Battlecry = BattlecryObject.GetComponent<BattlecryController>();
    }
    public DataCard FindCardByID(int id)
    {
        foreach (DataCard card in Database.cartastotales)
        {
            if (card.ID == id)
            {
                return card;
            }
        }
        return null;
    }
    public bool AddHandCard(DataCard c)
    {
        if (mano.Count < capacityHand)
        {
            mano.Add(c);
            return true;
        }
        return false;
    }
    public bool RemoveHandCard(DataCard c)
    {
        int i = -1;
        for (int j = 0; j < mano.Count; j++)
        {
            if (((DataCard)mano[j]).ID.Equals(c.ID))
            {
                i = j;
            }
        }
        if (i != -1)
        {
            mano.RemoveAt(i);
            return true;
        }
        return false;
    }
    public bool AddTableCard()
    {
        if (mesa.Count < capacityTable)
        {
            return true;
        }
        else
        {
            Utils.GetLogger().ShowMessage("Tengo demasiados esbirros", 2, Color.red);
        }
        return false;
    }
    public bool AddTableCard(DataMinion c)
    {
        if (mesa.Count < capacityTable)
        {
            mesa.Add(c);
            return true;
        }
        else
        {
            Utils.GetLogger().ShowMessage("Tengo demasiados esbirros", 2, Color.red);
        }
        return false;
    }
    public bool RemoveTableCard(DataMinion c)
    {
        if (mesa.IndexOf(c) != -1)
        {
            mesa.Remove(c);
            return true;
        }
        return false;
    }
    public bool AddTableEnemyCard()
    {
        if (mesaEnemiga.Count < capacityTable)
        {
            return true;
        }
        return false;
    }
    public bool AddTableEnemyCard(DataMinion c)
    {
        if (mesaEnemiga.Count < capacityTable)
        {
            mesaEnemiga.Add(c);
            return true;
        }
        return false;
    }
    public bool RemoveTableEnemigaCard(DataMinion c)
    {
        if (mesaEnemiga.IndexOf(c) != -1)
        {
            mesaEnemiga.Remove(c);
            return true;
        }
        return false;
    }
    public void UpdateValuesCards()
    {
        mano.Clear();
        mesa.Clear();
        mesaEnemiga.Clear();
        //
        GameObject ManoObject = GameObject.Find("HandPanel");
        var elements = ManoObject.GetComponentsInChildren<LayoutElement>();
        foreach (LayoutElement ele in elements)
        {
            if (ele.gameObject.name == "PlaceHolder")
            {
                continue;
            }
            AddHandCard(ele.gameObject.GetComponent<LoadCardValues>().getCarta());
        }
        //
        GameObject MesaObject = GameObject.Find("TableBot");
        var elements2 = MesaObject.GetComponentsInChildren<LayoutElement>();
        foreach (LayoutElement ele in elements2)
        {
            if (ele.gameObject.name == "PlaceHolder")
            {
                continue;
            }
            AddTableCard(ele.gameObject.GetComponent<LoadCardValues>().getMinion());
        }
        //
        GameObject MesaEnemyObject = GameObject.Find("TableTop");
        var elements3 = MesaEnemyObject.GetComponentsInChildren<LayoutElement>();
        foreach (LayoutElement ele in elements3)
        {
            AddTableEnemyCard(ele.gameObject.GetComponent<LoadCardValues>().getMinion());
        }
        CountHand = mano.Count;
        CountTable = mesa.Count;
        CountTableEnemiga = mesaEnemiga.Count;
    }

    public bool PlayCard(GameObject obj)
    {
        DataCard c = ((LoadCardValues)Utils.Search(obj, "LoadCardValues")).getCarta();
        if (PlayerController.CanPlayByMana(c.CosteMana))
        {
            switch (c.Type)
            {
                case CardType.HECHIZO:
                    DataSpell dataspell = CardActionSet.GetSpellProperty(c.ID);
                    PlaySpell(c, obj);
                    return true;
                case CardType.ESBIRRO:
                    PlayMinion(c, obj);
                    return true;
                case CardType.ARMA:
                    PlayWeapon(c, obj);
                    return true;
                default:
                    return false;
            }           
        }
        else
        {
            return false;
        }
    }
    private void PlayMinion(DataCard c,GameObject obj)
    {
        if (AddTableCard(DataCard.toDataMinion(c)))
        {
            //BATTLECRY LIST           
            Utils.GetLoader().LoadMinion("TableBot", c);
            Destroy(obj);
            PlayerController.RemoveMana(c.CosteMana);
            if (MinionController.GetProperty(DataCard.toDataMinion(c), "BATTLECRY"))
            {
                Battlecry.Battlecry(c, obj);
            }
        }
        else
        {
            Utils.GetLogger().ShowMessage("Tengo demasiados esbirros", 2, Color.yellow);
        }
    }

    
    public IEnumerator spell(DataSpell dataspell, GameObject obj, DataCard c)
    {
        Utils.getTargeter().setStartPosition(obj.transform.position);
        Utils.getTargeter().target = true;
        Utils.getTargeter().targeting = true;
        yield return new WaitUntil(() => Utils.getTargeter().Selected != null);
        switch (dataspell.Target)
        {
            case TargetType.SINGLE:
                if ((Utils.getTargeter().Selected.name.Equals("PlayerImage")))
                {
                    if (dataspell.Type == SpellType.DAMAGE)
                    {
                        PlayerController.Damage(dataspell.Value);
                        Destroy(obj);
                        Utils.getTargeter().setSelectionToNull();
                        PlayerController.RemoveMana(c.CosteMana);
                    }
                    else if (dataspell.Type == SpellType.HEAL)
                    {
                        PlayerController.Heal(dataspell.Value);
                        Destroy(obj);
                        Utils.getTargeter().setSelectionToNull();
                        PlayerController.RemoveMana(c.CosteMana);
                    }
                }
                else
                {
                    targetMinion(dataspell, Utils.getTargeter().Selected, "TableBot");
                    Destroy(obj);
                    Utils.getTargeter().setSelectionToNull();
                    PlayerController.RemoveMana(c.CosteMana);
                }
                break;
            case TargetType.SINGLEALLY:
                try
                {
                    if (targetMinion(dataspell, Utils.getTargeter().Selected, "TableBot"))
                    {
                        Utils.getTargeter().target = false;
                        Utils.getTargeter().Selected = null;
                        Destroy(obj);
                        Utils.getTargeter().setSelectionToNull();
                        PlayerController.RemoveMana(c.CosteMana);
                    }
                }
                catch (Exception ex)
                {
                    Utils.GetLogger().ShowMessage("No es un objetivo válido", 2, Color.red);
                }
                break;
            case TargetType.SINGLEENEMY:
                try
                {
                    if (targetMinion(dataspell, Utils.getTargeter().Selected, "TableTop"))
                    {
                        Utils.getTargeter().target = false;
                        Utils.getTargeter().Selected = null;
                        Destroy(obj);
                        Utils.getTargeter().setSelectionToNull();
                        PlayerController.RemoveMana(c.CosteMana);
                    }
                }
                catch (Exception ex)
                {
                    Utils.GetLogger().ShowMessage("No es un objetivo válido", 2, Color.red);
                }
                break;
        }
    }
    private void PlaySpell(DataCard c, GameObject obj)
    {
        DataSpell dataspell = CardActionSet.GetSpellProperty(c.ID);
        StopAllCoroutines();
        switch (dataspell.Target)
        {
            case TargetType.SINGLE:
                
                StartCoroutine(spell(dataspell, obj, c));
                break;
            case TargetType.SINGLEALLY:
                
                StartCoroutine(spell(dataspell, obj, c));
                break;
            case TargetType.SINGLEENEMY:
               
                StartCoroutine(spell(dataspell, obj, c));
                return;
            case TargetType.ALL:
                //Player
                if (dataspell.Type == SpellType.DAMAGE)
                {
                    PlayerController.Damage(dataspell.Value);
                    return;
                }
                else if (dataspell.Type == SpellType.HEAL)
                {
                    PlayerController.Heal(dataspell.Value);
                    return;
                }
                //Rival

                //Mesa
                foreach (DataMinion carta in mesa)
                {
                    GameObject MinionObject = GameObject.Find(carta.Nombre);
                    if (MinionObject != null)
                    {
                        targetMinion(dataspell, MinionObject, "TableBot");
                    }
                }
                //Mesa Enemiga
                foreach (DataMinion carta in mesaEnemiga)
                {
                    GameObject MinionObject = GameObject.Find(carta.Nombre);
                    if (MinionObject != null)
                    {
                        targetMinion(dataspell, MinionObject, "TableTop");
                    }
                }
                Destroy(obj);
                Utils.getTargeter().setSelectionToNull();
                PlayerController.RemoveMana(c.CosteMana);
                return;
            case TargetType.ALLALLY:
                foreach (DataMinion carta in mesa)
                {
                    GameObject MinionObject = GameObject.Find(carta.Nombre);
                    if (MinionObject != null)
                    {
                        targetMinion(dataspell, MinionObject, "TableBot");
                    }
                }
                Destroy(obj);
                Utils.getTargeter().setSelectionToNull();
                PlayerController.RemoveMana(c.CosteMana);
                return;
            case TargetType.ALLENEMY:
                foreach (DataMinion carta in mesaEnemiga)
                {
                    GameObject MinionObject = GameObject.Find(carta.Nombre);
                    if (MinionObject != null)
                    {
                        targetMinion(dataspell, MinionObject, "TableTop");
                    }
                }
                Destroy(obj);
                Utils.getTargeter().setSelectionToNull();
                PlayerController.RemoveMana(c.CosteMana);
                return;
        }
    }

    private static bool targetMinion(DataSpell dataspell, GameObject Minionobject, string board)
    {
        bool dead = false;
        ArrayList removedCards = new ArrayList();
        GameObject MinionObject = Minionobject;
        if (MinionObject.transform.parent.name.Equals(board))
        {
            DataMinion minion = MinionObject.GetComponent<LoadCardValues>().getMinion();
            if (dataspell.Type == SpellType.DAMAGE)
            {
                MinionController.Damage(minion, dataspell.Value, board);
                if (MinionController.isDead(minion))
                {
                    removedCards.Add(MinionObject.GetComponent<LoadCardValues>().getMinion());
                    Destroy(MinionObject);
                    dead = true;
                }
            }
            else if (dataspell.Type == SpellType.HEAL)
            {
                MinionController.Heal(minion, dataspell.Value, board);
            }
            if (dead)
            {
                foreach (DataMinion removedCard in removedCards)
                {
                    GameObject.Find("CardController").GetComponent<CardController>().RemoveTableCard(removedCard);
                }
                removedCards.Clear();
            }
        }
        else
        {
            Utils.GetLogger().ShowMessage("No es un objetivo válido", 2, Color.red);
            return false;
        }
        return true;
    }

    private void PlayWeapon(DataCard c,GameObject g)
    {
        PlayerController.RemoveMana(c.CosteMana);
        PlayerController.EquipWeapon(c);
        Destroy(g);
    }
}