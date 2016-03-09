using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectControls : MonoBehaviour {
	Camera				camera = null;

	public GameObject	owned_units = null;
	public Canvas		canvas = null;
	Rect				selection_box = new Rect(0, 0, 0, 0);
	bool				dragging = false;
	Vector3				start_mouse_position = new Vector3(0, 0, 0);

	List<Objects>		selected_objects = new List<Objects>();

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		updateLeftClickInputs();
		updateRightClickInputs();
	}

	void updateLeftClickInputs() {
		if (Input.GetMouseButtonDown(0))
		{
			dragging = false;
			selection_box.min = Input.mousePosition;
			selection_box.max = Input.mousePosition;
			start_mouse_position = Input.mousePosition;
		}
		else if (Input.GetMouseButton(0))
		{
			if (Input.mousePosition != start_mouse_position)
			{
				dragging = true;
				selection_box.max = Input.mousePosition;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (dragging)
			{
				//Player is drag selecting
				bool hasUnit = false;
				foreach (Transform unit in owned_units.transform)
				{
					Vector3 unitScreenPos = camera.WorldToScreenPoint(unit.position);

					if (selection_box.Contains(unitScreenPos, true)) {
						if(!hasUnit) {
							hasUnit = true;
							selected_objects.Clear();
						}
						selected_objects.Add(unit.GetComponent<Objects>());
					}
				}
			}
			else {
				//Player clicks once without dragging
				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit))
				{
					Objects collided = hit.collider.gameObject.GetComponent<Objects>();
					if (collided != null) {
						selected_objects.Clear();
						selected_objects.Add(collided);
					}
				}
			}

			dragging = false;
		}
	}
	void updateRightClickInputs() {
		if (Input.GetMouseButtonDown(1)) {
			if (selected_objects.Count > 0) {	//Ignore if no unit is selected
				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Level terrain"))) {
					//Give move command to all selected units
					if (Input.GetKey(KeyCode.LeftShift)
					 || Input.GetKey(KeyCode.RightShift)) {
						//Add command to the queue if shift button is held down
						foreach (Objects unit in selected_objects)
							unit.AddCommand(ObjectCommands.Commands.UNIT_MOVE, hit.point);
					}
					else {
						foreach (Objects unit in selected_objects)
							unit.SetSingleCommand(ObjectCommands.Commands.UNIT_MOVE, hit.point);
					}
				}
			}
		}
	}
}
