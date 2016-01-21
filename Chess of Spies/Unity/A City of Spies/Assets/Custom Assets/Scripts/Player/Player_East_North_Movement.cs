using UnityEngine;
using System.Collections;

public class Player_East_North_Movement : MonoBehaviour {

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
		playerPosition.backRightSelected = true;
		playerPosition.backLeftSelected = false;
		playerPosition.leftBackSelected = false;
		playerPosition.leftFrontSelected = false;
	}

	void OnMouseUp () {

	}
	// Update is called once per frame
	void Update () {
		if (playerPosition.backRightSelected == true) {
			nextMoveMaterial.color = Color.green;
		}

		if (playerPosition.backRightSelected == true && playerPosition.ableToMove == true) {
			player.gameObject.transform.position = new Vector3 (playerPosition.playerXPosition + 2, 0.2f, playerPosition.playerZPosition + 1);
			playerPosition.playerEast = true;
			//knightModel.gameObject.transform.Rotate (knightPosition.gameObject.transform.rotation.x, 180f, knightModel.gameObject.transform.rotation.z, Space.World);
			nextMoveMaterial.color = Color.white;
			playerPosition.backRightSelected = false;
			playerPosition.waitTime += 1;
			playerPosition.timeSinceMove = 0;
		}
	
	}
}
