using UnityEngine;
using System.Collections;

public class Building : Object {

	// Use this for initialization
	new void Start () {
		type = ObjectType.OBJECT_TYPE_BUILDING;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
	}
}
