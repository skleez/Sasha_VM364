using UnityEngine;
using System.Collections;

public class Player_West_South_Movement : MonoBehaviour {

	public Material nextMoveMaterial;
	public GameObject player;
	public GameObject knightModel;
	public Player_Position playerPosition;

	public float movementXPosition;
	public float movementZPosition;

	public bool overSidewalk = false;
	public bool overStreet = false;
	public bool overEntrance = false;


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



		//world coordinates of movement square
		movementXPosition = playerPosition.playerXPosition - 2f;
		movementZPosition = playerPosition.playerZPosition - 1f;


		for (int x = 0; x < playerPosition.sidewalkSquares.GetLength (0); x += 1) {
			

			float sidewalkX = playerPosition.sidewalkSquares [x, 0];
			float sidewalkZ = playerPosition.sidewalkSquares [x, 1];

			//Debug.Log (sidewalkX + ", " + sidewalkZ + " (" + movementXPosition + ", " + movementZPosition +")");

			if ((sidewalkX == movementXPosition) && (sidewalkZ == movementZPosition)) {


				nextMoveMaterial.color = Color.red;
			} 
				
		}

		for (int x = 0; x < playerPosition.entranceSquares.GetLength(0); x += 1) {

			float entranceX = playerPosition.entranceSquares [x, 0];
			float entranceZ = playerPosition.entranceSquares [x, 1];

			if ((entranceX == movementXPosition) && (entranceZ == movementZPosition)) {
				nextMoveMaterial.color = Color.yellow;
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 0.3f, gameObject.transform.position.z);
				overEntrance = true;
			}
		}

		for (int x = 0; x < playerPosition.streetSquares.GetLength(0); x += 1) {
			
			float streetX = playerPosition.streetSquares [x, 0];
			float streetZ = playerPosition.streetSquares [x, 1];
			
			if ((streetX == movementXPosition) && (streetZ == movementZPosition)) {

				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 0f, gameObject.transform.position.z);

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

