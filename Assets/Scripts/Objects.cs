using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Object : MonoBehaviour {
	public enum ObjectType {
		OBJECT_TYPE_NONE = 0,
		OBJECT_TYPE_UNIT,
		OBJECT_TYPE_BUILDING
	}

	public enum ObjectCommands {
		COMMANDS_NONE = 0,

		UNIT_COMMANDS_START,
			UNIT_MOVE,
			UNIT_ATTACK_MOVE,
			UNIT_ATTACK,
			UNIT_PATROL,
			UNIT_HOLD_POSITION,
		UNIT_COMMANDS_END,

		BUILDING_COMMANDS_START,
			
		BUILDING_COMMANDS_END
	}

	protected ObjectType			type = ObjectType.OBJECT_TYPE_NONE;

	protected Vector3				direction = new Vector3(0.0f, 0.0f, 1.0f);
	protected Vector3				target_position = new Vector3(0.0f, 0.0f, 0.0f);

	protected List<ObjectCommands>	command_queue = new List<ObjectCommands>();
	protected ObjectCommands		current_command = ObjectCommands.COMMANDS_NONE;

	// Use this for initialization
	protected void Start () {
	}

	// Update is called once per frame
	protected void Update () {
		direction = gameObject.transform.forward;
	}

	protected void	AddCommand(ObjectCommands newCommand) { command_queue.Add(newCommand); }
	protected void	GetNextCommand() {
		if(command_queue.Count > 0) {
			current_command = command_queue[0];
			command_queue.RemoveAt(0);
		}
		else {
			current_command = ObjectCommands.COMMANDS_NONE;
		}

	}
	protected void	ClearCommandQueue() { command_queue.Clear(); }
}
