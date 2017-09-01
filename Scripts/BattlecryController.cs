using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlecryController : MonoBehaviour {

	public void Battlecry(DataCard c, GameObject obj)
    {
        switch (c.ID)
        {
            case 7:
                PlayerController.Heal(3);
                break;
            case 8:
                PlayerController.Damage(3);
                break;
            case 9:
                StopAllCoroutines();
                Utils.getTargeter().setStartPosition(Input.mousePosition);
                Utils.getTargeter().target = true;
                Utils.getTargeter().targeting = true;
                StartCoroutine(target(DataCard.toDataMinion(c), obj));
                break;
        }
    }

    public IEnumerator target(DataMinion minion, GameObject obj)
    {
        yield return new WaitUntil(() => Utils.getTargeter().Selected != null);
        GameObject minionselectedObject = Utils.getTargeter().Selected;
        DataMinion minionselected = minionselectedObject.GetComponent<LoadCardValues>().getMinion();
        switch (minion.ID)
        {
            case 9:
                if (minionselectedObject.transform.parent.name.Equals("TableBot"))
                {
                    MinionController.AddHealth(minionselected, 4, minionselectedObject.transform.parent.name);
                }
                break;

        }
        Utils.getTargeter().setSelectionToNull();
    }
}
