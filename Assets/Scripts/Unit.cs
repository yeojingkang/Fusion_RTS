using UnityEngine;
using System.Collections;

public class Unit : Objects {
	float			move_speed = 10.0f;
	NavMeshAgent	agent = null;

	public Spell[]	spells = new Spell[4];

	float			hp = 0;
	float			max_hp = 100;

	int				gold = 0;

	bool			dead = false;

	Vector3			spawn_position = Vector3.zero;
	Vector3			spawn_forward = new Vector3(0,0,1);
	float			curr_respawn_timer = 0.0f;
	float			ori_respawn_timer = 5.0f;

	// Use this for initialization
	new void	Start () {
		type = ObjectType.OBJECT_TYPE_UNIT;

		for (int i = 0; i < spells.Length; ++i)
			spells[i] = new Spell(this);

		//Temp. code (can be permanent if wanted)
		spells[0].Init(Spell.SpellType.SPELL_NORMAL_ATTACK);

		//Init navMeshAgent params
		agent = GetComponent<NavMeshAgent>();
		agent.speed = move_speed;
		agent.acceleration = 9999.0f;
		agent.stoppingDistance = 0.5f;
		agent.updateRotation = true;

		Respawn();
	}

	// Update is called once per frame
	new void	Update () {
		//if (!isLocalPlayer)
		//	return;

		if(dead) {
			UpdateRespawnTimer();
		}
		else {
			base.Update();
			DoCurrentCommand();
		}

		//Update spell cooldowns even while unit is dead
		foreach (Spell spell in spells)
			spell.Update();
	}

	void	UpdateRespawnTimer() {
		curr_respawn_timer -= Time.deltaTime;

		if(curr_respawn_timer <= 0.0f) {
			Respawn();
		}
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

	void	Die() {
		dead = true;
		curr_respawn_timer = ori_respawn_timer;

		agent.Stop();
		StopAllCommands();

		SetComponentsActive(false);
	}
	void	Respawn() {
		dead = false;
		hp = max_hp;
		transform.position = spawn_position;
		transform.forward = spawn_forward;

		SetComponentsActive(true);

		agent.Stop();
		StopAllCommands();
	}
	//Sets all components except this script in the gameobject active/inactive
	void	SetComponentsActive(bool active) {
		MonoBehaviour[] comps = GetComponents<MonoBehaviour>();

		foreach (MonoBehaviour comp in comps) {
			if (comp != this)
				comp.enabled = active;
		}

		GetComponent<Renderer>().enabled = active;
		GetComponent<Collider>().enabled = active;
		agent.enabled = active;
	}

	//Public functions
	/////////////////////////////////////////////////////////////////////////////////////////

	public void	Damage(float dmg) {
		hp -= dmg;

		if (hp <= 0) {
			Die();
		}
	}

	public void	addGold(int amt) { gold += amt; }
	public void	reduceGold(int amt) {
		gold -= amt;
		if (gold < 0)
			gold = 0;
	}

	public void	setSpawnPosition(Vector3 newSpawnPos) { spawn_position = newSpawnPos; }
	public bool	isDead() { return dead; }

	public void	changeSpell(int index, Spell.SpellType newType) {
		if(index > -1 && index < spells.Length)
			spells[index].Init(newType);
	}
}