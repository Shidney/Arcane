using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Database : MonoBehaviour
{
    private string SaveFile;
    public List<DataCard> DataBaseCards = new List<DataCard>();
    public static List<DataCard> cartastotales = new List<DataCard>();

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SaveFile = Application.dataPath + "/Resources/Files/Cards.json";
        saveCardFile();
        loadCardFile();
        GameObject.Find("CardController").GetComponent<CardController>().deck = cartastotales;
        DataBaseCards = cartastotales;
    }

    private void saveCardFile()
    {
        //Generar todas las cartas
        DataCard carta = new DataCard(0, "Combustión", "Inflinge <b>4p.</b> de daño a un esbirro", "Images/Fireball", CardRarity.COMUN, CardType.HECHIZO, CardRaza.ARCANISTA, 6, 0, 0);
        DataCard carta2 = new DataCard(1, "Alma de peste", "1", "Images/PestSoul", CardRarity.RARA, CardType.ESBIRRO, CardRaza.NIGROMANTE, 1, 1, 7);
        DataCard carta3 = new DataCard(2, "Alma de muerte", "<b>Provocar</b>", "Images/DeathSoul", CardRarity.RARA, CardType.ESBIRRO, CardRaza.NIGROMANTE, 2, 2, 7);
        DataCard carta4 = new DataCard(3, "Alma de guerra", "<b>Escudo Divino</b>", "Images/WarSoul", CardRarity.RARA, CardType.ESBIRRO, CardRaza.NIGROMANTE, 3, 3, 7);
        DataCard carta5 = new DataCard(4, "Alma de hambre", "<b>Cargar</b>", "Images/HungerSoul", CardRarity.RARA, CardType.ESBIRRO, CardRaza.NIGROMANTE, 4, 4, 7);
        DataCard carta6 = new DataCard(5, "Sanación", "Restaura <b>4p.</b> de salud a tus esbirros", "Images/HealingTouch", CardRarity.COMUN, CardType.HECHIZO, CardRaza.HADA, 6, 0, 0);
        DataCard carta7 = new DataCard(6, "Daño heroico", "Inflinge <b>4p.</b> de daño", "Images/Arcanista", CardRarity.LEGENDARIA, CardType.HECHIZO, CardRaza.NIGROMANTE, 1, 0, 0);
        DataCard carta8 = new DataCard(7, "Clériga experta", "<b>Grito de Batalla:</b>Restaura <b>3p.</b> de salud a tu héroe", "Images/Arcanista", CardRarity.EPICA, CardType.ESBIRRO, CardRaza.HADA, 2, 2, 3);
        DataCard carta9 = new DataCard(8, "Demonio Aterrador", "<b>Grito de Batalla:</b>Inflinge <b>3p.</b> de daño a tu héroe", "Images/Skeleton", CardRarity.RARA, CardType.ESBIRRO, CardRaza.NIGROMANTE, 3, 4, 6);
        DataCard carta10 = new DataCard(9, "Emperador", "<b>Grito de Batalla:</b>Otorga <b>4p.</b> de salud a un esbirro aliado", "Images/Arcanista", CardRarity.RARA, CardType.ESBIRRO, CardRaza.ELEMENTALISTA, 3, 4, 6);
        DataCard carta11 = new DataCard(10, "Hacha afilada", "", "Images/WarAxe", CardRarity.RARA, CardType.ARMA, CardRaza.ELEMENTALISTA, 3, 4, 4);

        //Generar Sets Hechizos
        CardActionSet.SetSpellProperty(0, new DataSpell(4, TargetType.SINGLEENEMY, SpellType.DAMAGE));
        //
        CardActionSet.SetSpellProperty(5, new DataSpell(4, TargetType.ALLALLY, SpellType.HEAL));
        //
        CardActionSet.SetSpellProperty(6, new DataSpell(4, TargetType.SINGLE, SpellType.DAMAGE));

        //Guardar en array
        DataCard[] array = new DataCard[14];//modificar este numero por el numero de cartas
        array[0] = carta;//añadir todas las cartas creadas al array
        array[1] = carta2;//añadir todas las cartas creadas al array
        array[2] = carta3;//añadir todas las cartas creadas al array
        array[3] = carta4;//añadir todas las cartas creadas al array
        array[4] = carta5;//añadir todas las cartas creadas al array
        array[5] = carta6;//añadir todas las cartas creadas al array
        array[6] = carta;
        array[7] = carta;
        array[8] = carta;
        array[9] = carta7;
        array[10] = carta8;
        array[11] = carta9;
        array[12] = carta10;
        array[13] = carta11;

        foreach (var v in array)
        {
            if(v.Type==CardType.ESBIRRO)
            {
                Dictionary<GameTag, bool> proper = new Dictionary<GameTag, bool>();
                proper.Add(GameTag.FATIGUED, true);
                CardActionSet.SetMinionProperty(v.ID, proper);
            }
        }
        Dictionary<GameTag, bool> prop = new Dictionary<GameTag, bool>();
        prop = CardActionSet.GetMinionProperty(2);
        prop.Add(GameTag.TAUNT, true);
        CardActionSet.SetMinionProperty(2, prop);
        //
        prop = CardActionSet.GetMinionProperty(3);
        prop.Add(GameTag.DIVINESHIELD, true);
        CardActionSet.SetMinionProperty(3, prop);
        //
        prop = CardActionSet.GetMinionProperty(4);
        prop.Add(GameTag.CHARGE, true);
        prop[GameTag.FATIGUED] = false;
        CardActionSet.SetMinionProperty(4, prop);
        //
        prop = CardActionSet.GetMinionProperty(7);
        prop.Add(GameTag.BATTLECRY, true);
        CardActionSet.SetMinionProperty(7, prop);
        //
        prop = CardActionSet.GetMinionProperty(8);
        prop.Add(GameTag.BATTLECRY, true);
        CardActionSet.SetMinionProperty(8, prop);
        // 
        prop = CardActionSet.GetMinionProperty(9);
        prop.Add(GameTag.BATTLECRY, true);
        CardActionSet.SetMinionProperty(9, prop);
        if (File.Exists(SaveFile))
        {
            File.Delete(SaveFile);
        }
        string json = JsonHelper.ToJson<DataCard>(array);
        File.AppendAllText(SaveFile, json);
    }

    private void loadCardFile()
    {
        SaveFile = Application.dataPath + "/Resources/Files/Cards.json";
        DataCard[] array = JsonHelper.FromJson<DataCard>(File.ReadAllText(SaveFile));
        foreach (DataCard d in array)
        {
            cartastotales.Add(d);
        }
    }
}

