using UnityEngine;
using System.Collections;

public class Spell_Toughen :MonoBehaviour {

    //this spell increase the pushing force for the unit
    Unit owner = null;

    int spell_level = 1;

    void Start()
    {
        //increase pushing force depending on spell level
        switch (spell_level)
        {
            case 1:
                //level 1 pushing force increase by 10%
                owner.pushing_force = 1.1f;
                break;

            case 2:
                //level 2 pushing force increase by 20%
                owner.pushing_force = 1.2f;
                break;
            case 3:
                //level 3 pushing force increase by 30%
                owner.pushing_force = 1.3f;
                break;

            default:
                owner.pushing_force = 1.0f;
                break;

        }

    }

    public void SetOwner(Unit newOwner) { owner = newOwner; }
}
