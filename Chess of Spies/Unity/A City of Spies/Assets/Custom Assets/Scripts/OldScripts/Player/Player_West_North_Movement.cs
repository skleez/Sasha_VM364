using UnityEngine;
using System.Collections;

public class Player_West_North_Movement : MonoBehaviour {


	public Material nextMoveMaterial;
	public GameObject player;
	public GameObject knightModel;
	public Player_Position playerPosition;

	public float movementXPosition;
	public float movementZPosition;


	// Use this for initialization
	void Start () {

		//Screen.lockCursor = true;
	
	}
	
	void OnMouseOver () {

		nextMoveMaterial.color = Color.blue;
	}

	void OnMouseExit () {
		nextMoveMaterial.color = Color.white;
	}
	void OnMouseDown () {
		playerPosition.frontLeftSelected = false;
		playerPosition.frontRightSelected = true;
		playerPosition.rightFrontSelected = false;
		playerPosition.rightBackSelected = false;
		playerPosition.backRightSelected = false;
		playerPosition.backLeftSelected = false;
		playerPosition.leftBackSelected = false;
		playerPosition.leftFrontSelected = false;
	}

	void OnMouseUp () {

	}
	
	// Update is called once per frame
	void Update () {

		movementXPosition = playerPosition.playerXPosition - 2f;
		movementZPosition = playerPosition.playerZPosition + 1f;

		for (int x = 0; x < playerPosition.sidewalkSquares.GetLength (0); x += 1) {
			
			
			float sidewalkX = playerPosition.sidewalkSquares [x, 0];
			float sidewalkZ = playerPosition.sidewalkSquares [x, 1];
			

			//Debug.Log (playerXCheck + ", " + playerZCheck + " (" + playerPosition.playerXPosition + ", " + playerPosition.playerZPosition +")");
			
			if ((movementXPosition == sidewalkX) && (movementZPosition == sidewalkZ)) {
				nextMoveMaterial.color = Color.red;
			} 
			
		}
		
//		for (int x = 0; x < playerPosition.entranceSquares.GetLength(0); x += 1) {
//			float entranceX = playerPosition.entranceSquares [x, 0];
//			float entranceZ = playerPosition.entranceSquares [x, 1];
//			
//			float playerXCheckEntrance = entranceX + 2f;
//			float playerZCheckEntrance = entranceZ - 1f;
//
//			bool entranceSquare = false;
//
//			Debug.Log(playerXCheckEntrance + ", " + playerZCheckEntrance+ " (" + playerPosition.playerXPosition + ", " + playerPosition.playerZPosition +")");
//			
//			if ((playerPosition.playerXPosition == playerXCheckEntrance) && (playerPosition.playerZPosition == playerZCheckEntrance)) {
//				nextMoveMaterial.color = Color.yellow;
//				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 1f, gameObject.transform.position.z);
//			} else {
//				nextMoveMaterial.color = Color.white;
//				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 0f, gameObject.transform.position.z);
//			}
//		}
	
		if (playerPosition.frontRightSelected == true) {
			nextMoveMaterial.color = Color.green;
		}

		if (playerPosition.frontRightSelected == true && playerPosition.ableToMove == true) {
			player.gameObject.transform.position = new Vector3 (playerPosition.playerXPosition - 2, 0.2f, playerPosition.playerZPosition + 1);
			playerPosition.playerWest = true;
			//knightModel.gameObject.transform.Rotate (knightModel.gameObject.transform.rotation.x, 0, knightModel.gameObject.transform.rotation.z, Space.World);
			nextMoveMaterial.color = Color.white;
			playerPosition.frontRightSelected = false;
			playerPosition.waitTime += 1;
			playerPosition.timeSinceMove = 0;
		}
	}
}
