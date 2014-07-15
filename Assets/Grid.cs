using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	public GameObject plane;
	public int width = 10;
	public int height = 10;

	public static IList PreviousSelection = new ArrayList();
	public static GameObject[,] MyGrid = new GameObject[10, 10];
	public static GameObject staticPlayer;
	
	void Awake() {
		for (int x = 0; x < width; x++) {
			for (int z = 0; z < height; z++) {
				GameObject gridPlane = (GameObject)Instantiate(plane);
				gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x,
				                                           gridPlane.transform.position.y,
				                                           gridPlane.transform.position.z + z);
				gridPlane.renderer.material = PlaneHelper.planeHelper.DefaultMaterial;
				MyGrid[x,z] = gridPlane;
			}
		}

		staticPlayer = GameObject.CreatePrimitive(PrimitiveType.Cube);

		staticPlayer.transform.position = new Vector3 (3, 0, 3); // 0,0,0
		staticPlayer.renderer.material.color = Color.black;
		staticPlayer.AddComponent ("Player");
	}
}
