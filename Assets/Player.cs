using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private bool _isSelected = false;

	void OnMouseDown() {
		var currPosition = gameObject.transform.position;

		if (_isSelected) {
			PlaneHelper.ClearPreviousSelection ();
			_isSelected = false;
		} else {
			PlaneHelper.ShowPossibleMoves ((int)currPosition.x, (int)currPosition.z);
			_isSelected = true;
		}
	}
}
