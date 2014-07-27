using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	public GameObject plane;
	public static int Width = 10;
	public static int Height = 10;

	public static IList PreviousSelection = new ArrayList();
	public static GameObject[,] MyGrid = new GameObject[10, 10];
	public static GameObject staticPlayer;
	public static GameObject enemyPlayer;
	
	void Awake() {
		for (int x = 0; x < Width; x++) {
			for (int z = 0; z < Height; z++) {
				GameObject gridPlane = (GameObject)Instantiate(plane);
				gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x,
				                                           gridPlane.transform.position.y,
				                                           gridPlane.transform.position.z + z);


				gridPlane.renderer.material = PlaneHelper.planeHelper.DefaultMaterial;

				MyGrid[x,z] = gridPlane;
			}
		}

		MyGrid [2, 2].renderer.material = PlaneHelper.planeHelper.RockyTerrainMaterial;
		var myScript = (PlaneHelper)MyGrid [2, 2].GetComponent ("PlaneHelper");
		myScript.isRockyTerrain = true;

		MyGrid [4, 4].renderer.material = PlaneHelper.planeHelper.RockyTerrainMaterial;
		myScript = (PlaneHelper)MyGrid [4, 4].GetComponent ("PlaneHelper");
		myScript.isRockyTerrain = true;

		MyGrid [6, 6].renderer.material = PlaneHelper.planeHelper.RockyTerrainMaterial;
		myScript = (PlaneHelper)MyGrid [6, 6].GetComponent ("PlaneHelper");
		myScript.isRockyTerrain = true;

		MyGrid [8, 8].renderer.material = PlaneHelper.planeHelper.RockyTerrainMaterial;
		myScript = (PlaneHelper)MyGrid [8, 8].GetComponent ("PlaneHelper");
		myScript.isRockyTerrain = true;

		staticPlayer = GameObject.CreatePrimitive(PrimitiveType.Cube);

		staticPlayer.transform.position = new Vector3 (3, 0, 3);
		staticPlayer.renderer.material.color = Color.black;
		staticPlayer.AddComponent ("Player");
	}

	void OnGUI()
	{
		GUI.Label (new Rect (110, 20, 100, 20), new GUIContent ("Max moves: " + Player.GetMaxMoves ()));
		if(GUI.Button(new Rect(100,40,100,50), new GUIContent("-")) && Player.GetMaxMoves() > 1)
		//if(GUI.Button(new Rect(100,40,200,150), new GUIContent("-")) && Player.GetMaxMoves() > 1) // for phone
	    {
			Player.SetMaxMoves(Player.GetMaxMoves()-1);

			if (Grid.PreviousSelection.Count > 0)
			{
				var playerPosition = Player.player.transform.position;
				PlaneHelper.ClearPreviousSelection();
				PlaneHelper.ShowPossibleMoves((int)playerPosition.x, (int)playerPosition.z, Player.GetMaxMoves());
			}
		}
		if(GUI.Button(new Rect(100,90,100,50), new GUIContent("+")) && Player.GetMaxMoves() < Mathf.Min(Grid.Width, Grid.Height))
		//if(GUI.Button(new Rect(100,190,200,150), new GUIContent("+")) && Player.GetMaxMoves() < Mathf.Min(Grid.Width, Grid.Height)) // for phone
		{
			Player.SetMaxMoves(Player.GetMaxMoves()+1);

			if (Grid.PreviousSelection.Count > 0)
			{
				var playerPosition = Player.player.transform.position;
				PlaneHelper.ClearPreviousSelection();
				PlaneHelper.ShowPossibleMoves((int)playerPosition.x, (int)playerPosition.z, Player.GetMaxMoves());
			}
		}
	}
}
