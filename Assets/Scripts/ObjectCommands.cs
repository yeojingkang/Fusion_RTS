using UnityEngine;
using System.Collections;

public class ObjectCommands {
	public enum Commands {
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

	Vector3		targetPos;
	Object		targetObject;

	Commands	type = Commands.COMMANDS_NONE;

	public bool startExecute = false;

	public ObjectCommands() {
		type = Commands.COMMANDS_NONE;
		targetPos = Vector3.zero;
		targetObject = null;
		startExecute = false;
	}
	public ObjectCommands(Commands newType, Vector3 newTargetPos = default(Vector3), Object newTargetObject = null) {
		SetCommand(newType, newTargetPos, newTargetObject);
		startExecute = false;
	}

	public Commands GetCommandType() { return type; }
	public Vector3	GetTargetPos() { return targetPos; }
	public Object	GetTargetObject() { return targetObject; }

	public void	SetCommand(Commands newType, Vector3 newTargetPos = default(Vector3), Object newTargetObject = null) {
		type = newType;
		targetPos = newTargetPos;
		targetObject = newTargetObject;
	}
	public void	Reset() {
		type = Commands.COMMANDS_NONE;
		targetObject = null;
		startExecute = false;
	}
}
