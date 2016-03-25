using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

namespace CustomLobbyNetwork {
	public class LobbyPlayer : NetworkLobbyPlayer {
		public bool			m_playerReady;

		public Button 		m_switchTeamButton;
		public Button		m_lobbyBackButton;

		[SyncVar(hook = "OnMyName")]
		public string	m_playerName;
		[SyncVar(hook = "OnMyTeam")]
		public int		m_team;

		public Transform	m_currentTeam;

		Transform		m_teamCube_1;
		Transform		m_teamCube_2;

		void Awake() {
			m_teamCube_1 = GameObject.Find("Triangles").transform.GetComponentInChildren<GridLayoutGroup>().transform;
			m_teamCube_2 = GameObject.Find("Cubes").transform.GetComponentInChildren<GridLayoutGroup>().transform;
		
			transform.parent = m_currentTeam;
		}

		void Update() {
			m_currentTeam = ( m_team == 1 ) ? m_teamCube_1 : m_teamCube_2;
			transform.parent = m_currentTeam;

			// Gotta do it this way to make the name displaying correctly on both sides
			GetComponentInChildren<Text>().text = m_playerName;
		}

		public override void OnClientEnterLobby() {
			base.OnClientEnterLobby();

			if( CustomLobbyNetwork.NetworkManagerScript.s_Singleton != null )
				CustomLobbyNetwork.NetworkManagerScript.s_Singleton.OnPlayersNumberModified(1);
			
			if ( isLocalPlayer ) 
				SetupLocalPlayer();
			else
				SetupNetworkPlayer();

			OnMyName( m_playerName );
			OnMyTeam( m_team );

			// Team assignment
			//if ( NetworkManagerScript.s_Singleton.m_numPlayers % 2 == 0 )
			//	transform.parent = m_teamCube_1;
			//else
			//	transform.parent = m_teamCube_2;

			m_team = ( CustomLobbyNetwork.NetworkManagerScript.s_Singleton.m_numPlayers % 2 == 0 ) ? 1 : 2;
			m_currentTeam = ( m_team == 1 ) ? m_teamCube_1 : m_teamCube_2;
			transform.parent = m_currentTeam;
		}

		public override void OnStartAuthority() {
			base.OnStartAuthority();

			SetupLocalPlayer();
		}

		void SetupLocalPlayer() {
			GetComponentInChildren<Text>().text = m_playerName;
			CmdNameChange( CustomLobbyNetwork.NetworkManagerScript.s_Singleton.m_playerName );
			GetComponent<Image>().color = Color.red;

			SetupRoomButtons();

			//CmdTeamChange();
		}

		void SetupNetworkPlayer() {
			OnClientReady( false );
		}

		void SetupRoomButtons() {
			m_switchTeamButton = GameObject.Find("SwitchSides_Button").GetComponent<Button>();
			if ( !m_switchTeamButton )
				Debug.Log("Switch team button initialization failed!");

			m_switchTeamButton.onClick.RemoveAllListeners();
			m_switchTeamButton.onClick.AddListener(SwitchTeams);

			m_lobbyBackButton = GameObject.Find("RoomMenu").transform.FindChild("Back Button").GetComponent<Button>();
			if ( !m_lobbyBackButton )
				Debug.Log("Back button initialization failed!");

			m_lobbyBackButton.onClick.RemoveListener(OnRemovePlayerClick);
			m_lobbyBackButton.onClick.AddListener(OnRemovePlayerClick);
		}

		void SwitchTeams() {
			//m_team = ( m_team != 1 ) ? 1 : 2;
			//m_currentTeam = ( m_team == 1 ) ? m_teamCube_1 : m_teamCube_2;
			//transform.parent = m_currentTeam;
			CmdTeamChange();
		}

		public override void OnClientReady( bool readyState ) {
			// TODO: all the UI display stuffs here when client is ready
			if ( readyState ) {
				m_playerReady = true;
			}
			else {
				m_playerReady = false;
			}
		}

		public void OnMyName( string newName ) { m_playerName = newName; }
		public void OnMyTeam( int team ) { m_team = team; }

		public void OnRemovePlayerClick() {
			if ( !isLocalPlayer )
				return;

			RemovePlayer();
		}

		[ClientRpc]
		public void RpcUpdateCountdown( int countdown ) {
			// TODO: do network lobby manager countdown text
		}

		[ClientRpc]
		public void RpcUpdateSwitchTeamButton() { SetupRoomButtons(); }

		[Command]
		public void CmdNameChange(string name) {
			m_playerName = name;
			GetComponentInChildren<Text>().text = m_playerName;
		}

		[Command]
		public void CmdTeamChange() {
			m_team = ( m_team != 1 ) ? 1 : 2;
		}

		public void OnDestroy() {

			if ( CustomLobbyNetwork.NetworkManagerScript.s_Singleton != null )
				CustomLobbyNetwork.NetworkManagerScript.s_Singleton.OnPlayersNumberModified(-1);
		}
	}
}