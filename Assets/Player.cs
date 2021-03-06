﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private bool _isSelected = false;
	private static int _maxMoves = 2;
	public static Player player;

	void Awake()
	{
		player = this;
	}

	private void MoveToCoord(float x, float y, float z) {
		var time = (Mathf.Abs (Grid.staticPlayer.transform.position.z - z) + Mathf.Abs (Grid.staticPlayer.transform.position.x - x)) * .5f;

		StartCoroutine (Grid.MoveObj (Grid.staticPlayer, new Vector3 (x, y, z), time));
	}

	public void MoveToCoord(float x, float z) {
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
