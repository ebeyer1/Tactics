using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private bool _isSelected = false;

	void OnMouseDown() {
		var currPosition = gameObject.transform.position;
		//gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 1f, gameObject.transform.position.y, gameObject.transform.position.z);

		if (_isSelected) {
			PlaneHelper.ClearPreviousSelection ();
			_isSelected = false;
		} else {
			PlaneHelper.ShowPossibleMoves ((int)currPosition.x, (int)currPosition.z);
			_isSelected = true;
		}
	}

	public static void MoveTo(int x, int z) {
		//var y = go.transform.position.y;
		//this.Mv (x, y, z);
	}

	public void Mv(int x, int y, int z)
	{
		gameObject.transform.position = new Vector3 (x, y, z);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
