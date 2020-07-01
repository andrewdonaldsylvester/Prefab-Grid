using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods {

	public static class GameObjectArrayIndex {

		public static System.Tuple<int, int> Index(this GameObject[,] array, GameObject obj) {

			for (int i = 0; i < array.GetLength (0); i++) {

				for (int j = 0; j < array.GetLength (1); j++) {

					if (GameObject.ReferenceEquals(obj, array[i, j])) {

						return new System.Tuple<int, int> (i, j);

					}

				}

			}

			throw new System.ArgumentException("Object not contained in array.");

		}

	}

}
