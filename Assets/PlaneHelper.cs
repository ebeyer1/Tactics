using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlaneHelper : MonoBehaviour {
	public Material DefaultMaterial;
	public Material HighlightedMaterial;
	public Material EnemyHighlightedMaterial;
	public Material RockyTerrainMaterial;
	public bool isRockyTerrain;

	public static PlaneHelper planeHelper;

	void Awake(){
		planeHelper = this;
	}

	public static void ClearPreviousSelection() {
		Grid.HighlightedTiles.ForEach (t => Destroy (t));
		Grid.HighlightedTiles.Clear ();
	}

	public static void ShowPossibleMoves(int x, int z, int dim, bool isEnemy = false) {
		int required = 0;
		var scrpt = (PlaneHelper)Grid.MyGrid [x, z].GetComponent ("PlaneHelper");
		if (scrpt.isRockyTerrain && dim != Player.GetMaxMoves()) {
			required = 1;
		}

		if (dim < required) {
			return;
		}

		if (!Grid.HighlightedTiles.Any (p => p.transform.position.x == x & p.transform.position.z == z)) {
			GameObject p = GameObject.CreatePrimitive (PrimitiveType.Plane);
			p.transform.position = new Vector3 (x, .01f, z);
			if (Grid.enemyPlayer.transform.position.x == x && Grid.enemyPlayer.transform.position.z == z && !isEnemy) {
				p.GetComponent<Renderer>().material = PlaneHelper.planeHelper.EnemyHighlightedMaterial;
				p.AddComponent<EnemyHighlightedTileHelper>();
			} else {
				p.GetComponent<Renderer>().material = PlaneHelper.planeHelper.HighlightedMaterial;
				p.AddComponent<HighlightedTileHelper>();
			}
			p.GetComponent<Renderer>().transform.localScale = new Vector3 (.1f, .1f, .1f);

			Grid.HighlightedTiles.Add(p);
		}

		if (scrpt.isRockyTerrain && dim != Player.GetMaxMoves()) {
			dim--;
		}
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
}