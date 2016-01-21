using UnityEngine;
using System.Collections;

public class Player_South_East_Movement : MonoBehaviour {

	public Material nextMoveMaterial;
	public GameObject player;
	public GameObject knightModel;
	public Player_Position playerPosition;

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
		playerPosition.frontRightSelected = false;
		playerPosition.rightFrontSelected = false;
		playerPosition.rightBackSelected = false;
		playerPosition.backRightSelected = false;
		playerPosition.backLeftSelected = false;
		playerPosition.leftBackSelected = true;
		playerPosition.leftFrontSelected = false;
	}

	void OnMouseUp () {
	}
	// Update is called once per frame
	void Update () {
		if (playerPosition.leftBackSelected == true) {
			nextMoveMaterial.color = Color.green;
		}

		if (playerPosition.leftBackSelected == true && playerPosition.ableToMove == true) {
			player.gameObject.transform.position = new Vector3 (playerPosition.playerXPosition + 1, 0.2f, playerPosition.playerZPosition - 2);
			playerPosition.playerSouth = true;

			//knightModel.gameObject.transform.Rotate (knightModel.gameObject.transform.rotation.x, 270f, knightModel.gameObject.transform.rotation.z, Space.World);
			nextMoveMaterial.color = Color.white;
			playerPosition.leftBackSelected = false;
			playerPosition.waitTime += 1;
			playerPosition.timeSinceMove = 0;
		}
	
	}
}
