using UnityEngine;
using System.Collections;

public class Camera_Mouse : MonoBehaviour {

	public GameObject mainCamera;

	// Use this for initialization
	void Start () {

		//Cursor.visible = true;
		//Cursor.lockState = CursorLockMode.Locked;
	
	}
	
	// Update is called once per frame
	void Update () {
	
//		Vector3 mouseOffset = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);
//
//		mainCamera.transform.Rotate(mouseOffset, Space.Self);
	}
}
