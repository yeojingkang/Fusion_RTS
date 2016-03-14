using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
	public enum SpellType {
		SPELL_NONE = 0,
		SPELL_NORMAL_ATTACK
	}

	SpellType	type = SpellType.SPELL_NONE;
	float		cooldown = 0.0f;
	bool		mustBeTargeted = false;

	// Use this for initialization
	void Start () {
		type = SpellType.SPELL_NONE;
		cooldown = 0.0f;
		mustBeTargeted = false;
	}

	// Update is called once per frame
	void Update () {
		if (cooldown > 0.0f)
			cooldown -= Time.deltaTime;
	}

	public bool Cast() {
		if (cooldown > 0.0f)
			return false;

		switch(type) {
		case SpellType.SPELL_NONE:
			break;

		case SpellType.SPELL_NORMAL_ATTACK:
			break;
		}

		return true;
	}

	public float	getCooldown() { return cooldown; }
	public bool		isTargeted() { return mustBeTargeted; }
}
