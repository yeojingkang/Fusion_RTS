using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour {
	public enum ObjectType {
		OBJECT_TYPE_NONE = 0,
		OBJECT_TYPE_UNIT,
		OBJECT_TYPE_BUILDING
	}

	protected ObjectType	type = ObjectType.OBJECT_TYPE_NONE;

	protected Vector3		direction = new Vector3(0.0f, 0.0f, 1.0f);
	protected Vector3		target_position = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
		direction = gameObject.transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
