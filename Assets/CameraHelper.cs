using UnityEngine;
using System.Collections;

public class CameraHelper : MonoBehaviour {
	private static float rotationRate = .021f;
	
	private static float minZoomY = 7f;
	private static float maxZoomY = 14f;
	private static float minZoomZ = -3f;
	private static float maxZoomZ = 0f;
	
	private float ySpeed = (maxZoomY - minZoomY) * rotationRate;
	private float zSpeed = (maxZoomZ - minZoomZ) * rotationRate;
	
	void Update () {
		var currentPos = transform.position;
		if (Input.GetAxis ("Vertical") > 0) { // up arrow
			var newY = currentPos.y - ySpeed;
			newY = newY <= minZoomY ? minZoomY : newY;

			var newZ = currentPos.z + zSpeed;
			newZ = newZ >= maxZoomZ ? maxZoomZ : newZ;
			transform.position = new Vector3(currentPos.x, (float)newY, (float)newZ);
		}
		if (Input.GetAxis ("Vertical") < 0) { // down arrow
			var newY = currentPos.y + ySpeed;
			newY = newY >= maxZoomY ? maxZoomY : newY;

			var newZ = currentPos.z - zSpeed;
			newZ = newZ <= minZoomZ ? minZoomZ : newZ;
			transform.position = new Vector3(currentPos.x, (float)newY, (float)newZ);
		}
	}
}
