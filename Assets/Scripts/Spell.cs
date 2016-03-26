using UnityEngine;
using System.Collections;

public class Spell: MonoBehaviour {
	public enum SpellType {
		SPELL_NONE = 0,
		SPELL_NORMAL_ATTACK = 1,
		SPELL_ACTIVE_OFFENCE = 2,
		SPELL_ACTIVE_DEFENSE = 3,
		SPELL_PASSIVE = 4
	}

	GameObject spell = null;
    Unit owner = null;

	public SpellType	type = SpellType.SPELL_NONE;

    public float ori_cooldown = 0.0f;
	float		cooldown = 0.0f;
    public float cast_range = 0.0f;
    public bool mustBeTargeted = false;

    public int level = 0;
    public int max_level = 3;

    public Spell(Unit newOwner)
    {
        owner = newOwner;
    }

	public void Update () {
		if (type != SpellType.SPELL_PASSIVE)
		{
			if (cooldown > 0.0f)
				cooldown -= Time.deltaTime;

			if (cooldown < 0.0f)
				cooldown = 0.0f;
		}
	}

	public void Init(SpellType newType) {
		type = newType;
		cooldown = 0.0f;

		switch(type) {
		case SpellType.SPELL_NONE:
			break;

		case SpellType.SPELL_NORMAL_ATTACK:
			cast_range = 5.0f;
			ori_cooldown = 3.0f;
			mustBeTargeted = true;
			spell = (GameObject)Resources.Load("Spells/Normal_Attack");
			break;
		}
	}
    public void Cast(Vector3 position, Vector3 forward = default(Vector3), Quaternion rotation = default(Quaternion)) 
    {

    }
    //public bool Cast(Vector3 position, Vector3 forward = default(Vector3), Quaternion rotation = default(Quaternion))
    //{
    //    if (cooldown > 0.0f)
    //        return false;

    //    switch (type)
    //    {
    //        case SpellType.SPELL_NONE:
    //            break;

    //        case SpellType.SPELL_NORMAL_ATTACK:
    //            GameObject.Instantiate(spell, position + forward * 1.5f, rotation);
    //            GameObject proj = GameObject.Instantiate(spell, position + forward *1.5f, rotation) as GameObject;
    //            proj.GetComponent<NormalAttackScript>().SetOwner(owner);
    //            break;
    //    }

    //    cooldown = ori_cooldown;
    //    return true;
    //}

	public float	getCooldown() { return cooldown; }
	public bool		isReady() { return cooldown <= 0.0f; }
	public bool		isTargeted() { return mustBeTargeted; }
	public float	getCastRange() { return cast_range; }
}
