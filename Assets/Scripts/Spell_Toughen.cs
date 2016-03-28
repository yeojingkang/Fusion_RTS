using UnityEngine;
using System.Collections;

public class Spell_Toughen :MonoBehaviour {

    //this spell increase the pushing force for the unit
    Unit owner = null;

    int spell_level = 1;

    void Start()
    {
        //increase pushing force depending on spell level
        switch(spell_level)
        {
            case 1:
                owner.pushing_force = 1.1f;
                break;

            case 2:
                owner.pushing_force = 1.2f;
                break;
            case 3:
                owner.pushing_force = 1.3f;
                break;

            default:
                owner.pushing_force = 1.0f;
                break;

        }

    }

    public void SetOwner(Unit newOwner) { owner = newOwner; }
}
