using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ShowCard(eventData));
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Transform t in GameObject.Find("CardInfo").GetComponentInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }
    }

    public IEnumerator ShowCard(PointerEventData eventData)
    {
        GameObject Object = this.gameObject;
        if (Object != null)
        {
            DataCard carta = Object.GetComponent<LoadCardValues>().getCarta();
            Utils.GetLoader().LoadValue("CardInfo", carta);
            GameObject moveme = Utils.SearchNotthis(GameObject.Find("CardInfo").transform, Object.name);
        }
        yield return new WaitForSecondsRealtime(1);
        yield return new WaitUntil(() => eventData.IsPointerMoving());
        foreach (Transform t in GameObject.Find("CardInfo").GetComponentInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }
    }
}
