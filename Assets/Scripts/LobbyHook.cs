using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace CustomLobbyNetwork {
	public abstract class LobbyHook : MonoBehaviour {
		public virtual void OnLobbyServerSceneLoadedForPlayer( NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer) { }
	}
}