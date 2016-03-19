using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public enum PlayerTeam {
	TEAM_CUBE,
	TEAM_TRI
};

public class NetworkManagerScript : NetworkLobbyManager {
	static public NetworkManagerScript s_Singleton;
	public int				m_networkPort;
	public int				m_numPlayers;
	public int				m_startGameCountdown;
	public float			m_pingRefreshRate;
	public float			m_panelSizeMinification;
	public Text				m_connectionStatus;
	public string			m_ipAddressToPing;
	public string			m_playerName;
	public InputField		m_ipAddressToConnect;
	public InputField		m_nameInputField;

	void Start () {
		s_Singleton = this;

		if (m_networkPort == 0)
			m_networkPort = 1234;

		m_ipAddressToConnect.text = "localhost";

		// If no word is entered in editor, set ip address to Google
		if (m_ipAddressToPing.Length == 0)
			m_ipAddressToPing = "74.125.130.102";

		DontDestroyOnLoad(gameObject);
	}

	public void StartupHost() {
		if (NetworkServer.active)
			return;
		
		NetworkLobbyManager.singleton.networkPort = m_networkPort;
		NetworkLobbyManager.singleton.StartHost();
	}

	public void StartupClient() {
		if (NetworkClient.active)
			return;

		m_ipAddressToConnect.text.ToLower();
		if (!m_ipAddressToConnect.text.Equals("localhost") &&
			m_ipAddressToConnect.text.Contains("abcdefghijklmnopqrstuvwxyz")) {
			Debug.Log("IP Address entered is invalid!");
			return;
		}

		NetworkLobbyManager.singleton.networkPort = m_networkPort;
		NetworkLobbyManager.singleton.networkAddress = m_ipAddressToConnect.text;

		NetworkLobbyManager.singleton.StartClient();

		m_numPlayers++;
	}

	public void TerminateHost() {
		if (!NetworkServer.active)
			return;
		
		NetworkLobbyManager.singleton.StopHost();
	}

	public void TerminateClient() {
		if (!NetworkClient.active)
			return;

		NetworkLobbyManager.singleton.StopClient();

		m_numPlayers--;
	}

	public void AddLocalPlayer() {
		
	}

	public void RemovePlayer(NetworkLobbyPlayer player) {
		player.RemovePlayer();
	}

	// TODO: after all the UI is done, setup this
	void OnLevelWasLoaded( int level ) {
		if ( level == 0 ) 
			SetupMenuSceneButtons();
		else
			SetupInGameSceneButtons();
	}

	void SetupMenuSceneButtons() {
		
	}

	void SetupInGameSceneButtons() {
	
	}

	public void OnPressStartGame() {
		m_playerName = m_nameInputField.text;
	}

	public bool GameStartCountdown() {
		float timer = (float)m_startGameCountdown - Time.deltaTime;
		if ( timer <= 0.0f )
			return true;

		return false;
	}

	public void OnPlayersNumberModified( int count ) {
		m_numPlayers += count;
	}

	public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerId) {
		//GameObject obj = Instantiate( m_networkLobbyPlayerPrefab ) as GameObject;



		return null;
	}


}
