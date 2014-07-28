using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private bool _isSelected = false;
	private static int _maxMoves = 2;
	public static Player player;

	void Awake()
	{
		player = this;
	}

	private static void MoveToCoord(float x, float y, float z) {
		Grid.staticPlayer.transform.position = new Vector3(x,y,z);
	}

	public static void MoveToCoord(float x, float z) {
		MoveToCoord (x, Grid.staticPlayer.transform.position.y, z);
	}

	public static void SetMaxMoves(int max)
	{
		_maxMoves = max;
	}

	public static int GetMaxMoves()
	{
		return _maxMoves;
	}

	void OnMouseDown() {
		if (TurnController.IsPlayersTurn ()) {
			var currPosition = gameObject.transform.position;

			if (_isSelected) {
				PlaneHelper.ClearPreviousSelection ();
				_isSelected = false;
			} else {
				PlaneHelper.ShowPossibleMoves ((int)currPosition.x, (int)currPosition.z, _maxMoves);
				_isSelected = true;
			}
		}
	}
}
