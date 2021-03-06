﻿using UnityEngine;
using CustomLobbyNetwork;
using UnityEngine.Networking;
using System.Collections;

public class NetworkLobbyHook : LobbyHook {
	public override void OnLobbyServerSceneLoadedForPlayer( NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer ) {
		LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
		Unit unit = gamePlayer.GetComponent<Unit>();

		// Initialize game player stuffs here (e.g. name, health, score)
		unit.name = "Player_" + lobby.m_playerName;
	}
}
