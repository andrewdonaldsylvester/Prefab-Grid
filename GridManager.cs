using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class GridManager : MonoBehaviour{

	private bool created = false;

//	[SerializeField]
	public GameObject[,] tiles;

//	[SerializeField]
//	private GameObject tlBound;
//
//	[SerializeField]
//	private GameObject brBound;

	// The following variables are editable in the inspector

	[HideInInspector]
	[SerializeField]
	private GameObject tilePrefab;

	[HideInInspector]
	[SerializeField]
	public int rows;

	[HideInInspector]
	[SerializeField]
	public int cols;

	[HideInInspector]
	[SerializeField]
	public float tileWidth = 1;

	[HideInInspector]
	[SerializeField]
	private float tileHeight = 1;

	[HideInInspector]
	[SerializeField]
	private float xMargin = 0.1f;

	[HideInInspector]
	[SerializeField]
	private float yMargin = 0.1f;

	[HideInInspector]
	[SerializeField]
	private bool bounded = false;

	[HideInInspector]
	[SerializeField]
	private float xOffset = 0;

	[HideInInspector]
	[SerializeField]
	private float yOffset = 0;

	[HideInInspector]
	[SerializeField]
	private bool autoCenter = true;



	public void CreateTiles() {

//		if (bounded && (tlBound == null || brBound == null)) {
//
//			tlBound = new GameObject ("TL");
//
//			brBound = new GameObject ("BR");
//
//		} else if (!bounded) {
//
//			if (tlBound != null) {
//				
//				DestroyImmediate (tlBound);
//
//				tlBound = null;
//
//			}
//
//			if (brBound != null) {
//
//				DestroyImmediate (brBound);
//
//				brBound = null;
//
//			}
//
//		}

		// Destroy all of the old tiles

//		print (tiles.GetLength (0));
//		print (tiles.GetLength (1));

		if (tiles != null) {

			for (int i = 0; i < tiles.GetLength (0); i++) {

				for (int j = 0; j < tiles.GetLength (1); j++) {

					DestroyImmediate (tiles [i, j]);

				}

			}

		}

//		do {
//
//			DestroyImmediate(transform.GetChild(0));
//
//		} while (transform.childCount > 0);

		// Create a new array and populate it with new tiles

		tiles = new GameObject[rows, cols];

		if (tilePrefab != null) {

			for (int row = 0; row < rows; row++) {

				for (int col = 0; col < cols; col++) {
				
					GameObject tile = Instantiate (tilePrefab, transform); // Create the tile

					tiles [row, col] = tile;

				}

			}

			created = true;

			// Position the new tiles

			PositionTiles ();

		}

		print(tiles [0, 0]);

	}


	public void PositionTiles() {

		// Make sure that the grid has created tiles

		if (!created) {

			CreateTiles ();

		}

//		if (bounded) {
//
//			tileWidth = Mathf.Abs(((tlBound.transform.position.x - brBound.transform.position.x) + xMargin - (cols * xMargin)) / cols);
//			tileHeight = Mathf.Abs(((tlBound.transform.position.y - brBound.transform.position.y) + yMargin - (rows * yMargin)) / rows);
//
//		}

		// Loop over each tile and reposition and resize it 

		for (int row = 0; row < rows; row++) {

			for (int col = 0; col < cols; col++) {

				GameObject tile = tiles[row, col];

				float posX = (col * tileWidth) + (xMargin * (col));
				float posY = (row * -tileHeight) - (yMargin * (row));

				tile.gameObject.transform.localPosition = new Vector2 (posX, posY);
				tile.gameObject.transform.localScale = new Vector2 (tileWidth, tileHeight);

			}

		}

		// Automatically center the grid 

		if (autoCenter) {
		
			Center ();

		}

	}

	public void Center() {

		// This positions the grid so that the middle tile (or the middle point) is at the offset points

		float gridWidth = (cols * tileWidth) + ((cols - 1) * xMargin);
		float gridHeight = (rows * tileHeight) + ((rows - 1) * yMargin);

		transform.position = new Vector2 (((tileWidth - gridWidth) / 2) + xOffset, ((gridHeight - tileHeight) / 2) - yOffset);

	}

	public Dictionary<string, GameObject> GetAdjacents(GameObject tile) {

		// Create a dictionary to hold the adjacent game objects

		Dictionary<string, GameObject> adjacents = new Dictionary<string, GameObject> ();

		// Get the index of the given tile

		System.Tuple<int, int> index = tiles.Index (tile);

		int row = index.Item1;
		int col = index.Item2;

		// Check top

		if (row - 1 >= 0) {
		
			adjacents.Add("T", tiles [row - 1, col]);

		}

		// Check right

		if (col + 1 < cols) {

			adjacents.Add("R", tiles[row, col + 1]);

		}

		// Check bottom

		if (row + 1 < rows) {

			adjacents.Add("B", tiles[row + 1, col]);

		}

		// Check left

		if (col - 1 >= 0) {

			adjacents.Add("L", tiles[row, col - 1]);

		}

		return adjacents;

	}

	public Dictionary<string, GameObject> GetDiagonals(GameObject tile) {
	
		Dictionary<string, GameObject> diagonals = new Dictionary<string, GameObject> ();

		System.Tuple<int, int> index = tiles.Index (tile);

		int row = index.Item1;
		int col = index.Item2;

		// Check top right

		if (row - 1 >= 0 && col + 1 < cols) {

			diagonals.Add ("TR", tiles [row - 1, col + 1]);

		}

		// Check bottom right

		if (row + 1 < rows && col + 1 < cols) {

			diagonals.Add ("BR", tiles[row + 1, col + 1]);

		}

		// Check bottom left

		if (row + 1 < rows && col - 1 >= 0) {

			diagonals.Add ("BL", tiles[row + 1, col - 1]);

		}

		// Check top left

		if (row - 1 >= 0 && col - 1 >= 0) {

			diagonals.Add ("BL", tiles[row - 1, col - 1]);

		}

		return diagonals;

	}

//	public void OnDrawGizmos() {
//
//		if (bounded) {
//			
//			Gizmos.color = Color.red;
//
//			Gizmos.DrawWireSphere (tlBound.transform.position, 0.5f);
//
//			Gizmos.color = Color.blue;
//
//			Gizmos.DrawWireSphere (brBound.transform.position, 0.5f);
//
//			Gizmos.color = Color.green;
//
//			Gizmos.DrawLine (tlBound.transform.position, new Vector3 (tlBound.transform.position.x, brBound.transform.position.y, 0));
//			Gizmos.DrawLine (tlBound.transform.position, new Vector3 (brBound.transform.position.x, tlBound.transform.position.y, 0));
//			Gizmos.DrawLine (brBound.transform.position, new Vector3 (tlBound.transform.position.x, brBound.transform.position.y, 0));
//			Gizmos.DrawLine (brBound.transform.position, new Vector3 (brBound.transform.position.x, tlBound.transform.position.y, 0));
//
//		}
//
//	}

}
