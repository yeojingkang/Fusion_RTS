using UnityEngine;
using System.Collections;

public class FogOfWarEntity : MonoBehaviour {

	private int m_LayerMask;
	public Transform m_FogOfWarPlane;

	// Use this for initialization
	void Start () {
		m_LayerMask = (int)(1 << 9);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
		Ray rayToPlayerPos = Camera.main.ScreenPointToRay (screenPos);

		RaycastHit hit;
		if (Physics.Raycast (rayToPlayerPos, out hit, 1000, m_LayerMask)) 
		{
			m_FogOfWarPlane.GetComponent<Renderer> ().material.SetVector ("_EntityPos", hit.point);
		}
	}
}
