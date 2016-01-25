using UnityEngine;
using System.Collections;

public class Player_West_South_Movement : MonoBehaviour {

	public Material nextMoveMaterial;
	public GameObject player;
	public GameObject knightModel;
	public Player_Position playerPosition;


	// Use this for initialization
	void Start () {

		//Screen.lockCursor = true;

	}

	void OnMouseOver () {

		//gameObject.GetComponent<Renderer> ().enabled = true;
		nextMoveMaterial.color = Color.blue;

	}

	void OnMouseExit () {
		nextMoveMaterial.color = Color.white;
		//gameObject.GetComponent<Renderer> ().enabled = false;
	}
	void OnMouseDown () {
		playerPosition.frontLeftSelected = true;
		playerPosition.frontRightSelected = false;
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
		

//		for (float sidewalkX = 0; sidewalkX < playerPosition.sidewalkSquares.GetLength(0); sidewalkX++){
//			for (float sidewalkZ = 0; sidewalkX < playerPosition.sidewalkSquares.GetLength(1); sidewalkZ++){
//				//Debug.Log (sidewalkX + ", " + sidewalkZ);
//			}
//		}

		for (int x = 0; x < playerPosition.sidewalkSquares.GetLength (0); x += 1) {
			

			float sidewalkX = playerPosition.sidewalkSquares [x, 0];
			float sidewalkZ = playerPosition.sidewalkSquares [x, 1];

			//add negative movement change
			float playerXCheck = sidewalkX + 2f;
			float playerZCheck = sidewalkZ + 1f;

			Debug.Log (playerXCheck + ", " + playerZCheck + " (" + playerPosition.playerXPosition + ", " + playerPosition.playerZPosition +")");

			if ((playerPosition.playerXPosition == playerXCheck) && (playerPosition.playerZPosition == playerZCheck)) {
				nextMoveMaterial.color = Color.red;
			} else {
				//gameObject.GetComponent<Renderer> ().enabled = true;
			}
				
		}
//
			//float[,] sidewalk = new float[,] {playerPosition{0}, square};


			//float sidewalkX = playerPosition.side{0} ;


		if (playerPosition.frontLeftSelected == true) {
			nextMoveMaterial.color = Color.green;
		}

		if (playerPosition.frontLeftSelected == true && playerPosition.ableToMove == true) {
			player.gameObject.transform.position = new Vector3 (playerPosition.playerXPosition - 2, 0.2f, playerPosition.playerZPosition - 1); 
			playerPosition.playerWest = true;

			//knightModel.gameObject.transform.Rotate (knightModel.gameObject.transform.rotation.x, 0f, knightModel.gameObject.transform.rotation.z, Space.World);
			nextMoveMaterial.color = Color.white;
			playerPosition.frontLeftSelected = false;
			playerPosition.waitTime += 1;
			playerPosition.timeSinceMove = 0;
		}
	
	}
}
