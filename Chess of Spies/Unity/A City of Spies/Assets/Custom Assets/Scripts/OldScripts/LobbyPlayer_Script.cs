using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LobbyPlayer_Script : NetworkBehaviour {

	public GameObject[] lobbyPlayers;
	public int lobbyPlayerPersonalID;

	public short lobbyPlayerNetworkID;

	[SyncVar] public int lobbyPlayerID;

	void OnLobbyClientConnect(){
		
	}

	void GetLobbyPlayerID () {
		lobbyPlayers = GameObject.FindGameObjectsWithTag ("LobbyPlayer");
		switch (lobbyPlayers.Length) {
		case 1:
			lobbyPlayerPersonalID = 1;
			break;
		case 2: 
			lobbyPlayerPersonalID = 2;
			break;
		case 3:
			lobbyPlayerPersonalID = 3;
			break;
		case 4: 
			lobbyPlayerPersonalID = 4;
			break;
		case 5:
			lobbyPlayerPersonalID = 5;
			break;
		case 6: 
			lobbyPlayerPersonalID = 6;
			break;
		case 7:
			lobbyPlayerPersonalID = 7;
			break;
		case 8: 
			lobbyPlayerPersonalID = 8;
			break;
		}

		lobbyPlayerNetworkID = GetComponent<NetworkIdentity> ().playerControllerId;

		CmdSetLobbyPlayerID (lobbyPlayerPersonalID);
	}

	[Command]
	void CmdSetLobbyPlayerID (int newID){
		lobbyPlayerID = newID;
	}

	// Use this for initialization
	void Start () {
		GetLobbyPlayerID ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
