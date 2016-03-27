using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using CustomLobbyNetwork;
using System.Collections;


namespace CustomLobbyNetwork {
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
		public Button			m_backButton;
		public GameObject		m_menuCanvas;

		protected bool			_isInGame;
		protected bool 			_disconnectServer;
		protected ulong 		_currentMatchID;
		protected LobbyHook 	_lobbyHooks;

		void Start () {
			s_Singleton = this;

			if (m_networkPort == 0)
				m_networkPort = 1234;

			m_ipAddressToConnect.text = "localhost";

			// If no word is entered in editor, set ip address to Google
			if (m_ipAddressToPing.Length == 0)
				m_ipAddressToPing = "74.125.130.102";

			m_numPlayers = 0;
			_isInGame = false;
			_disconnectServer = false;
			_lobbyHooks = GetComponent<CustomLobbyNetwork.LobbyHook>();

			DontDestroyOnLoad(gameObject);
		}

		public void StartupHost() {
			if (NetworkServer.active)
				return;
			
			s_Singleton.networkPort = m_networkPort;
			s_Singleton.StartHost();
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

			s_Singleton.networkPort = m_networkPort;
			s_Singleton.networkAddress = m_ipAddressToConnect.text;

			s_Singleton.StartClient();

			s_Singleton.backDelegate = s_Singleton.StopClientClbk;
			s_Singleton.SetServerInfo( "Connecting to host", s_Singleton.networkAddress );
		}

		public void TerminateHost() {
			if (!NetworkServer.active)
				return;
			
			s_Singleton.StopHost();
		}

		public void TerminateClient() {
			if (!NetworkClient.active)
				return;

			s_Singleton.StopClient();

			m_numPlayers--;
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
			
		public override void OnLobbyClientSceneChanged( NetworkConnection conn ) {
			if ( SceneManager.GetSceneAt(0).name == lobbyScene ) {
				// lobby scene
				if ( _isInGame ) {
					if ( conn.playerControllers[0].unetView.isClient )
						backDelegate = StopHostClbk;
					else
						backDelegate = StopClientClbk;
				}
				else {
					// change to main menu panel
				}
			}
			else {
				// game scene
			}
		}

		public override void OnStartHost() {
			base.OnStartHost();
		}

		public void OnPlayersNumberModified( int count ) {
			m_numPlayers += count;
		}
			
		public void SetServerInfo( string status, string host ) {
			Debug.Log( status + "(" + host + ")" );
		}

		public delegate void BackButtonDelegate();
		public BackButtonDelegate backDelegate;
		public void GoBackButton() { backDelegate(); }

		// ============================================================== //
		// ===================== Server managements ===================== //
		// ============================================================== //

		public void AddLocalPlayer() {
			TryToAddPlayer();
		}

		public void RemovePlayer(NetworkLobbyPlayer player) {
			player.RemovePlayer();
		}

		public void SimpleBackClbk() {
			
		}

		public void StopHostClbk() {
			s_Singleton.StopHost();
			_disconnectServer = true;
		}

		public void StopClientClbk() {
			s_Singleton.StopClient();
		}

		// ============================================================== //
		// ====================== Server callbacks ====================== //
		// ============================================================== //

		public override GameObject OnLobbyServerCreateLobbyPlayer( NetworkConnection conn, short playerId ) {
			GameObject obj = Instantiate( lobbyPlayerPrefab.gameObject ) as GameObject;

			LobbyPlayer newPlayer = obj.GetComponent<LobbyPlayer>();

			for ( int i = 0; i < lobbySlots.Length; ++i ) {
				LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

				if ( p != null ) {
					p.RpcUpdateSwitchTeamButton();
				}
			}

			return obj;
		}

		public override void OnLobbyServerPlayerRemoved( NetworkConnection connectionConfig, short playerControllerId ) {
			for ( int i = 0; i < lobbySlots.Length; ++i ) {
				LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

				if ( p != null ) {
					p.RpcUpdateSwitchTeamButton();
				}
			}
		}

		public override void OnLobbyServerDisconnect( NetworkConnection conn ) {
			for ( int i = 0; i < lobbySlots.Length; ++i ) {
				LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

				if ( p != null ) {
					p.RpcUpdateSwitchTeamButton();
				}
			}
		}

		public override bool OnLobbyServerSceneLoadedForPlayer( GameObject lobbyPlayer, GameObject gamePlayer ) {
			if ( _lobbyHooks )
				_lobbyHooks.OnLobbyServerSceneLoadedForPlayer( this, lobbyPlayer, gamePlayer );

			return true;
		}
			
		// ============================================================== //
		// ==================== Countdown Management ==================== //
		// ============================================================== //

		public override void OnLobbyServerPlayersReady() { StartCoroutine(ServerCountdownCoroutine()); }
		IEnumerator ServerCountdownCoroutine() {
			// Do lobby countdown before game starts
			float remainingTime = m_startGameCountdown;
			int floorTime = Mathf.FloorToInt( remainingTime );

			while ( remainingTime > 0.0f ) {
				yield return null;

				remainingTime -= Time.deltaTime;
				int newFloorTime = Mathf.FloorToInt( remainingTime );

				if ( newFloorTime != floorTime ) {
					floorTime = newFloorTime;

					for ( int i = 0; i < lobbySlots.Length; ++i ) {
						if ( lobbySlots[i] != null ) {
							(lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(floorTime);
						}
					}
				}
			}

			for ( int i = 0; i < lobbySlots.Length; ++i ) {
				if ( lobbySlots[i] != null ) {
					(lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(0);
				}
			}

			m_menuCanvas.GetComponent<Menu>().SwitchMenu(true);
			ServerChangeScene( playScene );
		}

		// ============================================================== //
		// ====================== Client Callbacks ====================== //
		// ============================================================== //

		public override void OnClientConnect( NetworkConnection conn ) {
			base.OnClientConnect( conn );
		}

		public override void OnClientDisconnect( NetworkConnection conn ) {
			base.OnClientDisconnect( conn );
		}

		public override void OnClientError( NetworkConnection conn, int errorCode ) {
			Debug.Log( "Client error: " + ( errorCode == 6 ? "timeout" : errorCode.ToString()) );
		}
	}
}