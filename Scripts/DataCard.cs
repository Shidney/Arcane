using System;
using UnityEngine.UI;

[Serializable]
public class DataCard
{
    public int ID;
    public string Nombre;
    public string Descripcion;
    public string PathImage;
    public CardRarity Rarity;
    public CardType Type;
    public CardRaza Raza;

    public int CosteMana;
    public int Ataque;
    public int Vida;

    public DataCard(int id, string nombre, string descripcion, string pathImage, CardRarity rarity, CardType type, CardRaza raza, int costeMana, int ataque, int vida)
    {
        ID = id;
        Nombre = nombre;
        Descripcion = descripcion;
        PathImage = pathImage;
        Rarity = rarity;
        Type = type;
        Raza = raza;
        CosteMana = costeMana;
        Ataque = ataque;
        Vida = vida;
    }
    public static DataMinion toDataMinion(DataCard c)
    {
        if (c != null)
        {
            DataMinion dm = new DataMinion(c.ID, c.Nombre, c.Descripcion, c.PathImage, c.Ataque, c.Vida);
            if (CardActionSet.GetMinionProperty(c.ID) != null)
            {
                dm.Properties = CardActionSet.GetMinionProperty(c.ID);
            }
            return dm;
        }
        else
        {
            return null;
        }
    }

    public static DataWeapon toDataWeapon(DataCard c)
    {
        if (c != null)
        {
            DataWeapon dw = new DataWeapon(c.ID, c.Nombre,c.PathImage, c.Ataque, c.Vida);
            if (CardActionSet.GetMinionProperty(c.ID) != null)
            {
                dw.Properties = CardActionSet.GetWeaponProperty(c.ID);
            }
            return dw;
        }
        else
        {
            return null;
        }
    }
}

