using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AIPlayer_Script : NetworkBehaviour {

	[SyncVar]public int aiPlayerID;
	[SyncVar]public string aiPlayerNickname;
	[SyncVar]public int aiPlayerTeam;
	[SyncVar]public string aiPlayerLifeline;
	[SyncVar]public string aiPlayerContract;
	[SyncVar]public int aiPlayerTurnPosition;
	[SyncVar]public int aiPlayerTurnStart;
	[SyncVar]public int aiPlayerTurnComplete;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
