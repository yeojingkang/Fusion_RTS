using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkGameManager : NetworkBehaviour {

	public GameObject		m_objectivePrefab;
	public GameObject		m_objectiveSpawnPoint;
	public int				m_counter;
	public int				m_numOfPlayers;

	public override void OnStartServer() {
		SpawnObjective();
	}

	void SpawnObjective() {
		GameObject obj = GameObject.Instantiate( m_objectivePrefab, m_objectiveSpawnPoint.transform.position, Quaternion.identity ) as GameObject;
		NetworkServer.Spawn( obj );
	}
}
