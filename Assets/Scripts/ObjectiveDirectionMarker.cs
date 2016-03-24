using UnityEngine;
using System.Collections;

public class ObjectiveDirectionMarker : MonoBehaviour 
{
	public Transform[] m_arrowMarkers = new Transform [3];
	public float[] m_speedThreshold = new float[2];

	public int m_currentArrow = 0;
	public float m_arrowSwapTime = 0;
	public float m_nextSwapTime = 0;

	private Quaternion m_originalRotation;
	public float m_currentAngle = 0;

	public Rigidbody m_rigidbody = null;

	// Use this for initialization
	void Start () 
	{
		m_arrowMarkers [0] = this.transform.FindChild ("Direction Arrow 3");
		m_arrowMarkers [1] = this.transform.FindChild ("Direction Arrow 2");
		m_arrowMarkers [2] = this.transform.FindChild ("Direction Arrow 1");

		//m_arrowMarkers [0].GetComponent<SpriteRenderer> ().enabled = false;
		//m_arrowMarkers [1].GetComponent<SpriteRenderer> ().enabled = false;
		//m_arrowMarkers [2].GetComponent<SpriteRenderer> ().enabled = false;

		m_originalRotation = transform.rotation;

		m_rigidbody = this.GetComponent<Rigidbody> ();

		m_currentAngle = Vector3.Angle (new Vector3(-1, 0, 0) , m_rigidbody.velocity.normalized);
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		float currentMagnitude = m_rigidbody.velocity.magnitude;
		m_arrowSwapTime = 2.0f;

		//If ball is moving
		if (currentMagnitude > 0) 
		{
			/*
			if (currentMagnitude > m_speedThreshold [0]) 
			{
			} 

			else if (currentMagnitude > m_speedThreshold [1] && currentMagnitude < m_speedThreshold [0]) 
			{
			}

			else if (currentMagnitude < m_speedThreshold [1] && currentMagnitude > 0)
			{
				
			}
			*/

			if (Time.realtimeSinceStartup > m_nextSwapTime) 
			{
				//print ("HI");
				m_arrowMarkers [m_currentArrow].GetComponent<SpriteRenderer> ().enabled = true;

				if (m_currentArrow == 0) 
				{
					m_arrowMarkers [m_arrowMarkers.Length - 1].GetComponent<SpriteRenderer> ().enabled = false;
				} 

				else 
				{
					m_arrowMarkers [m_currentArrow].GetComponent<SpriteRenderer> ().enabled = false;
				}

				m_nextSwapTime = Time.realtimeSinceStartup + m_arrowSwapTime;

				m_currentArrow++;
				if (m_currentArrow >= m_arrowMarkers.Length) 
				{
					m_currentArrow = 0;
					//print ("CHANGE BACK TO 0");
				}
			}
		} 

		else 
		{
			//m_arrowMarkers [0].GetComponent<SpriteRenderer> ().enabled = false;
			//m_arrowMarkers [1].GetComponent<SpriteRenderer> ().enabled = false;
			//m_arrowMarkers [2].GetComponent<SpriteRenderer> ().enabled = false;
			//print ("WHAT");
		}
	}

	void LateUpdate()
	{
		this.transform.rotation = m_originalRotation;
	}
}
