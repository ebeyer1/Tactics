using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	public GameObject plane;
	public static int Width = 10;
	public static int Height = 10;

	public static List<GameObject> HighlightedTiles = new List<GameObject>();
	public static GameObject[,] MyGrid = new GameObject[10, 10];
	public static GameObject staticPlayer;
	public static GameObject enemyPlayer;

	private static bool compCanPlay = true;
	
	void Awake() {
		for (int x = 0; x < Width; x++) {
			for (int z = 0; z < Height; z++) {
				GameObject gridPlane = (GameObject)Instantiate(plane);
				gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x,
				                                           gridPlane.transform.position.y,
				                                           gridPlane.transform.position.z + z);


				gridPlane.GetComponent<Renderer>().material = PlaneHelper.planeHelper.DefaultMaterial;

				MyGrid[x,z] = gridPlane;
			}
		}

		MyGrid [2, 2].GetComponent<Renderer>().material = PlaneHelper.planeHelper.RockyTerrainMaterial;
		var myScript = (PlaneHelper)MyGrid [2, 2].GetComponent ("PlaneHelper");
		myScript.isRockyTerrain = true;

		MyGrid [4, 4].GetComponent<Renderer>().material = PlaneHelper.planeHelper.RockyTerrainMaterial;
		myScript = (PlaneHelper)MyGrid [4, 4].GetComponent ("PlaneHelper");
		myScript.isRockyTerrain = true;

		MyGrid [6, 6].GetComponent<Renderer>().material = PlaneHelper.planeHelper.RockyTerrainMaterial;
		myScript = (PlaneHelper)MyGrid [6, 6].GetComponent ("PlaneHelper");
		myScript.isRockyTerrain = true;

		MyGrid [8, 8].GetComponent<Renderer>().material = PlaneHelper.planeHelper.RockyTerrainMaterial;
		myScript = (PlaneHelper)MyGrid [8, 8].GetComponent ("PlaneHelper");
		myScript.isRockyTerrain = true;

		// me
		Object prefab = Resources.Load("space-wizard");
		staticPlayer = (GameObject) Instantiate(prefab, new Vector3(3, 0, 3), Quaternion.identity);
		staticPlayer.AddComponent<BoxCollider> ();
		staticPlayer.AddComponent <Player>();

		// enemy
		enemyPlayer = GameObject.CreatePrimitive (PrimitiveType.Capsule);

		enemyPlayer.transform.position = new Vector3 (7, 0, 7);
		enemyPlayer.GetComponent<Renderer>().material.color = Color.magenta;
		enemyPlayer.transform.localScale = new Vector3 (.5f, .5f, .5f);
	}

	private List<GameObject> cards = new List<GameObject> ();
	void OnGUI()
	{
		GUI.Label (new Rect (110, 20, 100, 20), new GUIContent ("Max moves: " + Player.GetMaxMoves ()));
		if(GUI.Button(new Rect(100,40,100,50), new GUIContent("-")) && Player.GetMaxMoves() > 1)
		//if(GUI.Button(new Rect(100,40,200,150), new GUIContent("-")) && Player.GetMaxMoves() > 1) // for phone
	    {
			Player.SetMaxMoves(Player.GetMaxMoves()-1);

			if (Grid.HighlightedTiles.Count > 0)
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

			if (Grid.HighlightedTiles.Count > 0)
			{
				var playerPosition = Player.player.transform.position;
				PlaneHelper.ClearPreviousSelection();
				PlaneHelper.ShowPossibleMoves((int)playerPosition.x, (int)playerPosition.z, Player.GetMaxMoves());
			}
		}

		if (GUI.Button(new Rect(100,140,100,50), new GUIContent("Add card"))) {
			var count = cards.Count;
			var go = GameObject.CreatePrimitive(PrimitiveType.Plane);
			go.transform.localScale = new Vector3(.1f,.1f,.1f);
			var column = ((int)(count / (Height-1)));
			var xSpot = 10.1f + (column * 1.1f); // startingColumnSpot + (column * columnSpacing);
			var row = (count % (Height-1));
			var zSpot = row*1.1f; // row * rowSpacing
			go.transform.position = new Vector3(xSpot,.1f,zSpot);

			// add a script component
			// script will have random abilities.
			// will also handle the onclick event which grants abilities.
			// on card hover will zoom object

			cards.Add(go);
		}

		GUI.Label (new Rect (750, 20, 100, 20), new GUIContent ("Turn: " + (TurnController.IsPlayersTurn () ? "player" : "computer")));
	}

	void Update() {
		if (Grid.enemyPlayer.activeSelf && !TurnController.IsPlayersTurn () && compCanPlay) {
			PlaneHelper.ShowPossibleMoves((int)Grid.enemyPlayer.transform.position.x,
			                              (int)Grid.enemyPlayer.transform.position.z,
			                              1,
			                              true);
			compCanPlay = false;
			// Move enemy towards player. Can move one space.
			var enemyPos = enemyPlayer.transform.position;
			var playerPos = staticPlayer.transform.position;
			Vector3 target;

			// enemy can move left
			if (Mathf.Abs(enemyPos.x - playerPos.x) >= Mathf.Abs(enemyPos.z - playerPos.z) &&
			    enemyPos.x > playerPos.x &&
			    CanMoveHere((int)enemyPos.x-1, (int)enemyPos.z))
			{
				target = new Vector3(enemyPos.x - 1, enemyPos.y, enemyPos.z);
			}
			// enemy can move right
			else if (Mathf.Abs(enemyPos.x - playerPos.x) >= Mathf.Abs(enemyPos.z - playerPos.z) &&
			    	 enemyPos.x > playerPos.x &&
			    	 CanMoveHere((int)enemyPos.x+1, (int)enemyPos.z))
			{
				target = new Vector3(enemyPos.x + 1, enemyPos.y, enemyPos.z);
			}
			// enemy can move down
			else if (enemyPos.z > playerPos.z &&
			         CanMoveHere((int)enemyPos.x, (int)enemyPos.z - 1))
			{
				target = new Vector3(enemyPos.x, enemyPos.y, enemyPos.z - 1);
			}
			// enemy moves up
			else {
				target = new Vector3(enemyPos.x, enemyPos.y, enemyPos.z + 1);
			}

			StartCoroutine(MoveObj(enemyPlayer, target, 1.0f));
		}
	}

	private static bool CanMoveHere(int x, int z) {
		return !((PlaneHelper)Grid.MyGrid [x, z].GetComponent ("PlaneHelper")).isRockyTerrain;
	}

	public static IEnumerator MoveObj(GameObject gameObj, Vector3 target, float time) {
		var startPos = gameObj.transform.position;

		var i = 0.0f;
		var rate = 1.0f/time;
		while (i < 1.0) {
			i += (float)(Time.deltaTime * rate);
			gameObj.transform.position = Vector3.Lerp(startPos, target, i);
			yield return null;
		}

		PlaneHelper.ClearPreviousSelection();
		compCanPlay = true;
		TurnController.ChangeTurn ();
	}
}
