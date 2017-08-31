using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class HeroAttack : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerController.CanAttack)
        {
            StopAllCoroutines();
            Utils.getTargeter().setStartPosition(Input.mousePosition);
            Utils.getTargeter().target = true;
            Utils.getTargeter().targeting = true;
            StartCoroutine(attackWithHero());
        }
        else
        {
            Utils.GetLogger().ShowMessage("No puedo atacar", 2, Color.red);
        }
    }

    public IEnumerator attackWithHero()
    {
        yield return new WaitUntil(() => Utils.getTargeter().Selected != null);
        if (!Utils.getTargeter().Selected.Equals("PlayerImage"))
        {
            GameObject target = Utils.getTargeter().Selected;
            if (target.transform.parent.name.Equals("TableTop"))
            {
                DataMinion miniontarget = target.GetComponent<LoadCardValues>().getMinion();

                if (!Interactable.IsAnyMinionTaunt())
                {
                    //Hero                  
                    MinionController.Damage(miniontarget, PlayerController.Weapon.Ataque, Utils.getTargeter().Selected.transform.parent.name);
                    PlayerController.Damage(miniontarget.Ataque);
                    Utils.getTargeter().setSelectionToNull();
                    PlayerController.useEquippedWeapon();
                    StopAllCoroutines();
                }
                else
                {
                    if (Interactable.IsAnyMinionTaunt(miniontarget))
                    {
                        //Hero
                        MinionController.Damage(miniontarget, PlayerController.Weapon.Ataque, Utils.getTargeter().Selected.transform.parent.name);
                        PlayerController.Damage(miniontarget.Ataque);
                        Utils.getTargeter().setSelectionToNull();
                        PlayerController.useEquippedWeapon();
                        StopAllCoroutines();
                    }
                    if (Utils.getTargeter().Selected != null)
                    {
                        Utils.getTargeter().setSelectionToNull();
                        Utils.GetLogger().ShowMessage("Se interpone un esbirro con provocar", 2, Color.grey);
                        attackWithHero();
                    }
                }
            }
            else
            {
                Utils.getTargeter().setSelectionToNull();
                Utils.GetLogger().ShowMessage("No puedo hacer eso", 2, Color.red);
                StopAllCoroutines();
            }
        }
        else
        {
            Utils.getTargeter().setSelectionToNull();
            Utils.GetLogger().ShowMessage("No puedo hacer eso", 2, Color.red);
            StopAllCoroutines();
        }
    }
}
