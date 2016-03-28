using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkObjectUpdateSync : NetworkBehaviour {

	[SyncVar]
	Vector3			m_syncPosition;
	Vector3			m_lastPos;

	[SyncVar]
	Quaternion		m_syncRotation;
	Quaternion		m_lastRot;

	float			m_positionUpdateThreshold = 0.5f;
	float			m_rotationUpdateThreshold = 5.0f;

	float			m_lerpRate = 15.0f;

	// Update is called once per frame
	void FixedUpdate () {
		UpdatePosition();
		LerpPosition();

		UpdateRotation();
		LerpRotation();
	}

	void LerpPosition() {
		if ( isLocalPlayer )
			return; 
		
		transform.position = Vector3.Lerp( transform.position, m_syncPosition, Time.deltaTime * m_lerpRate );
	}

	void LerpRotation() {
		if ( isLocalPlayer )
			return; 
		
		transform.rotation = Quaternion.Lerp( transform.rotation, m_syncRotation, Time.deltaTime * m_lerpRate );
	}

	[Command]
	void CmdSendPositionToServer( Vector3 pos ) {
		m_syncPosition = pos;
	}

	[Command]
	void CmdSendRotationToServer( Quaternion rot ) {
		m_syncRotation = rot;
	}

	[ClientCallback]
	void UpdatePosition() {
		if ( !isLocalPlayer )
			return;

		if ( Vector3.Distance( transform.position, m_lastPos ) > m_positionUpdateThreshold ) {
			CmdSendPositionToServer( transform.position );
			m_lastPos = transform.position;
			Debug.Log("Position updated");
		}
	}

	[ClientCallback]
	void UpdateRotation() {
		if ( !isLocalPlayer )
			return;

		if ( Quaternion.Angle(transform.rotation, m_lastRot) > m_rotationUpdateThreshold ) {
			CmdSendRotationToServer( transform.rotation );
			m_lastRot = transform.rotation;
			Debug.Log("Rotation updated");
		}
	}
}
