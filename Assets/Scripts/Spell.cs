using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
	public enum SpellType {
		SPELL_NONE = 0,
		SPELL_NORMAL_ATTACK
	}

	SpellType	type = SpellType.SPELL_NONE;

	// Use this for initialization
	void Start () {
		type = SpellType.SPELL_NONE;
	}

	// Update is called once per frame
	void Update () {

	}
}
