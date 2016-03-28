using UnityEngine;
using System.Collections;

public class NormalAttackScript : MonoBehaviour {
	Unit	owner = null;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * 22;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col) {
		Unit colUnit = col.gameObject.GetComponent<Unit>();
		if (colUnit != null) {
			colUnit.Damage(7.0f);

			if(colUnit.isDead()) {
				if(owner != null) {
					//Owner of projectile gets credit
					owner.addGold(5);
				}
			}
		}

		Destroy(gameObject);
	}

	public void SetOwner(Unit newOwner) { owner = newOwner; }
}
