using UnityEngine;
using System.Collections;

public class Player_Position : MonoBehaviour {

	public float playerXPosition = -5.5f;
	public float playerZPosition = 5.5f;

	public Vector3 playerPosition;

	public GameObject knightModel;

	public GameObject otherPiece;
	public Vector3 otherPiecePosition;

	public bool playerNorth = false;
	public bool playerEast = false;
	public bool playerSouth = false;
	public bool playerWest = false;

	public bool frontLeftSelected = false;
	public bool frontRightSelected = false;
	public bool rightFrontSelected = false;
	public bool rightBackSelected = false;
	public bool backRightSelected = false;
	public bool backLeftSelected = false;
	public bool leftBackSelected = false;
	public bool leftFrontSelected = false;


	public float waitTime = 1;
	public bool ableToMove = false;

	public float timeSinceMove = 0;

	public GameObject[] pawns;
	public Vector3[] pawnLocations;

	public float pawnsCaptured = 0;


	// Use this for initialization
	void Start () {

		//playerDirections = 0,1,2,3;

		playerPosition = new Vector3 (playerXPosition, 0.2f, playerZPosition);

		gameObject.transform.position = playerPosition;


	
	}

//	void OnCollisionEnter (Collider other) {
//
//		if (other.gameObject.tag == "Piece") {
//			var piece = other.gameObject.GetComponent<Pawn_Position>;
//			if (piece){
//				if (timeSinceMove < piece.timeSinceMove){
//				}
//			}
//
//	}
	

	// Update is called once per frame
	void Update () {

		//GameObject otherPiece = GameObject.FindWithTag ("Piece");
		//otherPiecePosition = otherPiece.transform.position;

//		if (gameObject.tag.transform.position == Other.transform.position) {
//			int otherTimeSinceMove = Other.timeSinceMove;
//
//			if (timeSinceMove < otherTimeSinceMove) {
//				Destroy(Other);
//			} else {
//				Destroy(gameObject);
//			}
//		}

		pawns = GameObject.FindGameObjectsWithTag ("Pawn");
	
		foreach (GameObject pawn in pawns) {
			float pawnPositionX = pawn.transform.position.x;
			float pawnPositionZ = pawn.transform.position.z;
			float pawnTimeSinceMove = pawn.GetComponent<Pawn_Position> ().timeSinceMove;
			if ((pawnPositionX == playerXPosition) && (pawnPositionZ == playerZPosition)) {
				if (timeSinceMove < pawnTimeSinceMove) {
					Destroy (pawn);
					pawnsCaptured += 1;
				}
				if (timeSinceMove > pawnTimeSinceMove) {
					Destroy (gameObject);
				}
			}
		}


//		if (playerNorth == true) {
//			knightModel.gameObject.transform.localEulerAngles = new Vector3 (0, 90, 0);
//			playerNorth = false;
//		}
//		if (playerSouth == true) {
//			knightModel.gameObject.transform.localEulerAngles = new Vector3 (0, 270, 0);
//			playerSouth = false;
//		}
//		if (playerWest == true) {
//			knightModel.gameObject.transform.localEulerAngles = new Vector3 (0, 0, 0);
//			playerWest = false;
//		}
//		if (playerEast == true) {
//			knightModel.gameObject.transform.localEulerAngles = new Vector3 (0, 180, 0);
//			playerEast = false;
//		}

		playerXPosition = gameObject.transform.position.x;
		playerZPosition = gameObject.transform.position.z;

		//playerDirection = Mathf.Abs((gameObject.transform.rotation.y / 90f) - 4);
		if (waitTime > 0) {

			waitTime = waitTime - Time.deltaTime;
			ableToMove = false;
		}

		if (waitTime < 0) {
			
			ableToMove = true;

		}

		timeSinceMove += Time.deltaTime;


		if (Input.GetKey (KeyCode.A)) {
			knightModel.gameObject.transform.Rotate (0, 300 * Time.deltaTime, 0);
		}
		if (Input.GetKey (KeyCode.D)) {
			knightModel.gameObject.transform.Rotate (0, -300 * Time.deltaTime, 0);
		}
//		if (Input.GetKey (KeyCode.W)) {
//			knightModel.gameObject.transform.Rotate ( 0, 0, 100 * Time.deltaTime);
//		}
//		if (Input.GetKey (KeyCode.S)) {
//			knightModel.gameObject.transform.Rotate (0, 0,  -100 * Time.deltaTime);
//		}
//	
	}
}
