using UnityEngine;
using System.Collections;

public class Unit : Object {
	float	move_speed = 1.0f;

	//User control stuff
	//bool    selectable = false;
	//bool    selected = false;

	// Use this for initialization
	new void	Start () {
		type = ObjectType.OBJECT_TYPE_UNIT;

		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.speed = move_speed;
		agent.SetDestination(new Vector3(50, 0, 77));
	}

	// Update is called once per frame
	new void	Update () {
		if (!isLocalPlayer)
			return;

		base.Update();
		DoCurrentCommand();
	}

	void	DoCurrentCommand() {
		if (current_command == ObjectCommands.COMMANDS_NONE)
			return;

		//Get next command and proceed to next loop if current command is not valid
		if(!(current_command > ObjectCommands.UNIT_COMMANDS_START
		  && current_command < ObjectCommands.UNIT_COMMANDS_END)) {
			GetNextCommand();
			return;
		}

		switch(current_command) {
		case ObjectCommands.UNIT_MOVE:
			break;
		case ObjectCommands.UNIT_ATTACK_MOVE:
			break;
		case ObjectCommands.UNIT_ATTACK:
			break;
		case ObjectCommands.UNIT_PATROL:
			break;
		case ObjectCommands.UNIT_HOLD_POSITION:
			break;
		}
	}
}