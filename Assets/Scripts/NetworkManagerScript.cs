using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

	public float			m_pingRefreshRate;
	public float			m_panelSizeMinification;
	//public RectTransform	m_networkPanel;
	public Text				m_connectionStatus;
	public string			m_ipAddressToPing;
	public InputField		m_ipAddressToConnect;

	bool				m_hasConnection;
	NetworkManager		m_networkMgr;

	void Awake () {
		// Test code
		if (!m_networkMgr)
			m_networkMgr = GetComponent<NetworkManager>();

		// Network panel UI
		//int panelSize_x = (int)(Screen.width * m_panelSizeMinification);
		//int panelSize_y = (int)(Screen.height * m_panelSizeMinification);
		//m_networkPanel.sizeDelta = new Vector2(panelSize_x, panelSize_y);
		//m_networkPanel.localPosition = new Vector3(0, 0, 0);

		m_hasConnection = false;

		// If no word is entered in editor, set ip address to Google
		if (m_ipAddressToPing.Length == 0)
			m_ipAddressToPing = "74.125.130.102";

		StartCoroutine( TestConnection() );
	}

	void Update() {
		UpdateConnectionStatus();
	}

	// To test if there is internet connection
	IEnumerator TestConnection() {
		float timeElapsed = 0.0f;
		float refreshRate = m_pingRefreshRate;

		while( true ) {
			Ping pingTest = new Ping( m_ipAddressToPing );   // HARDCODED GOOGLE IP ADDRESS
			timeElapsed = 0.0f;

			while ( !pingTest.isDone ) {
				timeElapsed += Time.deltaTime;
				if ( timeElapsed > refreshRate ) {
					m_hasConnection = false;
					break;
				}

				yield return null;
			}
			if ( timeElapsed <= refreshRate )
				m_hasConnection = true;

			yield return null;
		}
	}

	void UpdateConnectionStatus() {
		//if (m_hasConnection)
		//	m_connectionStatus.text = "Connection: YES";
		//else
		//	m_connectionStatus.text = "Connection: NO";
	}

	public void StartHost() {
		if (NetworkServer.active)
			return;

		m_networkMgr.StartHost();
	}

	public void StartClient() {
		if (NetworkClient.active)
			return;

		m_ipAddressToConnect.text.ToLower();
		if (!m_ipAddressToConnect.text.Equals("localhost") &&
			m_ipAddressToConnect.text.Contains("abcdefghijklmnopqrstuvwxyz")) {
			Debug.Log("IP Address entered is invalid!");
			return;
		}

		m_networkMgr.StartClient();
		m_networkMgr.networkAddress = m_ipAddressToConnect.text;

		if (!ClientScene.ready)
		{
			ClientScene.Ready(m_networkMgr.client.connection);

			if (ClientScene.localPlayers.Count == 0)
				ClientScene.AddPlayer(0);
		}
	}

	public void StopHost() {
		if (!NetworkServer.active)
			return;
		
		m_networkMgr.StopHost();
	}

	public void StopClient() {
		if (!NetworkClient.active)
			return;

		m_networkMgr.StopClient();
	}
}
