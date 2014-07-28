using UnityEngine;
using System.Collections;

public class TurnController : MonoBehaviour {
	private static bool _isPlayersTurn = true;

	public static void ChangeTurn() {
		_isPlayersTurn = !_isPlayersTurn;
	}

	public static bool IsPlayersTurn() {
		return _isPlayersTurn;
	}
}
