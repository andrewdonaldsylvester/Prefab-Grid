using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using ExtensionMethods;

public class Attempt1 : MonoBehaviour {

	public GridManager grid;

	public TextAsset file;

	void Start() {

		typeof(DataLoadFunction).GetMethod ("Invoke").Invoke(null, new object[] {});
		typeof(DataLoadFunction).GetMethod ("RedInit").Invoke(null, new object[] {gameObject});

		LoadTiles ();

	}

	public void LoadTiles() {

		string[,] caseArray = MapLoader.Load (file);

//		if ((caseArray.GetLength (0) != grid.tiles.GetLength(0)) && (caseArray.GetLength (1) != grid.tiles.GetLength(1))) {

		foreach (GameObject tile in grid.tiles) {

//			Debug.Log (tile);
//			Debug.Log (caseArray[grid.tiles.Index(tile).Item1, grid.tiles.Index(tile).Item2]);


			switch (caseArray[grid.tiles.Index(tile).Item1, grid.tiles.Index(tile).Item2]) {

			case "R":
				Debug.Log (tile);
				typeof(DataLoadFunction).GetMethod ("RedInit").Invoke(null, new object[] {tile});
				break;
			case "B":
				print("blue");
				break;
			default:
				print("default");
				break;

			}

		}

//		for (int i = 0; i < grid.rows; i++) {
//
//			for (int j = 0; j < grid.cols; j++) {
//
//				Debug.Log(grid.tiles[i, j]);
//
//
//				switch (caseArray[i, j]) {
//
//				case "R":
//					typeof(DataLoadFunction).GetMethod ("RedInit").Invoke(null, new object[] {grid.tiles[i, j]});
//					break;
//				case "B":
//					print("blue");
//					break;
//				default:
//					print("default");
//					break;
//
//				}
//
//			}



//		}

	}

}
