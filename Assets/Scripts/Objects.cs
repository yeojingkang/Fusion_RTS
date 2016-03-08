using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Objects : NetworkBehaviour {
	public enum ObjectType {
		OBJECT_TYPE_NONE = 0,
		OBJECT_TYPE_UNIT,
		OBJECT_TYPE_BUILDING
	}

	protected ObjectType			type = ObjectType.OBJECT_TYPE_NONE;

	protected Vector3				direction = new Vector3(0.0f, 0.0f, 1.0f);
	protected Vector3				target_position = new Vector3(0.0f, 0.0f, 0.0f);

	protected List<ObjectCommands>	command_queue = new List<ObjectCommands>();
	protected ObjectCommands		current_command = new ObjectCommands();

	// Use this for initialization
	protected void Start () {
	}

	// Update is called once per frame
	protected void Update () {
		//if (!isLocalPlayer)
		//	return;

		direction = gameObject.transform.forward;
	}

	protected void	AddCommand(ObjectCommands newCommand) { command_queue.Add(newCommand); }
	protected void	AddCommand(ObjectCommands.Commands type, Vector3 targetPos = default(Vector3), Object targetObject = null) {
		command_queue.Add(new ObjectCommands(type, targetPos, targetObject));
	}
	protected void	AddCommandInIndex(int index, ObjectCommands newCommand) { command_queue.Insert(index, newCommand); }
	protected void	AddCommandInIndex(int index, ObjectCommands.Commands type, Vector3 targetPos = default(Vector3), Object targetObject = null) {
		command_queue.Insert(index, new ObjectCommands(type, targetPos, targetObject));
	}

	protected void	GetNextCommand() {
		if(command_queue.Count > 0) {
			current_command = command_queue[0];
			command_queue.RemoveAt(0);
		}
		else {
			current_command.Reset();
		}

	}
	protected void	ClearCommandQueue() { command_queue.Clear(); }
	protected void	StopAllCommands() {
		command_queue.Clear();
		current_command.Reset();
	}
}
