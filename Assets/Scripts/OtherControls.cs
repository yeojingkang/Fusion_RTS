using UnityEngine;
using System.Collections;

public class OtherControls : MonoBehaviour {
	public GameObject   shop_menu = null;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		updateKeyboardInputs();
	}

	void	updateKeyboardInputs() {
		if(Input.GetKeyDown(KeyCode.F4)) {
			shop_menu.SetActive(!shop_menu.activeSelf);
		}
	}
}
