using UnityEngine;
using System.Collections;

public class NormalAttackScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * 15;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col) {
		Unit colUnit = col.gameObject.GetComponent<Unit>();
		if (colUnit != null) {
			colUnit.Damage(7.0f);
		}

		Destroy(gameObject);
	}
}
