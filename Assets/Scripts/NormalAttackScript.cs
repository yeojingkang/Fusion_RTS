using UnityEngine;
using System.Collections;

public class NormalAttackScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * 5;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
