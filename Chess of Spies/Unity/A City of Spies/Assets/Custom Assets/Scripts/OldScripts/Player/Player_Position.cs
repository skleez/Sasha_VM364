﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_Position : NetworkBehaviour {

	public float playerXPosition = -5.5f;
	public float playerZPosition = 5.5f;
	public float playerYPosition = 0.2f;

	public Vector3 playerPosition;

	public GameObject knightModel;
	public GameObject cameraHinge;



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

	public bool onStreetSquare = false;
	public bool onEntranceSquare = false;

	public float[,] streetSquares = new float[,] {
		{-5.5f,5.5f},{-4.5f,5.5f},{-3.5f,5.5f},{-2.5f,5.5f},{-1.5f,5.5f},{-0.5f,5.5f},{0.5f,5.5f},{1.5f,5.5f},{2.5f,5.5f},{3.5f,5.5f},{4.5f,5.5f},{5.5f,5.5f},{6.5f,5.5f},{7.5f,5.5f},
		{8.5f,5.5f},{9.5f,5.5f},{10.5f,5.5f},{11.5f,5.5f},{12.5f,5.5f},{13.5f,5.5f},{14.5f,5.5f},{15.5f,5.5f},{16.5f,5.5f},{17.5f,5.5f},{18.5f,5.5f},{19.5f,5.5f},{20.5f,5.5f},{21.5f,5.5f},{22.5f,5.5f},{23.5f,5.5f},
		{-5.5f,4.5f},{-4.5f,4.5f},{-3.5f,4.5f},{-2.5f,4.5f},{-1.5f,4.5f},{-0.5f,4.5f},{0.5f,4.5f},{1.5f,4.5f},{2.5f,4.5f},{3.5f,4.5f},{4.5f,4.5f},{5.5f,4.5f},{6.5f,4.5f},{7.5f,4.5f},
		{8.5f,4.5f},{9.5f,4.5f},{10.5f,4.5f},{11.5f,4.5f},{12.5f,4.5f},{13.5f,4.5f},{14.5f,4.5f},{15.5f,4.5f},{16.5f,4.5f},{17.5f,4.5f},{18.5f,4.5f},{19.5f,4.5f},{20.5f,4.5f},{21.5f,4.5f},{22.5f,4.5f},{23.5f,4.5f},
		{-5.5f,3.5f},{-4.5f,3.5f},{-3.5f,3.5f},{-2.5f,3.5f},{-1.5f,3.5f},{-0.5f,3.5f},{0.5f,3.5f},{1.5f,3.5f},{2.5f,3.5f},{3.5f,3.5f},{4.5f,3.5f},{5.5f,3.5f},{6.5f,3.5f},{7.5f,3.5f},
		{8.5f,3.5f},{9.5f,3.5f},{10.5f,3.5f},{11.5f,3.5f},{12.5f,3.5f},{13.5f,3.5f},{14.5f,3.5f},{15.5f,3.5f},{16.5f,3.5f},{17.5f,3.5f},{18.5f,3.5f},{19.5f,3.5f},{20.5f,3.5f},{21.5f,3.5f},{22.5f,3.5f},{23.5f,3.5f},
		{-5.5f,2.5f},{-4.5f,2.5f},{-3.5f,2.5f},{-2.5f,2.5f},{-1.5f,2.5f},{-0.5f,2.5f},{0.5f,2.5f},{1.5f,2.5f},{2.5f,2.5f},{3.5f,2.5f},{4.5f,2.5f},{5.5f,2.5f},{6.5f,2.5f},{7.5f,2.5f},
		{8.5f,2.5f},{9.5f,2.5f},{10.5f,2.5f},{11.5f,2.5f},{12.5f,2.5f},{13.5f,2.5f},{14.5f,2.5f},{15.5f,2.5f},{16.5f,2.5f},{17.5f,2.5f},{18.5f,2.5f},{19.5f,2.5f},{20.5f,2.5f},{21.5f,2.5f},{22.5f,2.5f},{23.5f,2.5f},
		{20.5f,1.5f},{21.5f,1.5f},{22.5f,1.5f},{23.5f,1.5f},{24.5f,1.5f},{25.5f,1.5f},{26.5f,1.5f},{27.5f,1.5f},{28.5f,1.5f},{29.5f,1.5f},{30.5f,1.5f},{31.5f,1.5f},
		{20.5f,0.5f},{21.5f,0.5f},{22.5f,0.5f},{23.5f,0.5f},{24.5f,0.5f},{25.5f,0.5f},{26.5f,0.5f},{27.5f,0.5f},{28.5f,0.5f},{29.5f,0.5f},{30.5f,0.5f},{31.5f,0.5f},
		{20.5f,-0.5f},{21.5f,-0.5f},{22.5f,-0.5f},{23.5f,-0.5f},{24.5f,-0.5f},{25.5f,-0.5f},{26.5f,-0.5f},{27.5f,-0.5f},{28.5f,-0.5f},{29.5f,-0.5f},{30.5f,-0.5f},{31.5f,-0.5f},
		{20.5f,-1.5f},{21.5f,-1.5f},{22.5f,-1.5f},{23.5f,-1.5f},{24.5f,-1.5f},{25.5f,-1.5f},{26.5f,-1.5f},{27.5f,-1.5f},{28.5f,-1.5f},{29.5f,-1.5f},{30.5f,-1.5f},{31.5f,-1.5f},
		{20.5f,-2.5f},{21.5f,-2.5f},{22.5f,-2.5f},{23.5f,-2.5f},{24.5f,-2.5f},{25.5f,-2.5f},{26.5f,-2.5f},{27.5f,-2.5f},{28.5f,-2.5f},{29.5f,-2.5f},{30.5f,-2.5f},{31.5f,-2.5f},
		{20.5f,-3.5f},{21.5f,-3.5f},{22.5f,-3.5f},{23.5f,-3.5f},{24.5f,-3.5f},{25.5f,-3.5f},{26.5f,-3.5f},{27.5f,-3.5f},{28.5f,-3.5f},{29.5f,-3.5f},{30.5f,-3.5f},{31.5f,-3.5f},
		{20.5f,-4.5f},{21.5f,-4.5f},{22.5f,-4.5f},{23.5f,-4.5f},{24.5f,-4.5f},{25.5f,-4.5f},{26.5f,-4.5f},{27.5f,-4.5f},{28.5f,-4.5f},{29.5f,-4.5f},{30.5f,-4.5f},{31.5f,-4.5f},
		{20.5f,-5.5f},{21.5f,-5.5f},{22.5f,-5.5f},{23.5f,-5.5f},{24.5f,-5.5f},{25.5f,-5.5f},{26.5f,-5.5f},{27.5f,-5.5f},{28.5f,-5.5f},{29.5f,-5.5f},{30.5f,-5.5f},{31.5f,-5.5f},
		{20.5f,-6.5f},{21.5f,-6.5f},{22.5f,-6.5f},{23.5f,-6.5f},{24.5f,-6.5f},{25.5f,-6.5f},{26.5f,-6.5f},{27.5f,-6.5f},{28.5f,-6.5f},{29.5f,-6.5f},{30.5f,-6.5f},{31.5f,-6.5f},
		{20.5f,-7.5f},{21.5f,-7.5f},{22.5f,-7.5f},{23.5f,-7.5f},{24.5f,-7.5f},{25.5f,-7.5f},{26.5f,-7.5f},{27.5f,-7.5f},{28.5f,-7.5f},{29.5f,-7.5f},{30.5f,-7.5f},{31.5f,-7.5f}};

	public float[,] sidewalkSquares = new float[,] {{-7.5f,7.5f},{-6.5f,7.5f},{-5.5f,7.5f},{-4.5f,7.5f},{-3.5f,7.5f},{-2.5f,7.5f},{-1.5f,7.5f},{-0.5f,7.5f},
		{0.5f,7.5f},{1.5f,7.5f},{2.5f,7.5f},{3.5f,7.5f},{4.5f,7.5f},{5.5f,7.5f},{6.5f,7.5f},{7.5f,7.5f},
		{8.5f,7.5f},{9.5f,7.5f},{10.5f,7.5f},{11.5f,7.5f},{12.5f,7.5f},{13.5f,7.5f},{14.5f,7.5f},{15.5f,7.5f},
		{16.5f,7.5f},{17.5f,7.5f},{18.5f,7.5f},{19.5f,7.5f},{20.5f,7.5f},{21.5f,7.5f},{22.5f,7.5f},{23.5f,7.5f},
		{-7.5f,6.5f},{-6.5f,6.5f},{-5.5f,6.5f},{-4.5f,6.5f},{-3.5f,6.5f},{-2.5f,6.5f},{-1.5f,6.5f},{-0.5f,6.5f},
		{0.5f,6.5f},{1.5f,6.5f},{2.5f,6.5f},{3.5f,6.5f},{4.5f,6.5f},{5.5f,6.5f},{6.5f,6.5f},{7.5f,6.5f},
		{8.5f,6.5f},{9.5f,6.5f},{10.5f,6.5f},{11.5f,6.5f},{12.5f,6.5f},{13.5f,6.5f},{14.5f,6.5f},{15.5f,6.5f},
		{16.5f,6.5f},{17.5f,6.5f},{18.5f,6.5f},{19.5f,6.5f},{20.5f,6.5f},{21.5f,6.5f},{22.5f,6.5f},{23.5f,6.5f},
		{-7.5f,5.5f},{-6.5f,5.5f},{-7.5f,4.5f},{-6.5f,4.5f},{-7.5f,3.5f},{-6.5f,3.5f},{-7.5f,2.5f},{-6.5f,2.5f},
		{-7.5f,1.5f},{-6.5f,1.5f},{2.5f,1.5f},{3.5f,1.5f},{4.5f,1.5f},{5.5f,1.5f},{6.5f,1.5f},{7.5f,1.5f},
		{8.5f,1.5f},{9.5f,1.5f},{10.5f,1.5f},{11.5f,1.5f},{12.5f,1.5f},{13.5f,1.5f},{14.5f,1.5f},{15.5f,1.5f},
		{16.5f,1.5f},{17.5f,1.5f},{-7.5f,0.5f},{-6.5f,0.5f},{2.5f,0.5f},{3.5f,0.5f},{4.5f,0.5f},{5.5f,0.5f},{6.5f,0.5f},{7.5f,0.5f},
		{8.5f,0.5f},{9.5f,0.5f},{10.5f,0.5f},{11.5f,0.5f},{12.5f,0.5f},{13.5f,0.5f},{14.5f,0.5f},{15.5f,0.5f},
		{16.5f,0.5f},{17.5f,0.5f}};


	public float[,] entranceSquares = new float[,] {{18.5f,-2.5f},{19.5f,-2.5f},{18.5f,-3.5f},{19.5f,-3.5f},{18.5f,-4.5f},{19.5f,-4.5f},{18.5f,-5.5f},{19.5f,-5.5f},};

	//public float[,] missionGetSquare = new float[,] {{18.5f,-5.5f},{18.5f,-4.5f}};

//	public float sidewalkOneX;
//	public float sidewalkOneZ;

	public float waitTime = 1;
	public bool ableToMove = false;

	public float timeSinceMove = 0;

	public GameObject[] pawns;
	public Vector3[] pawnLocations;

	public float pawnsCaptured = 0;


	// Use this for initialization
	void Start () {


		playerPosition = new Vector3 (playerXPosition, playerYPosition, playerZPosition);

		gameObject.transform.position = playerPosition;


	
	}


	

	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer)
		{
			// exit from update if this is not the local player
			return;
		}




//		sidewalkOneX = sidewalkSquares [0, 0];
//		sidewalkOneZ = sidewalkSquares [0, 1];

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
		for (int x = 0; x < streetSquares.GetLength (0); x += 1) {
			
			
			float streetX = streetSquares [x, 0];
			float streetZ = streetSquares [x, 1];
			

			if ((streetX == playerXPosition) && (streetZ == playerZPosition)) {
				onStreetSquare = true;
				onEntranceSquare = false;

			} 
			
		}
		for (int x = 0; x < entranceSquares.GetLength (0); x += 1) {
			
			
			float entranceX = entranceSquares [x, 0];
			float entranceZ = entranceSquares [x, 1];
			
			
			if ((entranceX == playerXPosition) && (entranceZ == playerZPosition)) {
				
				onEntranceSquare = true;
				onStreetSquare = false;
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
		playerPosition = new Vector3 (playerXPosition, playerYPosition, playerZPosition);

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
			knightModel.gameObject.transform.Rotate (0, -300 * Time.deltaTime, 0);
		}
		if (Input.GetKey (KeyCode.D)) {
			knightModel.gameObject.transform.Rotate (0, 300 * Time.deltaTime, 0);
		}
		if (Input.GetKey (KeyCode.W)) {
			cameraHinge.gameObject.transform.Rotate ( 0, 0, 100 * Time.deltaTime,Space.Self);
		}
		if (Input.GetKey (KeyCode.S)) {
			cameraHinge.gameObject.transform.Rotate (0, 0,  -100 * Time.deltaTime, Space.Self);
		}
	
	}
}
