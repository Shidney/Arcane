using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{
    static int fatigue=1;
    static System.Random r = new System.Random();

    public static void draw()
    {
        if (Database.cartastotales.Count > 0)
        {
            int seleccion = r.Next(0, GameObject.Find("CardController").GetComponent<CardController>().deck.Count);
            DataCard data = (DataCard)GameObject.Find("CardController").GetComponent<CardController>().deck[seleccion];
            GameObject.Find("CardController").GetComponent<CardController>().deck.RemoveAt(seleccion);

            if (GameObject.Find("CardController").GetComponent<CardController>().AddHandCard(data))
            {                
                Utils.GetLoader().LoadValue("HandPanel", data);
            }
            else
            {
                Utils.GetLogger().ShowMessage("Mi mano esta llena", 2, Color.red);
                Utils.GetLoader().LoadValue("Discarder", data);
                Utils.GetDiscarder().BurnCard(data);               
            }
        }
        else
        {
            Utils.GetLogger().ShowMessage("No me quedan cartas", 2, Color.yellow);
            DataCard data = new DataCard(0, "Fatiga", "Inflinge " + fatigue + " daño", "Images/AtaqueBackground", CardRarity.GRATIS, CardType.HECHIZO, PlayerController.Raza, 0, 0, 0);
            Utils.GetLoader().LoadValue("Discarder", data);
            Utils.GetDiscarder().BurnCard(data);
            PlayerController.Damage(fatigue);
            fatigue++;
        }
    }
}
