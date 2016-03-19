using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LobbyPlayer : NetworkLobbyPlayer {
	static Color[] m_teamColors = new Color[] { Color.blue, Color.red };

	public Button 		m_switchTeamButton;
	public Button		m_readyButton;
	public Button		m_waitButton;
	public Button		m_removePlayerButton;

	[SyncVar(hook = "OnMyName")]
	public string	m_playerName;
	[SyncVar(hook = "OnMyColor")]
	public Color	m_playerColor;

	public override void OnClientEnterLobby() {
		base.OnClientEnterLobby();

		if( NetworkManagerScript.s_Singleton != null )
			NetworkManagerScript.s_Singleton.OnPlayersNumberModified(1);

		if ( isLocalPlayer ) 
			SetupLocalPlayer();
		else
			SetupNetworkPlayer();

		OnMyName( m_playerName );
		OnMyColor( m_playerColor );

		// Team assignment
		if ( NetworkManagerScript.s_Singleton.m_numPlayers % 2 == 0 )
			transform.parent = GameObject.Find("Triangles").transform.GetComponentInChildren<GridLayoutGroup>().transform;
		else
			transform.parent = GameObject.Find("Cubes").transform.GetComponentInChildren<GridLayoutGroup>().transform;
	}

	public override void OnStartAuthority() {
		base.OnStartAuthority();

		SetupLocalPlayer();
	}

	void SetupLocalPlayer() {
		CheckRemoveButton();

		CmdNameChange( NetworkManagerScript.s_Singleton.m_playerName );
		GetComponentInChildren<Text>().text = m_playerName;

		CmdColorChange();
	}

	void SetupNetworkPlayer() {
		OnClientReady( false );
	}

	public void CheckRemoveButton() {
		if ( !isLocalPlayer )
			return;

		int playerCount = 0;
		foreach( PlayerController p in ClientScene.localPlayers )
			playerCount += ( p == null || p.playerControllerId == -1 ) ? 0 : 1;
	}

	public override void OnClientReady( bool readyState ) {
		// TODO: all the UI display stuffs here when client is ready
		if ( readyState ) {
			
		}
		else {
		
		}
	}

	public void OnMyName( string newName ) { m_playerName = newName; }
	public void OnMyColor( Color newColor ) { m_playerColor = newColor; }

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
	public void RpcUpdateRemoveButton() { CheckRemoveButton(); }

	[Command]
	public void CmdNameChange(string name) {
		m_playerName = name;
	}

	[Command]
	public void CmdColorChange() {
		
	}
}