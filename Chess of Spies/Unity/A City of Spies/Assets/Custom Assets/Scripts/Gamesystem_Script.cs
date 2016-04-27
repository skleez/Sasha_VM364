using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Gamesystem_Script : NetworkBehaviour {


	public GameObject[] players;
	public List<GameObject> activePlayers = new List<GameObject>();
	public bool randomPlayers = false;
	public bool assignTeams = false;
	public bool assignPlayerOne = false;
	public bool assignPlayerTwo = false;
	public bool assignPlayerThree = false;
	public bool assignPlayerFour = false;
	public bool assignPlayerFive = false;
	public bool assignPlayerSix = false;
	public bool assignPlayerSeven = false;
	public bool assignPlayerEight = false;

	[SyncVar]public string playerOneNickname;
	[SyncVar]public string playerTwoNickname;
	[SyncVar]public string playerThreeNickname;
	[SyncVar]public string playerFourNickname;
	[SyncVar]public string playerFiveNickname;
	[SyncVar]public string playerSixNickname;
	[SyncVar]public string playerSevenNickname;
	[SyncVar]public string playerEightNickname;

	[SyncVar] public int gameState; //0 = Lobby, 1 = Game
	[SyncVar] public int roundNumber;
	[SyncVar] public float roundTime;
	[SyncVar] public int turnNumber;

	[SyncVar]public int whiteScore;
	[SyncVar]public int blackScore;

	public bool activePlayerCheck;
	public GameObject activePlayer;
	public GameObject lastActivePlayer;

	public GameObject playerOneListing;
	public GameObject playerTwoListing;
	public GameObject playerThreeListing;
	public GameObject playerFourListing;
	public GameObject playerFiveListing;
	public GameObject playerSixListing;
	public GameObject playerSevenListing;
	public GameObject playerEightListing;

	public List<GameObject> whiteTeam = new List<GameObject> ();

	public List<GameObject> blackTeam = new List<GameObject>();

	public Sprite white;
	public Sprite black;
	public Sprite grey;

	public Button playerReadyButton;
	public GameObject readyIndicatorOne;
	public GameObject readyIndicatorTwo;
	public GameObject readyIndicatorThree;
	public GameObject readyIndicatorFour;
	public GameObject readyIndicatorFive;
	public GameObject readyIndicatorSix;
	public GameObject readyIndicatorSeven;
	public GameObject readyIndicatorEight;


	public Text roundText;
	public Text roundTimer;
	public Text whitePointsLobby;
	public Text blackPointsLobby;
	public Text whitePointsInGame;
	public Text blackPointsInGame;
	public GameObject lobbyUi;

	public int whiteTeamSize;
	public int blackTeamSize;

	public GameObject[] aiPlayers;
	public List<GameObject> activeAiPlayers = new List<GameObject> ();

	public bool aiPlayerLifeline = false;
	public bool aiPlayerContract = false;

	public GameObject[] playersInRound;
	public GameObject playerTurnIndicator;

	public string[] piecesStrings = new string[] {"p","q","k1","k2","r1","r2","b1","b2"};

	public GameObject[] pieces;
	public GameObject pawn;
	public GameObject queen;
	public GameObject knightOne;
	public GameObject knightTwo;
	public GameObject bishopOne;
	public GameObject bishopTwo;
	public GameObject rookOne;
	public GameObject rookTwo;

	public GameObject[] MovementSquares;



	//RPC Calls


	//Round Number and Timer
	[ClientRpc] void RpcRoundText () {
		roundText.text = "Round 0" + roundNumber + " Starts in:";
	}
	[ClientRpc] void RpcRoundTimer () {
		roundTimer.text = (Mathf.RoundToInt (roundTime)).ToString ();
	}
	[ClientRpc] void RpcRoundScoreLobby () {
		whitePointsLobby.text = (whiteScore).ToString ();
		blackPointsLobby.text = (blackScore).ToString ();
	}
	[ClientRpc] void RpcRoundScoreInGame () {
		whitePointsInGame.text = (whiteScore).ToString ();
		whitePointsInGame.text = (blackScore).ToString ();
	}

	//PlayerJoining
	[ClientRpc] void RpcPlayerOneJoined (){
		playerOneListing.SetActive (true);
	}
	[ClientRpc] void RpcPlayerTwoJoined (){
		playerTwoListing.SetActive (true);
	}
	[ClientRpc] void RpcPlayerThreeJoined (){
		playerThreeListing.SetActive (true);
	}
	[ClientRpc] void RpcPlayerFourJoined (){
		playerFourListing.SetActive (true);
	}
	[ClientRpc] void RpcPlayerFiveJoined (){
		playerFiveListing.SetActive (true);
	}
	[ClientRpc] void RpcPlayerSixJoined (){
		playerThreeListing.SetActive (true);
	}
	[ClientRpc] void RpcPlayerSevenJoined (){
		playerFourListing.SetActive (true);
	}
	[ClientRpc] void RpcPlayerEightJoined (){
		playerFiveListing.SetActive (true);
	}

	[ClientRpc] void RpcPlayerOneNicknameUpdate (string tempPlayerNickname){
		playerOneListing.GetComponent<Text> ().text = tempPlayerNickname;
	}
	[ClientRpc] void RpcPlayerTwoNicknameUpdate (string tempPlayerNickname){
		playerTwoListing.GetComponent<Text> ().text = tempPlayerNickname;
	}
	[ClientRpc] void RpcPlayerThreeNicknameUpdate (string tempPlayerNickname){
		playerThreeListing.GetComponent<Text> ().text = tempPlayerNickname;
	}
	[ClientRpc] void RpcPlayerFourNicknameUpdate (string tempPlayerNickname){
		playerFourListing.GetComponent<Text> ().text = tempPlayerNickname;
	}
	[ClientRpc] void RpcPlayerFiveNicknameUpdate (string tempPlayerNickname){
		playerFiveListing.GetComponent<Text> ().text = tempPlayerNickname;
	}
	[ClientRpc] void RpcPlayerSixNicknameUpdate (string tempPlayerNickname){
		playerSixListing.GetComponent<Text> ().text = tempPlayerNickname;
	}
	[ClientRpc] void RpcPlayerSevenNicknameUpdate (string tempPlayerNickname){
		playerSevenListing.GetComponent<Text> ().text = tempPlayerNickname;
	}
	[ClientRpc] void RpcPlayerEightNicknameUpdate (string tempPlayerNickname){
		playerEightListing.GetComponent<Text> ().text = tempPlayerNickname;
	}

	void UpdatePlayerNickname (GameObject tempPlayer){
		if (tempPlayer.GetComponent<Player_Script> ().playerID == 1) {
			RpcPlayerOneNicknameUpdate (tempPlayer.GetComponent<Player_Script> ().playerNickname);
		}
		if (tempPlayer.GetComponent<Player_Script> ().playerID == 2) {
			RpcPlayerTwoNicknameUpdate (tempPlayer.GetComponent<Player_Script> ().playerNickname);
		}
		if (tempPlayer.GetComponent<Player_Script> ().playerID == 3) {
			RpcPlayerThreeNicknameUpdate (tempPlayer.GetComponent<Player_Script> ().playerNickname);
		}
		if (tempPlayer.GetComponent<Player_Script> ().playerID == 4) {
			RpcPlayerFourNicknameUpdate (tempPlayer.GetComponent<Player_Script> ().playerNickname);
		}
		if (tempPlayer.GetComponent<Player_Script> ().playerID == 5) {
			RpcPlayerFiveNicknameUpdate (tempPlayer.GetComponent<Player_Script> ().playerNickname);
		}
		if (tempPlayer.GetComponent<Player_Script> ().playerID == 6) {
			RpcPlayerSixNicknameUpdate (tempPlayer.GetComponent<Player_Script> ().playerNickname);
		}
		if (tempPlayer.GetComponent<Player_Script> ().playerID == 7) {
			RpcPlayerSevenNicknameUpdate (tempPlayer.GetComponent<Player_Script> ().playerNickname);
		}
		if (tempPlayer.GetComponent<Player_Script> ().playerID == 8) {
			RpcPlayerEightNicknameUpdate (tempPlayer.GetComponent<Player_Script> ().playerNickname);
		}
	}

	//Ready Indicators
	void SetIndicatorReady (int localNumber){
		switch (localNumber) {
		case 1:
			RpcPlayerOneReady ();
			break;
		case 2: 
			RpcPlayerTwoReady ();
			break;
		case 3:
			RpcPlayerThreeReady ();
			break;
		case 4: 
			RpcPlayerFourReady ();
			break;
		case 5:
			RpcPlayerFiveReady ();
			break;
		case 6: 
			RpcPlayerSixReady ();
			break;
		case 7:
			RpcPlayerSevenReady ();
			break;
		case 8: 
			RpcPlayerEightReady ();
			break;
		}
	}

	[ClientRpc] void RpcPlayerOneReady () {
		readyIndicatorOne.GetComponent<Image> ().sprite = white;
	}
	[ClientRpc] void RpcPlayerTwoReady () {
		readyIndicatorTwo.GetComponent<Image> ().sprite = white;
	}
	[ClientRpc] void RpcPlayerThreeReady () {
		readyIndicatorThree.GetComponent<Image> ().sprite = white;
	}
	[ClientRpc] void RpcPlayerFourReady () {
		readyIndicatorFour.GetComponent<Image> ().sprite = white;
	}
	[ClientRpc] void RpcPlayerFiveReady () {
		readyIndicatorFive.GetComponent<Image> ().sprite = white;
	}
	[ClientRpc] void RpcPlayerSixReady () {
		readyIndicatorSix.GetComponent<Image> ().sprite = white;
	}
	[ClientRpc] void RpcPlayerSevenReady () {
		readyIndicatorSeven.GetComponent<Image> ().sprite = white;
	}
	[ClientRpc] void RpcPlayerEightReady () {
		readyIndicatorEight.GetComponent<Image> ().sprite = white;
	}
	[ClientRpc] void RpcResetAllPlayerReady (){
		readyIndicatorOne.GetComponent<Image> ().sprite = black;
		readyIndicatorTwo.GetComponent<Image> ().sprite = black;
		readyIndicatorThree.GetComponent<Image> ().sprite = black;
		readyIndicatorFour.GetComponent<Image> ().sprite = black;
		readyIndicatorFive.GetComponent<Image> ().sprite = black;
		readyIndicatorSix.GetComponent<Image> ().sprite = black;
		readyIndicatorSeven.GetComponent<Image> ().sprite = black;
		readyIndicatorEight.GetComponent<Image> ().sprite = black;

	}

	//Round Start
	void StartFirstRound () {
		RpcHideAllMovementSquares ();
		SetTeamSize ();
		SetUpAiPlayers ();
		RpcGetAllAiPlayerLifeline ();
		RpcGetAllAiPlayerContract ();
		RandomizePlayersTurnOrder ();
		RandomizePieces ();
		RpcLobbyUItoGame ();
		RpcTurnNumberStart ();
		RpcRoundScoreInGame ();
		FindPlayerinOrder ();
	}

	void StartNextRound () {
		RandomizePlayersTurnOrder ();
		ResetAllPlacedPieces ();
		RandomizePieces ();
		RpcLobbyUItoGame ();
		RpcTurnNumberStart ();
		RpcRoundScoreInGame ();
		FindPlayerinOrder ();

	}
	[ClientRpc] void RpcHideAllMovementSquares (){
		MovementSquares = GameObject.FindGameObjectsWithTag ("MovementSquares");
		foreach (GameObject movementSquare in MovementSquares) {
			movementSquare.SetActive (false);
		}
	}

	void SetTeamSize (){
		whiteTeamSize = Mathf.RoundToInt (8 / 2);
		blackTeamSize = Mathf.FloorToInt (8 / 2);
		foreach (GameObject player in players) {
			AddPlayerToTeam (player);
		}
	}

	//ADDS PLAYERS TO GAMEOBJECT TEAM <LISTS>
	void AddPlayerToTeam (GameObject tempPlayer){
		if (tempPlayer.GetComponent<Player_Script> ().playerTeam == 1) {
			if (whiteTeam.Contains (tempPlayer)) {
				
			} else {
				whiteTeam.Add (tempPlayer);
			}
		}
		if (tempPlayer.GetComponent<Player_Script> ().playerTeam == 2) {
			if (blackTeam.Contains (tempPlayer)) {

			} else {
				blackTeam.Add (tempPlayer);
			}
		}
	}

	//SET UP AI PLAYERS


	void RandomizePlayersTurnOrder () {
		
		for (int t = 0; t < players.Length; t++) {
			GameObject tmp = players [t];
			int r = Random.Range (t, players.Length);
			players [t] = players [r];
			players [r] = tmp;
			RpcSetPlayerTurnPosition (r);
		}
//		for (int t = 0; t < activeAiPlayers.Count; t++) {
//			GameObject tmp = activeAiPlayers[t];
//			int r = Random.Range (t, activeAiPlayers.Count);
//			activeAiPlayers [t] = activeAiPlayers [r];
//			activeAiPlayers [r] = tmp;
//			RpcSetAIPlayerTurnPosition (r);
//			randomPlayers = true;
//		}
	}

	[ClientRpc] void RpcSetPlayerTurnPosition (int newPosition){
		players[newPosition].GetComponent<Player_Script>().playerTurnPosition = newPosition + 1;
	}
	[ClientRpc] void RpcSetAIPlayerTurnPosition (int newPosition){
		activeAiPlayers[newPosition].GetComponent<AIPlayer_Script>().aiPlayerTurnPosition = newPosition + 1;
	}

	//MOVE ALL PIECES TO RANDOM LOCATIONS
	void RandomizePieces () {
		SetPieceRandomLocation (pawn);
		SetPieceRandomLocation (queen);
		SetPieceRandomLocation (rookOne);
		SetPieceRandomLocation (rookTwo);
		SetPieceRandomLocation (bishopOne);
		SetPieceRandomLocation (bishopTwo);
		SetPieceRandomLocation (knightOne);
		SetPieceRandomLocation (knightTwo);
	}

	void SetPieceRandomLocation(GameObject newPiece){
		if (newPiece.GetComponent<Piece_Script> ().placed == false) {
			int tempRandomX = Random.Range (-5, 38);
			int tempRandomZ = Random.Range (-22, 6);

			if (tempRandomX >= 2 && tempRandomX <= 19) {
				if (tempRandomZ >= -16 && tempRandomZ <= 1) {
					SetPieceRandomLocation (newPiece);
					return;
				}
			}
			if (tempRandomX >= 32 && tempRandomX <= 37) {
				if (tempRandomZ >= -10 && tempRandomZ <= 2) {
					SetPieceRandomLocation (newPiece);
					return;
				}
			}

			pieces = GameObject.FindGameObjectsWithTag ("Piece");

			foreach (GameObject piece in pieces) {
				if (piece.GetComponent<Piece_Script> ().placed == true) {
					if (tempRandomX + 0.5f == piece.transform.position.x) {
						if (tempRandomZ + 0.5f == piece.transform.position.z) {
							SetPieceRandomLocation (newPiece);
							return;
						}
					}
				}
			}
			newPiece.transform.position = new Vector3 (tempRandomX + 0.5f, 0, tempRandomZ + 0.5f);
			newPiece.GetComponent<Piece_Script> ().placed = true;
		}
	
	}

	void ResetAllPlacedPieces () {
		foreach (GameObject tempPiece in pieces) {
			tempPiece.GetComponent<Piece_Script> ().placed = false;
		}
	}

	[ClientRpc] void RpcTurnNumberStart(){
		turnNumber = 1;
	}

	[ClientRpc] void RpcLobbyUItoGame (){
		gameState++;
		lobbyUi.SetActive (false);
	}
	[ClientRpc] void RpcGameUItoLobby (){
		gameState--;
		lobbyUi.SetActive (true);
	}
	[ClientRpc] void RpcResetLobbyTimer(){
		roundTime = 60;
	}

	void FindPlayerinOrder (){
		if (turnNumber > players.Length) {
			RpcTurnNumberStart ();
		}

		foreach (GameObject tempPlayer in players) {
			if (tempPlayer.GetComponent<Player_Script> ().playerTurnPosition == turnNumber) {
				if (tempPlayer.GetComponent<Player_Script> ().playerLifelineCaptured == 0) {
//					if (tempPlayer == lastActivePlayer) {
//						if (tempPlayer.GetComponent<Player_Script> ().playerTeam == 1) {
//							AddOneToWhiteScore ();
//							AddOneToWhiteScore ();
//							//INCREMENT ROUND COUNTER
//							RpcIncrementRoundNumber();
//							RpcResetAllPlayerReady ();
//							//RETURN TO MENU
//							RpcGameUItoLobby();
//							//RESET ALL PLAYER VALUES
//							ResetAllPlayerValues();
//						}
//						if (tempPlayer.GetComponent<Player_Script> ().playerTeam == 2) {
//							AddOneToBlackScore ();
//							AddOneToBlackScore ();
//							//INCREMENT ROUND COUNTER
//							RpcIncrementRoundNumber();
//							RpcResetAllPlayerReady ();
//							//RETURN TO MENU
//							RpcGameUItoLobby();
//							//RESET ALL PLAYER VALUES
//							ResetAllPlayerValues();
//						}
//					}
					PlayerTurnStart (tempPlayer);
				}
				if (tempPlayer.GetComponent<Player_Script> ().playerLifelineCaptured == 1) {
					ActivePlayerTurnComplete();
				}
			}
		}

//		for (int x = 0; x < players.Length; x++) {
//
//			//If it is the player's turn
//			if (players [x].GetComponent<Player_Script> ().playerTurnPosition == turnNumber) {
//				//If the Player's lifeline has not been captured
//				if (players [x].GetComponent<Player_Script> ().playerLifelineCaptured == 0) {
//					//If the player is the only player left
//					if (players [x] == lastActivePlayer && lastActivePlayer != null) {
//						if (players [x].GetComponent<Player_Script> ().playerTeam == 1) {
//							AddOneToWhiteScore ();
//							AddOneToWhiteScore ();
//							//INCREMENT ROUND COUNTER
//							RpcIncrementRoundNumber();
//							RpcResetAllPlayerReady ();
//							//RETURN TO MENU
//							RpcGameUItoLobby();
//							//RESET ALL PLAYER VALUES
//							ResetAllPlayerValues();
//						}
//						if (players [x].GetComponent<Player_Script> ().playerTeam == 2) {
//							AddOneToBlackScore ();
//							AddOneToBlackScore ();
//							//INCREMENT ROUND COUNTER
//							RpcIncrementRoundNumber();
//							RpcResetAllPlayerReady ();
//							//RETURN TO MENU
//							RpcGameUItoLobby();
//							//RESET ALL PLAYER VALUES
//							ResetAllPlayerValues();
//						}
//					}
//					//Start the player's turn
//					PlayerTurnStart(players[x]);
//					continue;
//				}
//				//If the player's lifeline has been captured
//				if (players [x].GetComponent<Player_Script> ().playerLifelineCaptured == 1) {
//					//Increment Turn#
//					ActivePlayerTurnComplete();
//					continue;
//				}
//			} 
//		}

	}

	void ResetAllPlayerValues (){
		foreach (GameObject tempPlayer in players) {
			if (isServer) {
				RpcResetPlayerValues (tempPlayer);
			}
			if (isClient == true && isServer == false) {
				CmdResetPlayerValues (tempPlayer);
				
			}
		}
	}

	[ClientRpc] void RpcResetPlayerValues(GameObject tempPlayer){
		
		tempPlayer.GetComponent<Player_Script> ().GetPlayerLifeline ();
		tempPlayer.GetComponent<Player_Script> ().GetPlayerContract ();
		tempPlayer.GetComponent<Player_Script> ().playerReady = 0;
		tempPlayer.GetComponent<Player_Script> ().playerTurnPosition = 0;
		tempPlayer.GetComponent<Player_Script> ().playerLifelineCaptured = 0;
		tempPlayer.GetComponent<Player_Script> ().playerTurnComplete = 0;
	}

	[Command] void CmdResetPlayerValues(GameObject tempPlayer){

		tempPlayer.GetComponent<Player_Script> ().GetPlayerLifeline ();
		tempPlayer.GetComponent<Player_Script> ().GetPlayerContract ();
		tempPlayer.GetComponent<Player_Script> ().playerReady = 0;
		tempPlayer.GetComponent<Player_Script> ().playerTurnPosition = 0;
		tempPlayer.GetComponent<Player_Script> ().playerLifelineCaptured = 0;
		tempPlayer.GetComponent<Player_Script> ().playerTurnComplete = 0;
	}

	[ClientRpc] void RpcIncrementRoundNumber (){
		roundNumber++;
	}

	void PlayerTurnStart (GameObject tempPlayer){
		if (isServer) {
			RpcPlayerTurnStart (tempPlayer);
		}
		if (isClient == true && isServer == false) {
			CmdPlayerTurnStart (tempPlayer);
		}
	}

	[ClientRpc] void RpcPlayerTurnStart (GameObject tempPlayer){
		activePlayer = tempPlayer;
		activePlayer.GetComponent<Player_Script> ().playerTurnStart = 1;
		activePlayer.GetComponent<Player_Script> ().playerTurnComplete = 0;
		activePlayer.GetComponent<Player_Script> ().SetPieceLocalCanvasCamera ();
		playerTurnIndicator.GetComponent<Text> ().text = "It is " + activePlayer.GetComponent<Player_Script>().playerNickname + "'s turn";
	}

	[Command] void CmdPlayerTurnStart(GameObject tempPlayer){
		activePlayer = tempPlayer;
		activePlayer.GetComponent<Player_Script> ().playerTurnStart = 1;
		activePlayer.GetComponent<Player_Script> ().playerTurnComplete = 0;
		activePlayer.GetComponent<Player_Script> ().SetPieceLocalCanvasCamera ();
		playerTurnIndicator.GetComponent<Text> ().text = "It is " + activePlayer.GetComponent<Player_Script>().playerNickname + "'s turn";
	}
		
	void CaptureCheck (GameObject movingPiece){
		foreach (GameObject targetPiece in pieces) {
			if (targetPiece != movingPiece) {
				if (movingPiece.transform.position == targetPiece.transform.position) {
					targetPiece.GetComponent<Piece_Script> ().captured = true;
					targetPiece.transform.Translate (0, 20, 0);
					if (targetPiece.name == "Pawn") {
						foreach (GameObject tempPlayer in activePlayers) {
							if (tempPlayer.GetComponent<Player_Script> ().playerLifeline == "p") {
								PlayerLifelineCaptured (tempPlayer);
							}
						}
					}
				}
			}
		}
	}

	void PlayerLifelineCaptured (GameObject tempPlayer){
		if (isServer == true) {
			RpcPlayerLifelineCaptured (tempPlayer);
		}
		if (isServer == false) {
			CmdPlayerLifelineCaptured (tempPlayer);
		}
	}

	[ClientRpc]void RpcPlayerLifelineCaptured (GameObject tempPlayer){
		tempPlayer.GetComponent<Player_Script> ().playerLifelineCaptured = 1;
	}
	[Command]void CmdPlayerLifelineCaptured (GameObject tempPlayer){
		tempPlayer.GetComponent<Player_Script> ().playerLifelineCaptured = 1;
	}

	void AddOneToWhiteScore (){
		if (isServer == true) {
			RpcAddOneToWhiteScore ();
		}
		if (isServer == false) {
			CmdAddOneToWhiteScore ();
		}
	}
	void AddOneToBlackScore (){
		if (isServer == true) {
			RpcAddOneToWhiteScore ();
		}
		if (isServer == false) {
			CmdAddOneToWhiteScore ();
		}
	}

	[ClientRpc] void RpcAddOneToWhiteScore (){
		whiteScore++;
	}
	[Command] void CmdAddOneToWhiteScore (){
		whiteScore++;
	}
	[ClientRpc] void RpcAddOneToBlackScore (){
		blackScore++;
	}
	[Command] void CmdAddOneToBlackScore (){
		blackScore++;
	}

	void ActivePlayerTurnComplete (){
		lastActivePlayer = activePlayer;
		if (isServer == true) {
			RpcActivePlayerTurnComplete ();
		}
		if (isServer == false) {
			CmdActivePlayerTurnComplete ();
		}
	}

	[ClientRpc] void RpcActivePlayerTurnComplete (){
		activePlayer.GetComponent<Player_Script> ().playerTurnComplete = 1;
		turnNumber++;
	}

	[Command] void CmdActivePlayerTurnComplete(){
		activePlayer.GetComponent<Player_Script> ().playerTurnComplete = 1;
		turnNumber++;
	}





	// Use this for initialization
	void Start () {

		roundNumber = 1;
		roundTime = 60;

	}


	
	// Update is called once per frame
	void Update () {

		if (isServer) {
			if (gameState == 0) {
				if (roundTime >= 0) {
					roundTime -= Time.deltaTime;

					//Check when players join

					players = GameObject.FindGameObjectsWithTag ("Player");

					if (players.Length >= 1) {
						RpcPlayerOneJoined ();
					}
					if (players.Length >= 2) {
						RpcPlayerTwoJoined ();
					}
					if (players.Length >= 3) {
						RpcPlayerThreeJoined ();
					}
					if (players.Length >= 4) {
						RpcPlayerFourJoined ();
					}
					if (players.Length >= 5) {
						RpcPlayerFiveJoined ();
					}
					if (players.Length >= 6) {
						RpcPlayerSixJoined ();
					}
					if (players.Length >= 7) {
						RpcPlayerSevenJoined ();
					}
					if (players.Length >= 8) {
						RpcPlayerEightJoined ();
					}
					RpcRoundText ();
					RpcRoundTimer ();
					RpcRoundScoreLobby ();
				}

				foreach (GameObject player in players) {
					UpdatePlayerNickname (player);
				
				}

				foreach (GameObject player in players) {
					if (player.GetComponent<Player_Script> ().playerReady == 1) {
						int readyIndicatorNumber = player.GetComponent<Player_Script> ().playerID;
						SetIndicatorReady (readyIndicatorNumber);
					}
				}
				foreach (GameObject player in players) {
					if (player.GetComponent<Player_Script> ().playerReady == 0) {
						return;
					}
				}
				if (roundNumber == 1) {
					StartFirstRound ();
				}
				if (roundNumber > 1) {
					StartNextRound ();
				}

			}
	
		}
			
	}
		



	//Public Functions
	public void OnPlayerReadyUp () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			if (player.GetComponent<Player_Script> ().playerPersonalID > 0) {
				player.GetComponent<Player_Script>().OnPlayerReadyUp();
			}
		}
	}
	public void OnPlayerNicknameUpdateSubmit () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			if (player.GetComponent<Player_Script> ().playerPersonalID > 0) {
				player.GetComponent<Player_Script>().OnPlayerSubmitNickname();
			}
		}
	}
//	public void OnPlayerHideLobbyInformationToggle (){
//		players = GameObject.FindGameObjectsWithTag ("Player");
//		foreach (GameObject player in players) {
//			if (player.GetComponent<Player_Script> ().playerPersonalID > 0) {
//				player.GetComponent<Player_Script>().OnPlayerHideLobbyInformation();
//			}
//		}
//	}

	public void OnPlayerClickPawn () {
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (true);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightTwo.GetComponent<Piece_Script> ().movementSquares.SetActive (false);

	}
	public void OnPlayerClickQueen () {
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (true);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightTwo.GetComponent<Piece_Script> ().movementSquares.SetActive (false);
	}
	public void OnPlayerClickRookOne () {
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (true);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightTwo.GetComponent<Piece_Script> ().movementSquares.SetActive (false);
	}
	public void OnPlayerClickRookTwo () {
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (true);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightTwo.GetComponent<Piece_Script> ().movementSquares.SetActive (false);
	}
	public void OnPlayerClickBishopOne () {
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (true);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightTwo.GetComponent<Piece_Script> ().movementSquares.SetActive (false);
	}
	public void OnPlayerClickBishopTwo () {
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (true);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightTwo.GetComponent<Piece_Script> ().movementSquares.SetActive (false);
	}
	public void OnPlayerClickKnightOne () {
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (true);
		knightTwo.GetComponent<Piece_Script> ().movementSquares.SetActive (false);	
	}
	public void OnPlayerClickKnightTwo () {
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		knightTwo.GetComponent<Piece_Script> ().movementSquares.SetActive (true);
	}

	//PAWN MOVEMENT
	public void OnPlayerClickPawnMovementEast (){
		pawn.transform.position = new Vector3 (pawn.transform.position.x + 1, pawn.transform.position.y, pawn.transform.position.z);
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (pawn);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickPawnMovementNorth (){
		pawn.transform.position = new Vector3 (pawn.transform.position.x, pawn.transform.position.y, pawn.transform.position.z + 1);
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (pawn);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickPawnMovementWest (){
		pawn.transform.position = new Vector3 (pawn.transform.position.x - 1, pawn.transform.position.y, pawn.transform.position.z);
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (pawn);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickPawnMovementSouth (){
		pawn.transform.position = new Vector3 (pawn.transform.position.x, pawn.transform.position.y, pawn.transform.position.z - 1);
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (pawn);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickPawnMovementNorthEast (){
		pawn.transform.position = new Vector3 (pawn.transform.position.x + 1, pawn.transform.position.y, pawn.transform.position.z + 1);
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (pawn);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickPawnMovementNorthWest (){
		pawn.transform.position = new Vector3 (pawn.transform.position.x - 1, pawn.transform.position.y, pawn.transform.position.z + 1);
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (pawn);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickPawnMovementSouthEast (){
		pawn.transform.position = new Vector3 (pawn.transform.position.x + 1, pawn.transform.position.y, pawn.transform.position.z - 1);
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (pawn);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickPawnMovementSouthWest (){
		pawn.transform.position = new Vector3 (pawn.transform.position.x - 1, pawn.transform.position.y, pawn.transform.position.z - 1);
		pawn.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (pawn);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}

	//KNIGHT ONE
	public void OnPlayerClickKnightOneMovementOne (){
		knightOne.transform.position = new Vector3 (knightOne.transform.position.x + 2, knightOne.transform.position.y, knightOne.transform.position.z + 1);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightOneMovementTwo (){
		knightOne.transform.position = new Vector3 (knightOne.transform.position.x + 1, knightOne.transform.position.y, knightOne.transform.position.z + 2);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightOneMovementThree (){
		knightOne.transform.position = new Vector3 (knightOne.transform.position.x - 1, knightOne.transform.position.y, knightOne.transform.position.z + 2);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightOneMovementFour (){
		knightOne.transform.position = new Vector3 (knightOne.transform.position.x - 2, knightOne.transform.position.y, knightOne.transform.position.z + 1);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightOneMovementFive (){
		knightOne.transform.position = new Vector3 (knightOne.transform.position.x - 2, knightOne.transform.position.y, knightOne.transform.position.z - 1);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightOneMovementSix (){
		knightOne.transform.position = new Vector3 (knightOne.transform.position.x - 1, knightOne.transform.position.y, knightOne.transform.position.z - 2);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightOneMovementSeven (){
		knightOne.transform.position = new Vector3 (knightOne.transform.position.x + 1, knightOne.transform.position.y, knightOne.transform.position.z - 2);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightOneMovementEight (){
		knightOne.transform.position = new Vector3 (knightOne.transform.position.x + 2, knightOne.transform.position.y, knightOne.transform.position.z - 1);
		knightOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}

	//KNIGHT TWO
	public void OnPlayerClickKnightTwoMovementOne (){
		knightTwo.transform.position = new Vector3 (knightTwo.transform.position.x + 2, knightTwo.transform.position.y, knightTwo.transform.position.z + 1);
		knightTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightTwoMovementTwo (){
		knightTwo.transform.position = new Vector3 (knightTwo.transform.position.x + 1, knightTwo.transform.position.y, knightTwo.transform.position.z + 2);
		knightTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightTwoMovementThree (){
		knightTwo.transform.position = new Vector3 (knightTwo.transform.position.x - 1, knightTwo.transform.position.y, knightTwo.transform.position.z + 2);
		knightTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightTwoMovementFour (){
		knightTwo.transform.position = new Vector3 (knightTwo.transform.position.x - 2, knightTwo.transform.position.y, knightTwo.transform.position.z + 1);
		knightTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightTwoMovementFive (){
		knightTwo.transform.position = new Vector3 (knightTwo.transform.position.x - 2, knightTwo.transform.position.y, knightTwo.transform.position.z - 1);
		knightTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightTwoMovementSix (){
		knightTwo.transform.position = new Vector3 (knightTwo.transform.position.x - 1, knightTwo.transform.position.y, knightTwo.transform.position.z - 2);
		knightTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightTwoMovementSeven (){
		knightTwo.transform.position = new Vector3 (knightTwo.transform.position.x + 1, knightTwo.transform.position.y, knightTwo.transform.position.z - 2);
		knightTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickKnightTwoMovementEight (){
		knightTwo.transform.position = new Vector3 (knightTwo.transform.position.x + 2, knightTwo.transform.position.y, knightTwo.transform.position.z - 1);
		knightTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (knightTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}

	//QUEEN
	public void OnPlayerClickQueenMovementEast (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 1, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementEastTwo (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 2, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementEastThree (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 3, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementEastFour (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 4, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementEastFive (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 5, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorth (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z + 1);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthTwo (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z + 2);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthThree (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z + 3);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthFour (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z + 4);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthFive (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z + 5);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementWest (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 1, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementWestTwo (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 2, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementWestThree (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 3, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementWestFour (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 4, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementWestFive (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 5, queen.transform.position.y, queen.transform.position.z);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouth (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z - 1);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthTwo (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z - 2);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthThree (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z - 3);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthFour (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z - 4);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthFive (){
		queen.transform.position = new Vector3 (queen.transform.position.x, queen.transform.position.y, queen.transform.position.z - 5);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthEast (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 1, queen.transform.position.y, queen.transform.position.z + 1);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthEastTwo (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 2, queen.transform.position.y, queen.transform.position.z + 2);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthEastThree (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 3, queen.transform.position.y, queen.transform.position.z + 3);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthEastFour (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 4, queen.transform.position.y, queen.transform.position.z + 4);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthEastFive (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 5, queen.transform.position.y, queen.transform.position.z + 5);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthWest (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 1, queen.transform.position.y, queen.transform.position.z + 1);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthWestTwo (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 2, queen.transform.position.y, queen.transform.position.z + 2);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthWestThree (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 3, queen.transform.position.y, queen.transform.position.z + 3);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthWestFour (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 4, queen.transform.position.y, queen.transform.position.z + 4);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementNorthWestFive (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 5, queen.transform.position.y, queen.transform.position.z + 5);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthEast (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 1, queen.transform.position.y, queen.transform.position.z - 1);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthEastTwo (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 2, queen.transform.position.y, queen.transform.position.z - 2);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthEastThree (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 3, queen.transform.position.y, queen.transform.position.z - 3);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthEastFour (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 4, queen.transform.position.y, queen.transform.position.z - 4);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthEastFive (){
		queen.transform.position = new Vector3 (queen.transform.position.x + 5, queen.transform.position.y, queen.transform.position.z - 5);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthWest (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 1, queen.transform.position.y, queen.transform.position.z - 1);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthWestTwo (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 2, queen.transform.position.y, queen.transform.position.z - 2);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthWestThree (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 3, queen.transform.position.y, queen.transform.position.z - 3);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthWestFour (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 4, queen.transform.position.y, queen.transform.position.z - 4);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickQueenMovementSouthWestFive (){
		queen.transform.position = new Vector3 (queen.transform.position.x - 5, queen.transform.position.y, queen.transform.position.z - 5);
		queen.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (queen);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}

	//ROOK ONE

	public void OnPlayerClickRookOneMovementEast (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x + 1, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementEastTwo (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x + 2, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementEastThree (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x + 3, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementEastFour (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x + 4, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementEastFive (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x + 5, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementNorth (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z + 1);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementNorthTwo (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z + 2);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementNorthThree (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z + 3);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementNorthFour (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z + 4);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementNorthFive (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z + 5);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementWest (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x - 1, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementWestTwo (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x - 2, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementWestThree (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x - 3, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementWestFour (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x - 4, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementWestFive (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x - 5, rookOne.transform.position.y, rookOne.transform.position.z);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementSouth (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z - 1);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementSouthTwo (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z - 2);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementSouthThree (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z - 3);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementSouthFour (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z - 4);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookOneMovementSouthFive (){
		rookOne.transform.position = new Vector3 (rookOne.transform.position.x, rookOne.transform.position.y, rookOne.transform.position.z - 5);
		rookOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}

	//ROOK TWO

	public void OnPlayerClickRookTwoMovementEast (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x + 1, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementEastTwo (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x + 2, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementEastThree (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x + 3, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementEastFour (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x + 4, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementEastFive (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x + 5, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementNorth (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z + 1);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementNorthTwo (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z + 2);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementNorthThree (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z + 3);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementNorthFour (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z + 4);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementNorthFive (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z + 5);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementWest (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x - 1, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementWestTwo (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x - 2, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementWestThree (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x - 3, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementWestFour (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x - 4, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementWestFive (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x - 5, rookTwo.transform.position.y, rookTwo.transform.position.z);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementSouth (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z - 1);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementSouthTwo (){
		rookTwo.transform.position = new Vector3 (rookOne.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z - 2);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementSouthThree (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z - 3);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementSouthFour (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z - 4);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickRookTwoMovementSouthFive (){
		rookTwo.transform.position = new Vector3 (rookTwo.transform.position.x, rookTwo.transform.position.y, rookTwo.transform.position.z - 5);
		rookTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (rookTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}

	//BISHOP ONE

	public void OnPlayerClickBishopOneMovementNorthEast (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 1, bishopOne.transform.position.y, bishopOne.transform.position.z + 1);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementNorthEastTwo (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 2, bishopOne.transform.position.y, bishopOne.transform.position.z + 2);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementNorthEastThree (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 3, bishopOne.transform.position.y, bishopOne.transform.position.z + 3);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementNorthEastFour (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 4, bishopOne.transform.position.y, bishopOne.transform.position.z + 4);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementNorthEastFive (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 5, bishopOne.transform.position.y, bishopOne.transform.position.z + 5);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementNorthWest (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 1, bishopOne.transform.position.y, bishopOne.transform.position.z + 1);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementNorthWestTwo (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 2, bishopOne.transform.position.y, bishopOne.transform.position.z + 2);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementNorthWestThree (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 3, bishopOne.transform.position.y, bishopOne.transform.position.z + 3);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementNorthWestFour (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 4, bishopOne.transform.position.y, bishopOne.transform.position.z + 4);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementNorthWestFive (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 5, bishopOne.transform.position.y, bishopOne.transform.position.z + 5);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthEast (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 1, bishopOne.transform.position.y, bishopOne.transform.position.z - 1);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthEastTwo (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 2, bishopOne.transform.position.y, bishopOne.transform.position.z - 2);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthEastThree (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 3, bishopOne.transform.position.y, bishopOne.transform.position.z - 3);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthEastFour (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 4, bishopOne.transform.position.y, bishopOne.transform.position.z - 4);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthEastFive (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x + 5, bishopOne.transform.position.y, bishopOne.transform.position.z - 5);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthWest (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 1, bishopOne.transform.position.y, bishopOne.transform.position.z - 1);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthWestTwo (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 2, bishopOne.transform.position.y, bishopOne.transform.position.z - 2);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthWestThree (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 3, bishopOne.transform.position.y, bishopOne.transform.position.z - 3);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthWestFour (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 4, bishopOne.transform.position.y, bishopOne.transform.position.z - 4);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopOneMovementSouthWestFive (){
		bishopOne.transform.position = new Vector3 (bishopOne.transform.position.x - 5, bishopOne.transform.position.y, bishopOne.transform.position.z - 5);
		bishopOne.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}

	//BISHOP TWO

	public void OnPlayerClickBishopTwoMovementNorthEast (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 1, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 1);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementNorthEastTwo (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 2, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 2);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementNorthEastThree (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 3, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 3);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementNorthEastFour (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 4, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 4);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementNorthEastFive (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 5, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 5);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementNorthWest (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 1, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 1);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementNorthWestTwo (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 2, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 2);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementNorthWestThree (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 3, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 3);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementNorthWestFour (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 4, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 4);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementNorthWestFive (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 5, bishopTwo.transform.position.y, bishopTwo.transform.position.z + 5);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopOne);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthEast (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 1, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 1);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthEastTwo (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 2, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 2);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthEastThree (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 3, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 3);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthEastFour (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 4, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 4);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthEastFive (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x + 5, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 5);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthWest (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 1, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 1);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthWestTwo (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 2, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 2);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthWestThree (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 3, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 3);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthWestFour (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 4, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 4);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}
	public void OnPlayerClickBishopTwoMovementSouthWestFive (){
		bishopTwo.transform.position = new Vector3 (bishopTwo.transform.position.x - 5, bishopTwo.transform.position.y, bishopTwo.transform.position.z - 5);
		bishopTwo.GetComponent<Piece_Script>().movementSquares.SetActive (false);
		CaptureCheck (bishopTwo);
		ActivePlayerTurnComplete ();
		FindPlayerinOrder ();
	}




//AI

void SetUpAiPlayers (){
	RpcSetAiPlayersId ();
	RpcSetAiPlayerTeams ();
}

//NUMBER OF AI PLAYERS
[ClientRpc] void RpcSetAiPlayersId (){
	aiPlayers = GameObject.FindGameObjectsWithTag ("AIPlayer");

	for (int i = 0; i < (8 - players.Length); i++){
		aiPlayers [i].GetComponent<AIPlayer_Script> ().aiPlayerID = i + 1;
		aiPlayers [i].GetComponent<AIPlayer_Script> ().aiPlayerTurnPosition = i + 1;
	}
	foreach (GameObject aiPlayer in aiPlayers) {
		if (activeAiPlayers.Contains (aiPlayer)) {
			continue;
		}
		if (aiPlayer.GetComponent<AIPlayer_Script> ().aiPlayerID != 0) {
			activeAiPlayers.Add (aiPlayer);
		}
	}
}

//ACTIVE AI PLAYER'S TEAMS
[ClientRpc] void RpcSetAiPlayerTeams (){
	foreach (GameObject aiPlayer in aiPlayers){
		if (aiPlayer.GetComponent<AIPlayer_Script> ().aiPlayerID != 0) {
			if (whiteTeam.Count + blackTeam.Count <= 4) {
				int randomTeam = Random.Range (1, 3);
				aiPlayer.GetComponent<AIPlayer_Script> ().aiPlayerTeam = randomTeam;
				AddAiPlayerToTeam (aiPlayer);
			}
			if (whiteTeam.Count + blackTeam.Count > 4 && blackTeam.Count < blackTeamSize ) {
				if (whiteTeam.Contains(aiPlayer)){
					continue;
				} if (blackTeam.Contains (aiPlayer)) {
					continue;
				} else {
					aiPlayer.GetComponent<AIPlayer_Script> ().aiPlayerTeam = 2;
					AddAiPlayerToTeam (aiPlayer);
				}
			}
			if (whiteTeam.Count + blackTeam.Count > 4 && whiteTeam.Count < whiteTeamSize) {
				if (blackTeam.Contains (aiPlayer)) {
					continue;
				} if (whiteTeam.Contains (aiPlayer)) {
					continue;
				}else {
					aiPlayer.GetComponent<AIPlayer_Script> ().aiPlayerTeam = 1;
					AddAiPlayerToTeam (aiPlayer);
				}
			}
		}
	}
}

//ADD AI PLAYER TO TEAM LIST
void AddAiPlayerToTeam (GameObject tempAiPlayer){

	if (tempAiPlayer.GetComponent<AIPlayer_Script> ().aiPlayerTeam == 1) {
		whiteTeam.Add (tempAiPlayer);
	}
	if (tempAiPlayer.GetComponent<AIPlayer_Script> ().aiPlayerTeam == 2) {
		blackTeam.Add (tempAiPlayer);
	}

}

[ClientRpc]void RpcGetAllAiPlayerLifeline () {
	foreach (GameObject aiPlayer in aiPlayers){
		aiPlayerLifeline = true;

		if (aiPlayer.GetComponent<AIPlayer_Script> ().aiPlayerID > 0) {

			GetAiPlayerLifeline (aiPlayer);
		}
	}
}

[ClientRpc]void RpcGetAllAiPlayerContract () {

	foreach (GameObject aiPlayer in aiPlayers){
		aiPlayerContract = true;

		if (aiPlayer.GetComponent<AIPlayer_Script> ().aiPlayerID > 0) {

			GetAiPlayerContract (aiPlayer);
		}
	}
}

void GetAiPlayerLifeline (GameObject tempAiPlayer){

	string randomPiece = piecesStrings[Random.Range (0, piecesStrings.Length)];

	foreach (GameObject player in whiteTeam) {
		if (player.gameObject.tag == "Player") {
			if (randomPiece == player.GetComponent<Player_Script> ().playerLifeline) {
				GetAiPlayerLifeline (tempAiPlayer);
				return;
			}
		}
		if (player.gameObject.tag == "AIPlayer") {
			if (randomPiece == player.GetComponent<AIPlayer_Script> ().aiPlayerLifeline) {
				GetAiPlayerLifeline (tempAiPlayer);
				return;
			}
		}
	}
	foreach (GameObject player in blackTeam) {
		if (player.gameObject.tag == "Player") {
			if (randomPiece == player.GetComponent<Player_Script> ().playerLifeline) {
				GetAiPlayerLifeline (tempAiPlayer);
				return;
			}
		}
		if (player.gameObject.tag == "AIPlayer") {
			if (randomPiece == player.GetComponent<AIPlayer_Script> ().aiPlayerLifeline) {
				GetAiPlayerLifeline (tempAiPlayer);
				return;
			}
		}
	}
	tempAiPlayer.GetComponent<AIPlayer_Script>().aiPlayerLifeline = randomPiece;

}

void GetAiPlayerContract (GameObject tempAiPlayer){
	string randomPiece = piecesStrings[Random.Range (0, piecesStrings.Length)];

	foreach (GameObject player in whiteTeam) {
		if (player.gameObject.tag == "Player") {
			if (randomPiece == player.GetComponent<Player_Script> ().playerContract) {
				GetAiPlayerContract (tempAiPlayer);
				return;
			}
		}
		if (player.gameObject.tag == "AIPlayer") {
			if (randomPiece == player.GetComponent<AIPlayer_Script> ().aiPlayerContract) {
				GetAiPlayerContract (tempAiPlayer);
				return;
			}
		}
	}
	foreach (GameObject player in blackTeam) {
		if (player.gameObject.tag == "Player") {
			if (randomPiece == player.GetComponent<Player_Script> ().playerContract) {
				GetAiPlayerContract (tempAiPlayer);
				return;
			}
		}
		if (player.gameObject.tag == "AIPlayer") {
			if (randomPiece == player.GetComponent<AIPlayer_Script> ().aiPlayerContract) {
				GetAiPlayerContract (tempAiPlayer);
				return;
			}
		}
	}
	tempAiPlayer.GetComponent<AIPlayer_Script>().aiPlayerContract = randomPiece;

}
	
void FindAIPlayerinOrder (){

	for (int x = 0; x < activeAiPlayers.Count; x++) {
		if (activeAiPlayers [x].GetComponent<AIPlayer_Script> ().aiPlayerTurnPosition == turnNumber - players.Length) {
			RpcAIPlayerTurnStart (activeAiPlayers [x]);
			return;
		}
	}
}



[ClientRpc] void RpcAIPlayerTurnStart (GameObject tempAiPlayer){
	activePlayer = tempAiPlayer;
	activePlayer.GetComponent<AIPlayer_Script> ().aiPlayerTurnStart = 1;
	activePlayer.GetComponent<AIPlayer_Script> ().aiPlayerTurnComplete = 0;
	//activePlayer.GetComponent<Player_Script> ().SetPieceLocalCanvasCamera ();
	playerTurnIndicator.GetComponent<Text> ().text = "It is " + activePlayer.GetComponent<AIPlayer_Script>().aiPlayerNickname + "'s turn";
}

[ClientRpc] void RpcActiveAIPlayerTurnComplete (){
	activePlayer.GetComponent<AIPlayer_Script> ().aiPlayerTurnComplete = 1;
	turnNumber++;
}

}
