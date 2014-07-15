using UnityEngine;
using System.Collections;

public class PlaneHelper : MonoBehaviour {

	public Material SelectedMaterial;
	public Material DefaultMaterial;
	public Material HighlightedMaterial;
	public bool Hello;

	public static PlaneHelper planeHelper;

	void Awake(){
		planeHelper = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void ClearPreviousSelection() {
		foreach (var pt in Grid.PreviousSelection) {
			var point = (MyPoint)pt;
			//Grid.MyGrid[point.X, point.Z].renderer.material.color = Color.white;
			Grid.MyGrid[point.X, point.Z].renderer.sharedMaterial = planeHelper.DefaultMaterial;
		}
		Grid.PreviousSelection.Clear ();
	}

	public static void ShowPossibleMoves(int currentX, int currentZ)
	{
		for (int i = -2; i <= 2; i++) {
			for (int j = -2; j <= 2; j++) {
				var x = currentX + i;
				var z = currentZ + j;
				if (x >= 0 && z >= 0 && x <= 9 && z <= 9) {

					if ( (i == 2 && j != 0) || 
					     (i == -2 && j != 0) || 
					     (j == 2 && i != 0) || 
					     (j == -2 && i != 0) )
					{
						continue;
					}

					//Grid.MyGrid[x, z].renderer.material.color = Color.yellow;
					Grid.MyGrid[x, z].renderer.sharedMaterial = planeHelper.HighlightedMaterial;
					var pt = new MyPoint(x,z);
					Grid.PreviousSelection.Add(pt);
				}
			}
		}
		//Grid.MyGrid [currentX, currentZ].renderer.material.color = Color.green; // surround algo shouldn't even turn 0,0 yellow
		Grid.MyGrid [currentX, currentZ].renderer.sharedMaterial = planeHelper.SelectedMaterial;
	}

	void SomeBadName() {//void OnMouseDown() {
		ClearPreviousSelection ();
		//Destroy (gameObject);
		var currentX = (int)gameObject.transform.position.x;
		var currentZ = (int)gameObject.transform.position.z;
		/*
		 * -1, -1
		 * -1, 0
		 * -1, 1
		 * 0, -1
		 * 0, 0
		 * 0, 1
		 * 1, -1
		 * 1, 0
		 * 1, 1
		 * 
		 * -2, 0
		 * 0, -2
		 * 2, 0
		 * 0, 2
		 */
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				var x = currentX + i;
				var z = currentZ + j;
				if (x >= 0 && z >= 0) {
					//Grid.MyGrid[x, z].renderer.material.color = Color.yellow;

					var pt = new MyPoint(x,z);
					Grid.PreviousSelection.Add(pt);
					//Debug.Log(Grid.PreviousSelection.Count);
				}
			}
		}
		//Grid.MyGrid [currentX, currentZ].renderer.material.color = Color.green; // surround algo shouldn't even turn 0,0 yellow
		Grid.MyGrid [currentX, currentZ].renderer.sharedMaterial = SelectedMaterial; // surround algo shouldn't even turn 0,0 yellow
		//Grid.MyGrid [currentX - 1, currentZ - 1].renderer.material.color = Color.yellow;
		//Debug.Log (gameObject.transform.position.x);
		//Debug.Log (gameObject.transform.position.z);
	}

	void OnMouseDown()
	{
		var currentX = (int)gameObject.transform.position.x;
		var currentZ = (int)gameObject.transform.position.z;
		//if (Grid.MyGrid [currentX, currentZ].renderer.material.color == Color.yellow) {
		if (Grid.MyGrid [currentX, currentZ].renderer.sharedMaterial == HighlightedMaterial) {
			//Player.MoveTo(currentX, currentZ);
			//var go = Player.ge
			Grid.staticPlayer.transform.position = new Vector3(currentX,Grid.staticPlayer.transform.position.y, currentZ);
			ClearPreviousSelection(); // hhhhhherrrrreeee
			//Debug.Log("move to there");
		}
	}
}

public class MyPoint {
	private int x;
	private int z;
	public MyPoint(int x, int z){
		this.x = x;
		this.z = z;
	}

	public int X {
		get {
			return this.x;
		}
	}

	public int Z {
		get {
			return this.z;
		}
	}
}
