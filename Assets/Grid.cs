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

		staticPlayer = GameObject.CreatePrimitive(PrimitiveType.Cube);

		staticPlayer.transform.position = new Vector3 (3, 0, 3);
		staticPlayer.renderer.material.color = Color.black;
		staticPlayer.AddComponent ("Player");
	}
}
