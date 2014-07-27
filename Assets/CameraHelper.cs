using UnityEngine;
using System.Collections;

public class CameraHelper : MonoBehaviour {
	private float rotationSpeed = .15f;
	private float minZoom = 9.5f;
	private float maxZoom = 14f;
	
	void Update () {
		var currentPos = transform.position;
		if (Input.GetAxis ("Vertical") > 0) { // up arrow
			var newY = currentPos.y - rotationSpeed;
			newY = newY <= minZoom ? minZoom : newY;
			transform.position = new Vector3(currentPos.x, (float)newY, currentPos.z);
		}
		if (Input.GetAxis ("Vertical") < 0) { // down arrow
			var newY = currentPos.y + rotationSpeed;
			newY = newY >= maxZoom ? maxZoom : newY;
			transform.position = new Vector3(currentPos.x, (float)newY, currentPos.z);
		}
	}
}
