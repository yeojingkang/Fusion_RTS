using UnityEngine;
using System.Collections;

public class Unit : Objects {
	float			move_speed = 10.0f;
	NavMeshAgent	agent = null;

	public Spell[]	spells = new Spell[4];

	// Use this for initialization
	new void	Start () {
		type = ObjectType.OBJECT_TYPE_UNIT;

		for (int i = 0; i < spells.Length; ++i)
			//spells[i] = new Spell();
			spells[i] = gameObject.AddComponent<Spell>();

		spells[0].Init(Spell.SpellType.SPELL_NORMAL_ATTACK);

		agent = GetComponent<NavMeshAgent>();
		agent.speed = move_speed;
		agent.acceleration = 9999.0f;
		agent.stoppingDistance = 0.5f;
		agent.updateRotation = true;
		agent.updatePosition = true;
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
		 && command_queue.Count <= 0) {
			return;
		}

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
			DoAttack();
			break;
		case ObjectCommands.Commands.UNIT_PATROL:
			break;
		case ObjectCommands.Commands.UNIT_HOLD_POSITION:
			break;
		case ObjectCommands.Commands.UNIT_MOVE_CAST_SPELL:
			DoMoveCastSpell();
			break;
		}
	}

	void	DoMove() {
		if(!current_command.startExecute) {
			agent.SetDestination(current_command.GetTargetPos());
			agent.Resume();
			current_command.startExecute = true;
		}
		else {
			if (agent.remainingDistance <= agent.stoppingDistance) {
				//Agent reached target position
				agent.updateRotation = true;
				GetNextCommand();
			}
		}
	}
	void	DoAttackMove() {
		if (!current_command.startExecute) {
			agent.SetDestination(current_command.GetTargetPos());
			agent.Resume();
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
	void	DoAttack() {
	}
	void	DoMoveCastSpell() {
		if (!current_command.startExecute) {
			agent.SetDestination(current_command.GetTargetPos());
			agent.Resume();
			current_command.startExecute = true;
		}
		else {
			if (agent.remainingDistance <= spells[current_command.GetActivatedCommand()].getCastRange()
			 && Vector3.Angle(transform.forward, current_command.GetTargetPos() - transform.position) < 10.0f) {
				spells[current_command.GetActivatedCommand()].Cast(transform.position, transform.forward, transform.rotation);
				agent.Stop();
				GetNextCommand();
			}
			else if (agent.remainingDistance <= agent.stoppingDistance) {
				//Agent reached target position
				agent.updateRotation = true;
				GetNextCommand();
			}
		}
	}
}