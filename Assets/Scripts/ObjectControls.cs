using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectControls : MonoBehaviour {
	new Camera			camera = null;

	public Unit			unit = null;
	public Canvas		canvas = null;
	Rect				selection_box = new Rect(0, 0, 0, 0);
	//bool				dragging = false;
	Vector3				start_mouse_position = new Vector3(0, 0, 0);

	List<Objects>		selected_objects = new List<Objects>();

	int					left_click_command = -1;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		updateLeftClickInputs();
		updateRightClickInputs();
		updateKeyboardInputs();
	}

	void	updateLeftClickInputs() {
		if (Input.GetMouseButtonDown(0))
		{
			//dragging = false;
			//selection_box.min = Input.mousePosition;
			//selection_box.max = Input.mousePosition;
			//start_mouse_position = Input.mousePosition;
			switch(left_click_command) {
			case -1:
				break;
			case 0:
			case 1:
			case 2:
			case 3:
				//Cast spell
				if (unit != null) { //Ignore if unit is somehow null
					Ray ray = camera.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Level terrain"))) {
						if (Input.GetKey(KeyCode.LeftShift)
						 || Input.GetKey(KeyCode.RightShift)) {
							unit.AddCommand(ObjectCommands.Commands.UNIT_MOVE_CAST_SPELL, hit.point, null, left_click_command);
						}
						else {
							unit.SetSingleCommand(ObjectCommands.Commands.UNIT_MOVE_CAST_SPELL, hit.point, null, left_click_command);
						}
					}
				}
				break;
			case 10:
				//Attack move
				if (unit != null) { //Ignore if unit is somehow null
					Ray ray = camera.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Level terrain"))) {
						if (Input.GetKey(KeyCode.LeftShift)
						 || Input.GetKey(KeyCode.RightShift)) {
							//Add command to the queue if shift button is held down
							unit.AddCommand(ObjectCommands.Commands.UNIT_ATTACK_MOVE, hit.point);
						}
						else {
							unit.SetSingleCommand(ObjectCommands.Commands.UNIT_ATTACK_MOVE, hit.point);
						}
					}
				}
				break;
			}

			if(!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) left_click_command = -1;
		}
		else if (Input.GetMouseButton(0))
		{
			//if (Input.mousePosition != start_mouse_position)
			//{
			//	dragging = true;
			//	selection_box.max = Input.mousePosition;
			//}
		}
		else if (Input.GetMouseButtonUp(0))
		{
		//	if (dragging)
		//	{
		//		//Player is drag selecting
		//		bool hasUnit = false;
		//		foreach (Transform unit in owned_units.transform) {
		//			Vector3 unitScreenPos = camera.WorldToScreenPoint(unit.position);

		//			if (selection_box.Contains(unitScreenPos, true)) {
		//				if (!hasUnit) {
		//					hasUnit = true;
		//					selected_objects.Clear();
		//				}
		//				selected_objects.Add(unit.GetComponent<Objects>());
		//			}
		//		}
		//	}
		//	else {
		//		//Player clicks once without dragging
		//		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		//		RaycastHit hit;

		//		if (Physics.Raycast(ray, out hit))
		//		{
		//			Objects collided = hit.collider.gameObject.GetComponent<Objects>();
		//			if (collided != null) {
		//				selected_objects.Clear();
		//				selected_objects.Add(collided);
		//			}
		//		}
		//	}

		//	dragging = false;
		}
	}
	void	updateRightClickInputs() {
		if (Input.GetMouseButtonDown(1)) {
			if(unit != null) {	//Ignore if unit is somehow null
				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Level terrain"))) {
					//Give move command to all selected units
					if (Input.GetKey(KeyCode.LeftShift)
					 || Input.GetKey(KeyCode.RightShift)) {
						//Add command to the queue if shift button is held down
						unit.AddCommand(ObjectCommands.Commands.UNIT_MOVE, hit.point);
					}
					else {
						unit.SetSingleCommand(ObjectCommands.Commands.UNIT_MOVE, hit.point);
					}
				}
			}
		}
	}
	void	updateKeyboardInputs() {
		if (Input.GetKeyDown(KeyCode.S)) {
			unit.StopAllCommands();
		}
		else {
			if(Input.GetKeyDown(KeyCode.A)) {
				left_click_command = 10;
			}
			else if(Input.GetKeyDown(KeyCode.Q)) {
				if (unit.spells[0].isTargeted()) {
					if(unit.spells[0].isReady())
						left_click_command = 0;
				}
				else
					unit.spells[0].Cast(unit.transform.position);
			}
			else if(Input.GetKeyDown(KeyCode.W)) {
				if (unit.spells[1].isTargeted()) {
					if (unit.spells[1].isReady())
						left_click_command = 1;
				}
				else
					unit.spells[1].Cast(unit.transform.position);
			}
			else if (Input.GetKeyDown(KeyCode.E)) {
				if (unit.spells[2].isTargeted()) {
					if (unit.spells[2].isReady())
						left_click_command = 2;
				}
				else
					unit.spells[2].Cast(unit.transform.position);
			}
			else if (Input.GetKeyDown(KeyCode.R)) {
				if (unit.spells[3].isTargeted()) {
					if (unit.spells[3].isReady())
						left_click_command = 3;
				}
				else
					unit.spells[3].Cast(unit.transform.position);
			}
		}
	}
}
