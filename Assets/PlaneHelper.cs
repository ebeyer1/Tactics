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

	public static void ClearPreviousSelection() {
		foreach (var pt in Grid.PreviousSelection) {
			var point = (MyPoint)pt;
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

					Grid.MyGrid[x, z].renderer.sharedMaterial = planeHelper.HighlightedMaterial;
					var pt = new MyPoint(x,z);
					Grid.PreviousSelection.Add(pt);
				}
			}
		}
		Grid.MyGrid [currentX, currentZ].renderer.sharedMaterial = planeHelper.SelectedMaterial; // surround algo shouldn't even highlight this spot
	}

	void OnMouseDown()
	{
		var currentX = (int)gameObject.transform.position.x;
		var currentZ = (int)gameObject.transform.position.z;
		if (Grid.MyGrid [currentX, currentZ].renderer.sharedMaterial == HighlightedMaterial) {
			Grid.staticPlayer.transform.position = new Vector3(currentX,Grid.staticPlayer.transform.position.y, currentZ);
			ClearPreviousSelection();
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
