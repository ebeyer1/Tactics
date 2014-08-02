using UnityEngine;
using System.Collections;

public class HighlightedTileHelper : MonoBehaviour {

	void OnMouseDown() {
		var clickedX = (int)gameObject.transform.position.x;
		var clickedZ = (int)gameObject.transform.position.z;

		Player.player.MoveToCoord (clickedX, clickedZ);
	}
}