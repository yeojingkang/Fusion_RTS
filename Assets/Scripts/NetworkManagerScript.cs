using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

	public float			m_pingRefreshRate;
	public float			m_panelSizeMinification;
	public RectTransform	m_networkPanel;
	public Text				m_connectionStatus;
	public string			m_ipAddressToPing;

	bool				m_hasConnection;
	NetworkManager		m_networkMgr;
	NetworkManagerHUD	m_networkMgrHud;

	void Awake () {
		// Test code
		if (!m_networkMgr)
			m_networkMgr = GetComponent<NetworkManager>();

		// Network Manager HUD initialization
		m_networkMgrHud = GetComponent<NetworkManagerHUD>();
		m_networkMgrHud.manager = m_networkMgr;

		// Network panel UI
		int panelSize_x = (int)(Screen.width * m_panelSizeMinification);
		int panelSize_y = (int)(Screen.height * m_panelSizeMinification);
		m_networkPanel.sizeDelta = new Vector2(panelSize_x, panelSize_y);
		m_networkPanel.localPosition = new Vector3(0, 0, 0);

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
		if (m_hasConnection)
			m_connectionStatus.text = "Connection: YES";
		else
			m_connectionStatus.text = "Connection: NO";
	}
}
