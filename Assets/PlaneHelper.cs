using UnityEngine;
using System.Collections;

public class PlaneHelper : MonoBehaviour {

	public Material SelectedMaterial;
	public Material DefaultMaterial;
	public Material HighlightedMaterial;

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

	public static void ShowPossibleMoves(int x, int z, int dim) {
		if (dim < 0) {
			return;
		}

		if (Grid.MyGrid [x, z].renderer.sharedMaterial != planeHelper.HighlightedMaterial) {
			Grid.MyGrid [x, z].renderer.sharedMaterial = planeHelper.HighlightedMaterial;
		}
		var pt = new MyPoint(x,z);
		Grid.PreviousSelection.Add(pt);
		dim--;

		if (x+1 < Grid.Width) {
			ShowPossibleMoves(x+1,z,dim);
		}
		if (x-1 >= 0) {
			ShowPossibleMoves(x-1,z,dim);
		}
		if (z+1 < Grid.Height) {
			ShowPossibleMoves(x,z+1,dim);
		}
		if (z-1 >= 0) {
			ShowPossibleMoves(x,z-1,dim);
		}
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
