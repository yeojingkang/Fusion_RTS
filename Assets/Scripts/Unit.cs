using UnityEngine;
using System.Collections;

public class Unit : Objects {
	float	move_speed = 10.0f;
	NavMeshAgent	agent = null;

	// Use this for initialization
	new void	Start () {
		type = ObjectType.OBJECT_TYPE_UNIT;

		agent = GetComponent<NavMeshAgent>();
		agent.speed = move_speed;
		agent.acceleration = 9999.0f;
		agent.stoppingDistance = 2.5f;
	}

	// Update is called once per frame
	new void	Update () {
		//if (!isLocalPlayer)
		//	return;

		base.Update();
		DoCurrentCommand();
	}

	void	DoCurrentCommand() {
		if (current_command.GetCommandType() == ObjectCommands.Commands.COMMANDS_NONE
		 && command_queue.Count <= 0)
			return;

		//Get next command and proceed to next loop if current command is not valid
		if(!(current_command.GetCommandType() > ObjectCommands.Commands.UNIT_COMMANDS_START
		  && current_command.GetCommandType() < ObjectCommands.Commands.UNIT_COMMANDS_END)) {
			GetNextCommand();
			return;
		}

		switch(current_command.GetCommandType()) {
		case ObjectCommands.Commands.UNIT_MOVE:
			DoMove();
			break;
		case ObjectCommands.Commands.UNIT_ATTACK_MOVE:
			DoAttackMove();
			break;
		case ObjectCommands.Commands.UNIT_ATTACK:
			break;
		case ObjectCommands.Commands.UNIT_PATROL:
			break;
		case ObjectCommands.Commands.UNIT_HOLD_POSITION:
			break;
		}
	}

	void	DoMove() {
		if(!current_command.startExecute) {
			agent.SetDestination(current_command.GetTargetPos());
			current_command.startExecute = true;
		}
		else {
			if (agent.remainingDistance <= agent.stoppingDistance)
				//Agent reached target position
				GetNextCommand();
		}
	}
	void	DoAttackMove() {
		if (!current_command.startExecute) {
			agent.SetDestination(current_command.GetTargetPos());
			current_command.startExecute = true;
		}
		else {
			//Check for nearby enemy
			//If found, set targetObject
			if (agent.remainingDistance <= agent.stoppingDistance
			 && current_command.GetTargetObject() == null)
				GetNextCommand();
		}
	}
}