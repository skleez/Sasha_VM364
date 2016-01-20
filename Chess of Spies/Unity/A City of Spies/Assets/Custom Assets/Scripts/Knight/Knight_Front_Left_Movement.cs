using UnityEngine;
using System.Collections;

public class Knight_Front_Left_Movement : MonoBehaviour {

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
		knightPosition.frontLeftSelected = true;
		knightPosition.frontRightSelected = false;
		knightPosition.rightFrontSelected = false;
		knightPosition.rightBackSelected = false;
		knightPosition.backRightSelected = false;
		knightPosition.backLeftSelected = false;
		knightPosition.leftBackSelected = false;
		knightPosition.leftFrontSelected = false;
	}

	void OnMouseUp () {
		

	}
	
	// Update is called once per frame
	void Update () {

		if (knightPosition.frontLeftSelected == true) {
			nextMoveMaterial.color = Color.green;
		}

		if (knightPosition.frontLeftSelected == true && knightPosition.ableToMove == true) {
			player.gameObject.transform.position = new Vector3 (knightPosition.playerXPosition - 2, 0.2f, knightPosition.playerZPosition - 1); 
			knightPosition.playerWest = true;

			//knightModel.gameObject.transform.Rotate (knightModel.gameObject.transform.rotation.x, 0f, knightModel.gameObject.transform.rotation.z, Space.World);
			nextMoveMaterial.color = Color.white;
			knightPosition.frontLeftSelected = false;
			knightPosition.waitTime += 1;
			knightPosition.timeSinceMove = 0;
		}
	
	}
}
