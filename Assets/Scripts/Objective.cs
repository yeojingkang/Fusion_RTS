﻿using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour 
{
	public float m_speedLimit = 0;
	public float m_pushSpeed = 0;
	public float m_slowSpeed = 0;
	public float m_stopThreshold = 0;

	private Rigidbody m_rigidbody = null;
	private ObjectiveTrigger m_unitDetection = null;

	// Use this for initialization
	void Start () 
	{
		m_rigidbody = this.GetComponent<Rigidbody> ();
		m_unitDetection = this.transform.GetComponentInChildren<ObjectiveTrigger> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 currentPos = this.transform.position;
		Vector3 dirVector = Vector3.zero;

		//Check if there is any unit within detection radius
		if (m_unitDetection.m_unitList.Count > 0) 
		{
			foreach (Transform obj in m_unitDetection.m_unitList) 
			{
				//Determine the push vector for each unit
				Vector3 direction = (currentPos - obj.position).normalized;

				dirVector += direction;
			}

			//Normalize the resultant directional vector
			dirVector = dirVector.normalized * m_pushSpeed * Time.deltaTime;

			m_rigidbody.AddForce (dirVector);

			//Check if adding the current force will result in the magnitude reaching the top speed
			if (m_rigidbody.velocity.magnitude > m_speedLimit) 
			{
				m_rigidbody.velocity = m_rigidbody.velocity.normalized * m_speedLimit;
			} 
		}

		else 
		{
			dirVector = -m_rigidbody.velocity.normalized * m_slowSpeed * Time.deltaTime;

			//Adds a force based on the resultant directional vector
			m_rigidbody.AddForce (dirVector);

			//Check if adding the current force will result in the magnitude reaching 0 velocity
			if (m_rigidbody.velocity.magnitude < m_stopThreshold) 
			{
				m_rigidbody.velocity = Vector3.zero;
			} 
		}
	}
}