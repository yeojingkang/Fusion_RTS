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
			UNIT_MOVE_CAST_SPELL,
		UNIT_COMMANDS_END,

		BUILDING_COMMANDS_START,

		BUILDING_COMMANDS_END
	}

	Vector3		targetPos;
	Object		targetObject;
	int			activatedCommand;

	Commands	type = Commands.COMMANDS_NONE;

	public bool startExecute = false;

	public ObjectCommands() {
		type = Commands.COMMANDS_NONE;
		targetPos = Vector3.zero;
		targetObject = null;
		activatedCommand = -1;
		startExecute = false;
	}
	public ObjectCommands(Commands newType, Vector3 newTargetPos = default(Vector3), Object newTargetObject = null, int newActivatedCommand = -1) {
		SetCommand(newType, newTargetPos, newTargetObject, newActivatedCommand);
		startExecute = false;
	}

	public Commands GetCommandType() { return type; }
	public Vector3	GetTargetPos() { return targetPos; }
	public Object	GetTargetObject() { return targetObject; }
	public int		GetActivatedCommand() { return activatedCommand; }

	public void	SetCommand(Commands newType, Vector3 newTargetPos = default(Vector3), Object newTargetObject = null, int newActivatedCommand = -1) {
		type = newType;
		targetPos = newTargetPos;
		targetObject = newTargetObject;
		activatedCommand = newActivatedCommand;
	}
	public void	Reset() {
		type = Commands.COMMANDS_NONE;
		targetObject = null;
		activatedCommand = -1;
		startExecute = false;
	}
}
