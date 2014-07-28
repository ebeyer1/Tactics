using UnityEngine;
using System.Collections;

public class EnemyHighlightedTileHelper : MonoBehaviour {

	void OnMouseDown() {
		var clickedX = (int)gameObject.transform.position.x;
		var clickedZ = (int)gameObject.transform.position.z;
		
		Player.player.MoveToCoord (clickedX, clickedZ);

		Grid.enemyPlayer.SetActive (false);

		PlaneHelper.ClearPreviousSelection();
	}
}
