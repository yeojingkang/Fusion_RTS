using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveTrigger : MonoBehaviour 
{
	public List<Transform> m_unitList = new List<Transform>();

	void OnTriggerEnter(Collider col)
	{
		//Check if col is a unit
		if (col.tag == "Unit") 
		{
			//Just in case, check if there already is an instance of it in the list
			if (!m_unitList.Contains (col.transform)) 
			{
				m_unitList.Add (col.transform);
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		m_unitList.Remove (col.transform);
	}
}
