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

	List<Objects>		selected_units = new List<Objects>();

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
			selected_units.Clear();

			if (dragging)
			{
				//Player is drag selecting
				foreach (Transform unit in owned_units.transform)
				{
					Vector3 unitScreenPos = camera.WorldToScreenPoint(unit.position);

					if (selection_box.Contains(unitScreenPos, true))
					{
						selected_units.Add(unit.GetComponent<Objects>());
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
					if (collided != null)
						selected_units.Add(collided);
				}
			}

			dragging = false;
		}
	}
	void updateRightClickInputs() {
		if(Input.GetMouseButtonDown(1)) {
			if(selected_units.Count > 0) {
				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				//LayerMask mask = 

				if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Level terrain"))) {

					foreach (Objects unit in selected_units) {
						unit.SetSingleCommand(ObjectCommands.Commands.UNIT_MOVE, hit.point);
					}
				}
			}
		}
	}
}
