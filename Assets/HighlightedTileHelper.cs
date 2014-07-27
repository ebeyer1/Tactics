using UnityEngine;
using System.Collections;

public class HighlightedTileHelper : MonoBehaviour {

	void OnMouseDown() {
		var currentX = (int)gameObject.transform.position.x;
		var currentZ = (int)gameObject.transform.position.z;
		Grid.staticPlayer.transform.position = new Vector3(currentX,Grid.staticPlayer.transform.position.y, currentZ);
		PlaneHelper.ClearPreviousSelection();
	}
}