using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private bool _isSelected = false;
	private int _maxMoves = 2;

	void OnMouseDown() {
		var currPosition = gameObject.transform.position;

		if (_isSelected) {
			PlaneHelper.ClearPreviousSelection ();
			_isSelected = false;
		} else {
			PlaneHelper.ShowPossibleMoves((int)currPosition.x, (int)currPosition.z, _maxMoves);
			_isSelected = true;
		}
	}
}
