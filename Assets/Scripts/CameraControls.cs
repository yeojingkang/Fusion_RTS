using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
	public float	keyboard_speed = 10.0f;
	public float	mouse_speed = 25.0f;
	public float	mouse_drag_speed = 35.0f;
	public float	mouse_screen_edge_threshold = 0.01f;


	public float		mouse_zoom_amount = 2.0f;
	public const int	zoom_max = 2;
	public const int	zoom_min = 0;
	int					cur_zoom = 0;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Confined;
	}
	
	// Update is called once per frame
	void Update () {
		update_keyboard();
		update_mouse();

		update_camera_position();
	}

	void update_camera_position() {

	}
	void update_keyboard() {
		float dtSpeed = keyboard_speed * Time.deltaTime;

		//Up
		if (Input.GetKey(KeyCode.UpArrow)) {
			gameObject.transform.Translate(gameObject.transform.forward.x * dtSpeed, 0, gameObject.transform.forward.z * dtSpeed, Space.World);
		}
		//Down
		if (Input.GetKey(KeyCode.DownArrow)) {
			gameObject.transform.Translate(-gameObject.transform.forward.x * dtSpeed, 0, -gameObject.transform.forward.z * dtSpeed, Space.World);
		}
		//Left
		if (Input.GetKey(KeyCode.LeftArrow)) {
			gameObject.transform.Translate(-gameObject.transform.right * dtSpeed);
		}
		//Right
		if (Input.GetKey(KeyCode.RightArrow)) {
			gameObject.transform.Translate(gameObject.transform.right * dtSpeed);
		}
	}

	void update_mouse() {
		//Check if player is drag-scrolling (middle-mouse button is held down)
		if(Input.GetMouseButton(2)) {
			update_mouse_drag();
		}
		else {
			update_mouse_at_screen_edge();
		}

		update_mouse_scroll();
	}

	void update_mouse_drag() {
		float dtSpeed = mouse_drag_speed * Time.deltaTime;
		float x_move = Input.GetAxis("Mouse X");
		float y_move = Input.GetAxis("Mouse Y");


		gameObject.transform.Translate(-gameObject.transform.forward.x * y_move * dtSpeed, 0, -gameObject.transform.forward.z * y_move * dtSpeed, Space.World);
		gameObject.transform.Translate(-gameObject.transform.right * x_move * dtSpeed);
	}
	void update_mouse_at_screen_edge() {
		float dtSpeed = mouse_speed * Time.deltaTime;

		// Up/down
		if (Input.mousePosition.y <= Screen.height * mouse_screen_edge_threshold) {
			gameObject.transform.Translate(-gameObject.transform.forward.x * dtSpeed, 0, -gameObject.transform.forward.z * dtSpeed, Space.World);
		}
		else if (Screen.height - Input.mousePosition.y <= Screen.height * mouse_screen_edge_threshold) {
			gameObject.transform.Translate(gameObject.transform.forward.x * dtSpeed, 0, gameObject.transform.forward.z * dtSpeed, Space.World);
		}

		// Left/right
		if (Input.mousePosition.x <= Screen.width * mouse_screen_edge_threshold) {
			gameObject.transform.Translate(-gameObject.transform.right * dtSpeed);
		}
		else if (Screen.width - Input.mousePosition.x <= Screen.width * mouse_screen_edge_threshold) {
			gameObject.transform.Translate(gameObject.transform.right * dtSpeed);
		}
	}
	void update_mouse_scroll() {
		float scroll = Input.GetAxis("Mouse ScrollWheel");

		if(scroll > 0f) {
			if(cur_zoom >= zoom_max)
				cur_zoom = zoom_max;
			else
				++cur_zoom;
			update_zoom_position();
		}
		else if(scroll < 0f) {
			if (cur_zoom <= zoom_min)
				cur_zoom = zoom_min;
			else
				--cur_zoom;
			update_zoom_position();
		}
	}

	void update_zoom_position() {
		//gameObject.transform.position = gameObject.transform.position + gameObject.transform.forward * cur_zoom;
	}
}