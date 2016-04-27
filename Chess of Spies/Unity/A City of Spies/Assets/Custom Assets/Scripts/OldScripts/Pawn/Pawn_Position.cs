using UnityEngine;
using System.Collections;

public class Pawn_Position : MonoBehaviour {

	public bool frontSelected = false;
	public bool rightSelected = false;
	public bool leftSelected = false;


	public Vector3 pawnCurrentPosition;

	public Vector3 pawnTargetPosition = new Vector3 (0,0,0);
	public Vector3 pawnNextTargetPosition = new Vector3 (0,0,0);
	public Vector3 targetCoordinates1 = new Vector3 (0,0,0);
	public Vector3 targetCoordinates2 = new Vector3 (0,0,0);
	public Vector3 targetCoordinates3 = new Vector3 (0,0,0);
	public Vector3 targetCoordinates4 = new Vector3 (0,0,0);
	public Vector3 pawnNextCoordinates;

	public int pathStage = 1;

	public float xDistance;
	public bool xDirection;
	public float zDistance;
	public bool zDirection;
	//public Vector3 pawnPatrol

	public float waitTime = 5;
	public float timeAdded = 2;
	public bool ableToMove = false;

	public float timeSinceMove = 0;

	public bool nextMoveXPlus = false;
	public bool nextMoveXMinus = false;
	public bool nextMoveZPlus = false;
	public bool nextMoveZMinus = false;

	public Player_Position knightPosition;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Piece") {
			Destroy(other.gameObject);
		}
	}


	// Use this for initialization
	void Start () {

		pawnCurrentPosition = new Vector3 (pawnCurrentPosition.x, 0, pawnCurrentPosition.z);
		gameObject.transform.position = pawnCurrentPosition;
	
	}
	
	// Update is called once per frame
	void Update () {

		pawnCurrentPosition.x = gameObject.transform.position.x;
		pawnCurrentPosition.z = gameObject.transform.position.z;


		//Checking X Distance to target and Direction
		xDistance = Mathf.Abs (pawnCurrentPosition.x - pawnTargetPosition.x);

		if (pawnCurrentPosition.x > pawnTargetPosition.x) {
			xDirection = false;
		}

		if (pawnCurrentPosition.x < pawnTargetPosition.x) {
			xDirection = true;
		}

		//Checking Z Distance to target and Direction
		zDistance = Mathf.Abs (pawnCurrentPosition.z -pawnTargetPosition.z);

		if (pawnCurrentPosition.z > pawnTargetPosition.z) {
			zDirection = false;
		}

		if (pawnCurrentPosition.z < pawnTargetPosition.z) {
			zDirection = true;
		}
		//Checking largest distance, and defining move                        
		if (xDistance >= zDistance) {
			if (xDirection == true) {
				nextMoveXPlus = true;
			}
			if (xDirection == false) {
				nextMoveXMinus = true;
			}
		}
		if (xDistance < zDistance) {
			if (zDirection == true) {
				nextMoveZPlus = true;
			}
			if (zDirection == false) {
				nextMoveZMinus = true;
			}
		}



		if (xDistance == 0 && zDistance == 0) {
			pathStage += 1;
		}

		if (pathStage == 1) { 
			pawnTargetPosition = targetCoordinates1;
		}
		if (pathStage == 2) {
			pawnTargetPosition = targetCoordinates2;
		}
		if (pathStage == 3) {
			pawnTargetPosition = targetCoordinates3;
		}
		if (pathStage == 4) {
			pawnTargetPosition = targetCoordinates4;
		}
		if (pathStage == 5) {
			pathStage = 0;
		}

		if (waitTime > 0) {

			waitTime = waitTime - Time.deltaTime;
			ableToMove = false;

		}

		if (waitTime < 0) {

			ableToMove = true;
		}

		timeSinceMove += Time.deltaTime;

		if (ableToMove == true && nextMoveXPlus == true) {
			gameObject.transform.position = new Vector3 (pawnCurrentPosition.x + 1, 0, pawnCurrentPosition.z);
			nextMoveXPlus = false;
			waitTime += timeAdded;
			timeSinceMove = 0;
		}
		if (ableToMove == true && nextMoveXMinus == true) {
			gameObject.transform.position = new Vector3 (pawnCurrentPosition.x - 1, 0, pawnCurrentPosition.z);
			nextMoveXMinus = false;
			waitTime += timeAdded;
			timeSinceMove = 0;
		}
		if (ableToMove == true && nextMoveZPlus == true) {
			gameObject.transform.position = new Vector3 (pawnCurrentPosition.x, 0, pawnCurrentPosition.z + 1);
			nextMoveZPlus = false;
			waitTime += timeAdded;
			timeSinceMove = 0;
		}
		if (ableToMove == true && nextMoveZMinus == true) {
			gameObject.transform.position = new Vector3 (pawnCurrentPosition.x, 0, pawnCurrentPosition.z - 1);
			nextMoveZMinus = false;
			waitTime += timeAdded;
			timeSinceMove = 0;
		}

//		if ((knightPosition.playerXPosition == gameObject.transform.position.x) && (knightPosition.playerZPosition == gameObject.transform.position.z)) {
//			if (timeSinceMove < knightPosition.timeSinceMove) {
//				Destroy(knightPosition.gameObject);
//			}
//			if (timeSinceMove > knightPosition.timeSinceMove) {
//				Destroy(gameObject);
//				knightPosition.pawnsCaptured += 1; 
//			}
//		}
	
	}
}
