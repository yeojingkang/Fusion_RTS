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

	public Vector3 m_startingDirection = new Vector3 (-1, 0, 0);
	public Vector3 m_currentDirection = Vector3.zero;
	public Vector3 m_newDirection = Vector3.zero;
	public float m_currentAngle = 0;
	public float m_previousAngle = 0;

	public Rigidbody m_rigidbody = null;

	// Use this for initialization
	void Start () 
	{
		m_arrowMarkers [0] = this.transform.FindChild ("Direction Arrow 1");
		m_arrowMarkers [1] = this.transform.FindChild ("Direction Arrow 2");
		m_arrowMarkers [2] = this.transform.FindChild ("Direction Arrow 3");

		//m_arrowMarkers [0].GetComponent<SpriteRenderer> ().enabled = false;
		//m_arrowMarkers [1].GetComponent<SpriteRenderer> ().enabled = false;
		//m_arrowMarkers [2].GetComponent<SpriteRenderer> ().enabled = false;

		m_nextSwapTime = 5;

		m_originalRotation = transform.rotation;

		m_rigidbody = this.GetComponent<Rigidbody> ();

		//m_currentAngle = Vector3.Angle (new Vector3(-1,0, 0) , m_rigidbody.velocity.normalized);
		m_currentAngle = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float currentMagnitude = m_rigidbody.velocity.magnitude;
		m_arrowSwapTime = 2.0f;

		//If ball is moving
		if (currentMagnitude > 0) 
		{
			if(m_currentDirection.magnitude != 0)
			{
				m_newDirection = m_rigidbody.velocity.normalized;
				m_currentAngle = SignedAngle (m_currentDirection, m_newDirection);
				if (m_currentAngle != m_previousAngle) 
				{
					foreach (Transform arrow in m_arrowMarkers) 
					{
						arrow.RotateAround (this.transform.position, Vector3.up, (m_currentAngle));
					}

					m_previousAngle = m_currentAngle;
				}
				m_currentDirection = m_newDirection;
			}
			/*
			 * 
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

			/*
			if (Time.realtimeSinceStartup > m_nextSwapTime) 
			{
				m_arrowMarkers [m_currentArrow].GetComponent<SpriteRenderer> ().enabled = true;
				print (m_arrowMarkers[m_currentArrow]);
				if (m_currentArrow == 0) 
				{
					m_arrowMarkers [m_arrowMarkers.Length - 1].GetComponent<SpriteRenderer> ().enabled = false;
				} 

				else 
				{
					m_arrowMarkers [m_currentArrow - 1].GetComponent<SpriteRenderer> ().enabled = false;
				}

				m_nextSwapTime = Time.realtimeSinceStartup + m_arrowSwapTime;

				m_currentArrow++;
				if (m_currentArrow >= m_arrowMarkers.Length) 
				{
					m_currentArrow = 0;
				}

			}
			*/
			m_arrowMarkers [0].GetComponent<SpriteRenderer> ().enabled = true;
		} 

		else 
		{
			m_arrowMarkers [0].GetComponent<SpriteRenderer> ().enabled = false;
			m_arrowMarkers [1].GetComponent<SpriteRenderer> ().enabled = false;
			m_arrowMarkers [2].GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	void LateUpdate()
	{
		this.transform.rotation = m_originalRotation;
	}

	float SignedAngle(Vector3 a, Vector3 b)
	{
		float angle = Vector3.Angle (a, b);

		return angle * Mathf.Sign (Vector3.Cross (a, b).y);
	}
}
