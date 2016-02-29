using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectControls : MonoBehaviour {
	public GameObject   owned_units = null;
	public Canvas		canvas = null;
	Rect				selection_box = new Rect(0,0,0,0);

	List<Object>        selected_units = new List<Object>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			selection_box.min = Input.mousePosition;
			selection_box.max = Input.mousePosition;
		}

		if(Input.GetMouseButton(0)) {
			selection_box.max = Input.mousePosition;
		}

		if(Input.GetMouseButtonUp(0)) {
			Camera camera = GetComponent<Camera>();

			selected_units.Clear();

			foreach(GameObject unit in owned_units.transform) {
				Vector3 unitScreenPos = camera.WorldToScreenPoint(unit.transform.position);

				if(unitScreenPos.x > selection_box.xMin && unitScreenPos.x < selection_box.xMax
				&& unitScreenPos.y > selection_box.yMin && unitScreenPos.y < selection_box.yMax) {
					selected_units.Add(unit.GetComponent<Object>());
				}
			}
		}
	}
}
