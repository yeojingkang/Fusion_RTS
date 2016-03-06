using UnityEngine;
using System.Collections;

public class SetAspectRatio : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        this.GetComponent<Camera>().aspect =  1280f /720f;
    }
}
