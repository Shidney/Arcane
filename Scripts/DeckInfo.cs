using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject deckInfo = GameObject.Find("DeckInfo");
        Color c = deckInfo.GetComponent<Image>().color;
        deckInfo.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1);
        GameObject decktext=Utils.Search(deckInfo.transform, "DeckText");
        decktext.GetComponent<Text>().text = GameObject.Find("CardController").GetComponent<CardController>().deck.Count + " cartas.";

        GameObject handInfo = GameObject.Find("HandInfo");
        Color c2 = handInfo.GetComponent<Image>().color;
        handInfo.GetComponent<Image>().color = new Color(c2.r, c2.g, c2.b, 1);
        GameObject HandText = Utils.Search(handInfo.transform, "HandText");
        HandText.GetComponent<Text>().text = GameObject.Find("CardController").GetComponent<CardController>().mano.Count + " cartas.";

        GameObject tableInfo = GameObject.Find("TableInfo");
        Color c3 = tableInfo.GetComponent<Image>().color;
        tableInfo.GetComponent<Image>().color = new Color(c2.r, c2.g, c2.b, 1);
        GameObject TableText = Utils.Search(tableInfo.transform, "TableText");
        TableText.GetComponent<Text>().text = GameObject.Find("CardController").GetComponent<CardController>().mesa.Count+" esbirros";

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject deckInfo = GameObject.Find("DeckInfo");
        Color c = deckInfo.GetComponent<Image>().color;
        deckInfo.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0);
        GameObject decktext = Utils.Search(deckInfo.transform, "DeckText");
        decktext.GetComponent<Text>().text = "";

        GameObject handInfo = GameObject.Find("HandInfo");
        Color c2 = handInfo.GetComponent<Image>().color;
        handInfo.GetComponent<Image>().color = new Color(c2.r, c2.g, c2.b, 0);
        GameObject HandText = Utils.Search(handInfo.transform, "HandText");
        HandText.GetComponent<Text>().text = "";

        GameObject tableInfo = GameObject.Find("TableInfo");
        Color c3 = tableInfo.GetComponent<Image>().color;
        tableInfo.GetComponent<Image>().color = new Color(c2.r, c2.g, c2.b, 0);
        GameObject TableText = Utils.Search(tableInfo.transform, "TableText");
        TableText.GetComponent<Text>().text = "";
    }
    public void Update()
    {
        GameObject.Find("CardController").GetComponent<CardController>().UpdateValuesCards();
    }
}
