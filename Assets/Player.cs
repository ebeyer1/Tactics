using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private bool _isSelected = false;
	private static int _maxMoves = 2;
	public static Player player;

	public static void SetMaxMoves(int max)
	{
		_maxMoves = max;
	}

	public static int GetMaxMoves()
	{
		return _maxMoves;
	}

	void Awake()
	{
		player = this;
	}

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
