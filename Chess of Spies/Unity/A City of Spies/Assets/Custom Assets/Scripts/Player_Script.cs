using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player_Script : NetworkBehaviour {

	public Camera playerCamera;
	//public GameObject scriptsGameObject;
	public Sprite white;
	public Sprite black;
	public Sprite grey;

	public GameObject TeamBackgroundObj;
	public Image TeamBackgroundImage;
	public Text nicknamePlaceholder;
	public Text nicknameInputText;

	public GameObject playerOneListing;
	public GameObject playerTwoListing;
	public GameObject playerThreeListing;
	public GameObject playerFourListing;
	public GameObject playerFiveListing;
	public GameObject playerSixListing;
	public GameObject playerSevenListing;
	public GameObject playerEightListing;

	public bool setFirstName;

	public GameObject[] lobbyPlayers;
	public GameObject[] players;
	public List<GameObject> whiteTeam;
	public List<GameObject> blackTeam;

	public bool playerStart;

	public short playerNetworkID;
	public int playerPersonalID;
	public Transform playerTransform;

	public float whiteTeamSize;
	public float blackTeamSize;

	public int playerPersonalTeam;

	public string[] piecesStrings = new string[] {"p","q","k1","k2","r1","r2","b1","b2"};
	public GameObject[] pieceCanvases;

	public string playerPersonalLifeline;
	public string playerPersonalContract;

	[SyncVar]public int playerTeam;
	[SyncVar]public int playerID;
	[SyncVar]public string playerNickname;
	[SyncVar]public string playerLifeline;
	[SyncVar]public string playerContract;
	[SyncVar]public int playerReady;
	[SyncVar]public int playerTurnPosition;
	[SyncVar]public int playerTurnStart;
	[SyncVar]public string playerSelectedPiece;
	[SyncVar]public int playerLifelineCaptured;
	[SyncVar]public int playerContractCaptured;
	[SyncVar]public int playerTurnComplete;

	public GameObject lifelineIcon;
	public GameObject lifelineNumber;
	public GameObject contractIcon;
	public GameObject contractNumber;
	public GameObject lifelineIconInGame;
	public GameObject lifelineNumberInGame;
	public GameObject contractIconInGame;
	public GameObject contractNumberInGame;

	public GameObject pawn;
	public GameObject queen;
	public GameObject knightOne;
	public GameObject knightTwo;
	public GameObject bishopOne;
	public GameObject bishopTwo;
	public GameObject RookOne;
	public GameObject RookTwo;

	public float x = 0;
	public float z = 0;


	public int totalPlayersSaved;
	public bool savedPlayers = false;

	//PLAYER STARTS WITH ID AND TEAM
	public override void OnStartLocalPlayer (){
		HidePlayersStart ();
		GetTeamSize ();
		playerTransform = transform;
		GetPlayerNetworkID ();
		SetPlayerFirstName ();
		GetPlayerTeam ();
		GetPlayerLifeline ();
		GetPlayerContract ();

		//MOVE TO GAMESYSTEM FOR EACH PLAYERS TURN
		//SetPieceLocalCanvasCamera ();


	}

	public void SetPieceLocalCanvasCamera () {
		
		pieceCanvases = GameObject.FindGameObjectsWithTag ("PieceCanvas");

		foreach (GameObject pieceCanvas in pieceCanvases) {
			pieceCanvas.GetComponent<Canvas> ().worldCamera = playerCamera;
		}

	}

	void HidePlayersStart (){ 
		playerOneListing = GameObject.FindGameObjectWithTag ("PlayerOneListing");
		playerTwoListing = GameObject.FindGameObjectWithTag ("PlayerTwoListing");
		playerThreeListing = GameObject.FindGameObjectWithTag ("PlayerThreeListing");
		playerFourListing = GameObject.FindGameObjectWithTag ("PlayerFourListing");
		playerFiveListing = GameObject.FindGameObjectWithTag ("PlayerFiveListing");
		playerSixListing = GameObject.FindGameObjectWithTag ("PlayerSixListing");
		playerSevenListing = GameObject.FindGameObjectWithTag ("PlayerSevenListing");
		playerEightListing = GameObject.FindGameObjectWithTag ("PlayerEightListing");
		playerOneListing.SetActive (false);
		playerTwoListing.SetActive (false);
		playerThreeListing.SetActive (false);
		playerFourListing.SetActive (false);
		playerFiveListing.SetActive (false);
		playerSixListing.SetActive (false);
		playerSevenListing.SetActive (false);
		playerEightListing.SetActive (false);
	}

//	void RegesterPrefabs () {
//		ClientScene.RegisterPrefab (pawn);
//		ClientScene.RegisterPrefab (queen);
//		ClientScene.RegisterPrefab (knightOne);
//		ClientScene.RegisterPrefab (knightTwo);
//		ClientScene.RegisterPrefab (bishopOne);
//		ClientScene.RegisterPrefab (bishopTwo);
//		ClientScene.RegisterPrefab (RookOne);
//		ClientScene.RegisterPrefab (RookTwo);
//	}

	//SETS INITIAL TEAM SIZE WHEN PLAYER JOINS
	void GetTeamSize (){
		float totalPlayers = players.Length;
		whiteTeamSize = Mathf.RoundToInt (totalPlayers / 2);
		blackTeamSize = Mathf.FloorToInt (totalPlayers / 2);
	}

	//SETS PLAYER'S ID AND NAME
	void GetPlayerNetworkID (){

		players = GameObject.FindGameObjectsWithTag ("Player");

		playerNetworkID = GetComponent<NetworkIdentity> ().playerControllerId;

		switch (players.Length) {
		case 1:
			playerPersonalID = 1;
			break;
		case 2: 
			playerPersonalID = 2;
			break;
		case 3:
			playerPersonalID = 3;
			break;
		case 4: 
			playerPersonalID = 4;
			break;
		case 5:
			playerPersonalID = 5;
			break;
		case 6: 
			playerPersonalID = 6;
			break;
		case 7:
			playerPersonalID = 7;
			break;
		case 8: 
			playerPersonalID = 8;
			break;
		}

		CmdSetPlayerID (playerPersonalID);
		CmdSetNickname (GetPlayerName ());

	}

	string GetPlayerName (){
		string firstPlayerName = "Player " + playerPersonalID.ToString ();
		return firstPlayerName;
	}

	//CMD FOR PLAYERID AND PLAYERNICKNAME
	[Command]
	void CmdSetPlayerID(int newID){
		playerID = newID;
	}

	[Command]
	void CmdSetNickname(string newName){
		playerNickname = newName;
	}

	//SETS NAME TO UI
	void SetPlayerFirstName(){
		if (!isLocalPlayer) {
			playerTransform.name = playerNickname;
		} else {
			playerTransform.name = GetPlayerName();
		}
	}

	//GETS PLAYER'S TEAM
	void GetPlayerTeam ()
	{
		if (players.Length <= 4) {
			int randomTeam = Random.Range (1, 3);
			playerPersonalTeam = randomTeam;
		}
		if (players.Length > 4 && whiteTeam.Count >= whiteTeamSize) {
			int randomTeam = 2;
			playerPersonalTeam = randomTeam;
		}
		if (players.Length > 4 && blackTeam.Count >= blackTeamSize) {
			int randomTeam = 1;
			playerPersonalTeam = randomTeam;
		}

		CmdSetPlayerTeam (playerPersonalTeam);
		SetPlayerTeamBackground (playerPersonalTeam);

	}

	//CMD FOR PLAYERTEAM
	[Command]
	void CmdSetPlayerTeam(int newPlayerTeam){
		playerTeam = newPlayerTeam;
	}

	//SETS TEAM TO UI
	void SetPlayerTeamBackground (int tempPlayerTeam){
		if (isLocalPlayer) {
			TeamBackgroundImage = GameObject.FindGameObjectWithTag ("TeamBackground").GetComponent<Image> ();

			switch (tempPlayerTeam) {
			case 1:
				TeamBackgroundImage.sprite = white;
				break;
			case 2:
				TeamBackgroundImage.sprite = black;
				break;
			}
		}
	}

	//GETS PLAYERS LIFELINE AND CONTRACT
	public void GetPlayerLifeline () {
		string randomPiece = piecesStrings[Random.Range (0, piecesStrings.Length)];
		foreach (GameObject player in players) {
			if (player.GetComponent<Player_Script> ().playerPersonalID == 0) {
				if (randomPiece == player.GetComponent<Player_Script> ().playerLifeline) {
					GetPlayerLifeline ();
					return;
				} 
			}
		}
		playerPersonalLifeline = randomPiece;

		CmdSetPlayerLifeline (playerPersonalLifeline);
		SetPlayerLifelineUI (playerLifeline);
	}

	[Command]
	void CmdSetPlayerLifeline(string newLifeline){
		playerLifeline = newLifeline;
	}

	void SetPlayerLifelineUI (string newLifeline){
		lifelineIcon = GameObject.FindGameObjectWithTag ("LifelineIcon");
		lifelineNumber = GameObject.FindGameObjectWithTag ("LifelineNumber");
		lifelineIconInGame = GameObject.FindGameObjectWithTag ("LifelineIconInGame");
		lifelineNumberInGame = GameObject.FindGameObjectWithTag ("LifelineNumberInGame");
		if (newLifeline == "p") {
			lifelineIcon.GetComponent<Text> ().text = "o";
			lifelineNumber.GetComponent<Text> ().text = "";
			lifelineIconInGame.GetComponent<Text> ().text = "o";
			lifelineNumberInGame.GetComponent<Text> ().text = "";
		}
		if (newLifeline == "q") {
			lifelineIcon.GetComponent<Text> ().text = "w";
			lifelineNumber.GetComponent<Text> ().text = "";
			lifelineIconInGame.GetComponent<Text> ().text = "w";
			lifelineNumberInGame.GetComponent<Text> ().text = "";
		}
		if (newLifeline == "k1") {
			lifelineIcon.GetComponent<Text> ().text = "j";
			lifelineNumber.GetComponent<Text> ().text = "1";
			lifelineIconInGame.GetComponent<Text> ().text = "j";
			lifelineNumberInGame.GetComponent<Text> ().text = "1";
		}
		if (newLifeline == "k2") {
			lifelineIcon.GetComponent<Text> ().text = "j";
			lifelineNumber.GetComponent<Text> ().text = "2";
			lifelineIconInGame.GetComponent<Text> ().text = "j";
			lifelineNumberInGame.GetComponent<Text> ().text = "2";
		}
		if (newLifeline == "b1") {
			lifelineIcon.GetComponent<Text> ().text = "n";
			lifelineNumber.GetComponent<Text> ().text = "1";
			lifelineIconInGame.GetComponent<Text> ().text = "n";
			lifelineNumberInGame.GetComponent<Text> ().text = "1";
		}
		if (newLifeline == "b2") {
			lifelineIcon.GetComponent<Text> ().text = "n";
			lifelineNumber.GetComponent<Text> ().text = "2";
			lifelineIconInGame.GetComponent<Text> ().text = "n";
			lifelineNumberInGame.GetComponent<Text> ().text = "2";
		}
		if (newLifeline == "r1") {
			lifelineIcon.GetComponent<Text> ().text = "t";
			lifelineNumber.GetComponent<Text> ().text = "1";
			lifelineIconInGame.GetComponent<Text> ().text = "t";
			lifelineNumberInGame.GetComponent<Text> ().text = "1";
		}
		if (newLifeline == "r2") {
			lifelineIcon.GetComponent<Text> ().text = "t";
			lifelineNumber.GetComponent<Text> ().text = "2";
			lifelineIconInGame.GetComponent<Text> ().text = "t";
			lifelineNumberInGame.GetComponent<Text> ().text = "2";
		}
	}

	public void GetPlayerContract () {
		string randomPiece = piecesStrings[Random.Range (0, piecesStrings.Length)];
		foreach (GameObject player in players) {
			if (player.GetComponent<Player_Script> ().playerPersonalID == 0) {
				if (randomPiece == player.GetComponent<Player_Script> ().playerContract) {
					GetPlayerContract ();
					return;
				} 
			}
			if (player.GetComponent<Player_Script> ().playerPersonalID != 0) {
				if (randomPiece == player.GetComponent<Player_Script> ().playerLifeline) {
					GetPlayerContract ();
					return;
				} 
			}
		}
		if (randomPiece == playerLifeline) {
			GetPlayerContract ();
			return;
		}
		playerPersonalContract = randomPiece;

		CmdSetPlayerContract (playerPersonalContract);
		SetPlayerContractUI (playerPersonalContract);
	}

	[Command]
	void CmdSetPlayerContract(string newContract){
		playerContract = newContract;
	}

	void SetPlayerContractUI (string newContract){
		contractIcon = GameObject.FindGameObjectWithTag ("ContractIcon");
		contractNumber = GameObject.FindGameObjectWithTag ("ContractNumber");
		contractIconInGame = GameObject.FindGameObjectWithTag ("ContractIconInGame");
		contractNumberInGame = GameObject.FindGameObjectWithTag ("ContractNumberInGame");
		if (newContract == "p") {
			contractIcon.GetComponent<Text> ().text = "o";
			contractNumber.GetComponent<Text> ().text = "";
			contractIconInGame.GetComponent<Text> ().text = "o";
			contractNumberInGame.GetComponent<Text> ().text = "";
		}
		if (newContract == "q") {
			contractIcon.GetComponent<Text> ().text = "w";
			contractNumber.GetComponent<Text> ().text = "";
			contractIconInGame.GetComponent<Text> ().text = "w";
			contractNumberInGame.GetComponent<Text> ().text = "";
		}
		if (newContract == "k1") {
			contractIcon.GetComponent<Text> ().text = "j";
			contractNumber.GetComponent<Text> ().text = "1";
			contractIconInGame.GetComponent<Text> ().text = "j";
			contractNumberInGame.GetComponent<Text> ().text = "1";
		}
		if (newContract == "k2") {
			contractIcon.GetComponent<Text> ().text = "j";
			contractNumber.GetComponent<Text> ().text = "2";
			contractIconInGame.GetComponent<Text> ().text = "j";
			contractNumberInGame.GetComponent<Text> ().text = "2";
		}
		if (newContract == "b1") {
			contractIcon.GetComponent<Text> ().text = "n";
			contractNumber.GetComponent<Text> ().text = "1";
			contractIconInGame.GetComponent<Text> ().text = "n";
			contractNumberInGame.GetComponent<Text> ().text = "1";
		}
		if (newContract == "b2") {
			contractIcon.GetComponent<Text> ().text = "n";
			contractNumber.GetComponent<Text> ().text = "2";
			contractIconInGame.GetComponent<Text> ().text = "n";
			contractNumberInGame.GetComponent<Text> ().text = "2";
		}
		if (newContract == "r1") {
			contractIcon.GetComponent<Text> ().text = "t";
			contractNumber.GetComponent<Text> ().text = "1";
			contractIconInGame.GetComponent<Text> ().text = "t";
			contractNumberInGame.GetComponent<Text> ().text = "1";
		}
		if (newContract == "r2") {
			contractIcon.GetComponent<Text> ().text = "t";
			contractNumber.GetComponent<Text> ().text = "2";
			contractIconInGame.GetComponent<Text> ().text = "t";
			contractNumberInGame.GetComponent<Text> ().text = "2";
		}
	}

	//PUBLIC FUNCTION FOR READY BUTTON
	public void OnPlayerReadyUp () {
		//players = GameObject.FindGameObjectsWithTag ("Player");
		CmdPlayerReady ();
		//ToggleReadyButton (playerPersonalID);
	}

	//CMD FOR PLAYERREADY
	[Command] void CmdPlayerReady () {
		playerReady = 1;
	}

	public void OnPlayerSubmitNickname () {
		CmdSetNickname(nicknameInputText.text);
	}


	// Use this for initialization
	void Start () {

		nicknamePlaceholder = GameObject.FindGameObjectWithTag ("NicknamePlaceholder").GetComponent<Text> ();
		nicknameInputText = GameObject.FindGameObjectWithTag ("NicknameInputField").GetComponent<Text> ();



		gameObject.transform.position = new Vector3 (25.2f, 10, -20);
		//transform.localEulerAngles = new Vector3 (60, 0, 0);
		if(isLocalPlayer){
			playerCamera.enabled = true;

		}
		else{
			playerCamera.enabled = false;
		}


	}


	
	// Update is called once per frame
	void Update () {

		players = GameObject.FindGameObjectsWithTag ("Player");
//		if (totalPlayersSaved == players.Length && savedPlayers == false) {
//			
//			savedPlayers = true;
//		}
		foreach (GameObject tempPlayer in players) {
			bool whiteTeamContains = false;
			bool blackTeamContains = false;

			if (whiteTeam.Contains (tempPlayer)) {
				whiteTeamContains = true;
			}
			if (blackTeam.Contains (tempPlayer)) {
				blackTeamContains = true;
			}

			if (tempPlayer.GetComponent<Player_Script> ().playerTeam == 1 && whiteTeamContains == false) {
				whiteTeam.Add (tempPlayer);
			}
			if (tempPlayer.GetComponent<Player_Script> ().playerTeam == 2 && blackTeamContains == false) {
				blackTeam.Add (tempPlayer);
			}

		}
				
		if (!isLocalPlayer){
			return;
		}

		if (setFirstName == false) {
			SetPlayerFirstName ();
			setFirstName = true;
		}



		nicknamePlaceholder.text = playerNickname;



			 x = Input.GetAxis ("Horizontal") * 0.3f;

			 z = Input.GetAxis ("Vertical") * 0.3f;

		if (transform.position.x >= -3 && transform.position.x <= 36) {
			if (transform.position.z >= -23 && transform.position.z <= 3) {
			
				transform.Translate (x, 0, z);
			}
		}
		if (transform.position.x < -3) {
			transform.position = new Vector3 (-3, 10, transform.position.z);
		}
		if (transform.position.x > 36) {
			transform.position = new Vector3 (36, 10, transform.position.z);
		}
		if (transform.position.z < -23) {
			transform.position = new Vector3 (transform.position.x, 10, -23);
		}
		if (transform.position.z > 3) {
			transform.position = new Vector3 (transform.position.x, 10, 3);
		}
	
	}
}
