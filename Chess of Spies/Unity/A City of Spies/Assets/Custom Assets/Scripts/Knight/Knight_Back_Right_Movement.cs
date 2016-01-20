using UnityEngine;
using System.Collections;

public class Knight_Back_Right_Movement : MonoBehaviour {

	public Material nextMoveMaterial;
	public GameObject player;
	public GameObject knightModel;
	public Knight_Position knightPosition;

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
		knightPosition.frontLeftSelected = false;
		knightPosition.frontRightSelected = false;
		knightPosition.rightFrontSelected = false;
		knightPosition.rightBackSelected = false;
		knightPosition.backRightSelected = true;
		knightPosition.backLeftSelected = false;
		knightPosition.leftBackSelected = false;
		knightPosition.leftFrontSelected = false;
	}

	void OnMouseUp () {

	}
	// Update is called once per frame
	void Update () {
		if (knightPosition.backRightSelected == true) {
			nextMoveMaterial.color = Color.green;
		}

		if (knightPosition.backRightSelected == true && knightPosition.ableToMove == true) {
			player.gameObject.transform.position = new Vector3 (knightPosition.playerXPosition + 2, 0.2f, knightPosition.playerZPosition + 1);
			knightPosition.playerEast = true;
			//knightModel.gameObject.transform.Rotate (knightPosition.gameObject.transform.rotation.x, 180f, knightModel.gameObject.transform.rotation.z, Space.World);
			nextMoveMaterial.color = Color.white;
			knightPosition.backRightSelected = false;
			knightPosition.waitTime += 1;
			knightPosition.timeSinceMove = 0;
		}
	
	}
}
