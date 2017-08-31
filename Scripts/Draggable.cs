using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnto = null;
    public Transform placeholderParent = null;
    public GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        DataCard card = ((LoadCardValues)Utils.Search(eventData.pointerDrag, "LoadCardValues")).getCarta();
        if (card.Type == CardType.HECHIZO)
        {
            return;
        }
        placeholder = new GameObject();
        placeholder.name = "PlaceHolder";
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleHeight = 0;
        le.flexibleWidth = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnto = this.transform.parent;
        placeholderParent = parentToReturnto;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        DataCard card = ((LoadCardValues)Utils.Search(eventData.pointerDrag, "LoadCardValues")).getCarta();
        if(card.Type == CardType.HECHIZO)
        {
            return;
        }
        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }

        int newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.x < placeholderParent.GetChild(i).transform.position.x)
            {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DataCard card = ((LoadCardValues)Utils.Search(eventData.pointerDrag, "LoadCardValues")).getCarta();
        if (card.Type == CardType.HECHIZO)
        {
            return;
        }
        this.transform.SetParent(parentToReturnto);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeholder);
    }
}
