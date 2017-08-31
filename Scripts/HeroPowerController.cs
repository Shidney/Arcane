using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class HeroPowerController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject PlayerCanvas = GameObject.Find("PlayerCanvas");
        GameObject HeroPanel = Utils.Search(PlayerCanvas.transform,"HeroPower");
        GameObject ManaPanel = Utils.Search(HeroPanel.transform, "Mana Panel");
        GameObject ManaCost = Utils.Search(ManaPanel.transform, "Mana Text");
        int cost = int.Parse(ManaCost.GetComponent<Text>().text);
        if (PlayerController.CanPlayByMana(cost))
        {
            GameObject.Find("PlayerCanvas").GetComponent<PlayerController>().useHeroPower(cost);
            ManaPanel.GetComponent<Image>().color = Color.red;
        }
    }
}
