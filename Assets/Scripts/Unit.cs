using UnityEngine;
using System.Collections;

public class Unit : Object {
	float	move_speed = 1.0f;

	//User control stuff
	//bool    selectable = false;
	//bool    selected = false;

	// Use this for initialization
	void	Start () {
		type = ObjectType.OBJECT_TYPE_UNIT;

		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(new Vector3(-6, 0, -10));
	}

	// Update is called once per frame
	void	Update () {
		update_move();
	}

	void	update_move() {

	}
}