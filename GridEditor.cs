using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridEditor : Editor {

	public override void OnInspectorGUI() {

		base.OnInspectorGUI ();

		GridManager grid = (GridManager)target;

		EditorStyles.label.fontStyle = FontStyle.Normal;

		GUIStyle headerStyle = new GUIStyle(EditorStyles.label);
		headerStyle.fontStyle = FontStyle.Bold;


		// Check the tile prefab

		EditorGUI.BeginChangeCheck ();

		EditorGUILayout.LabelField("Grid Dimensions", headerStyle);

		EditorGUILayout.PropertyField (serializedObject.FindProperty("tilePrefab"), new GUIContent("Tile"));

		if (EditorGUI.EndChangeCheck ()) {

			grid.CreateTiles ();

		}


		// Check the grid size

		EditorGUI.BeginChangeCheck ();

		EditorGUILayout.LabelField("Grid Dimensions", headerStyle);

		GUILayout.BeginHorizontal ();

		EditorGUILayout.PropertyField (serializedObject.FindProperty("rows"), new GUIContent("Rows"), GUILayout.ExpandWidth(false));

		if (GUILayout.Button ("+", GUILayout.Height(20), GUILayout.Width(20))) {
		
			serializedObject.FindProperty ("rows").intValue++;
		
		}

		if (GUILayout.Button ("-", GUILayout.Height(20), GUILayout.Width(20))) {

			serializedObject.FindProperty ("rows").intValue--;

		}

		GUILayout.EndHorizontal ();



		GUILayout.BeginHorizontal ();

		EditorGUILayout.PropertyField (serializedObject.FindProperty("cols"), new GUIContent("Columns"), GUILayout.ExpandWidth(false));

		if (GUILayout.Button ("+", GUILayout.Height(20), GUILayout.Width(20))) {

			serializedObject.FindProperty ("cols").intValue++;

		}

		if (GUILayout.Button ("-", GUILayout.Height(20), GUILayout.Width(20))) {

			serializedObject.FindProperty ("cols").intValue--;

		}
		GUILayout.EndHorizontal ();


		if (serializedObject.FindProperty ("rows").intValue < 1) {

			serializedObject.FindProperty ("rows").intValue = 1;

		}

		if (serializedObject.FindProperty ("cols").intValue < 1) {

			serializedObject.FindProperty ("cols").intValue = 1;

		}

		serializedObject.ApplyModifiedProperties ();

		if (EditorGUI.EndChangeCheck()) {

			grid.CreateTiles();

		}




		// Check if the tile size was changed

		EditorGUI.BeginChangeCheck ();

		EditorGUILayout.LabelField("Tile Dimensions", headerStyle);

		EditorGUILayout.PropertyField (serializedObject.FindProperty("tileWidth"), new GUIContent("Width"));
		EditorGUILayout.PropertyField (serializedObject.FindProperty("tileHeight"), new GUIContent("Height"));

		EditorGUILayout.LabelField("Tile Margins", headerStyle);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("xMargin"), new GUIContent("Width"));
		EditorGUILayout.PropertyField (serializedObject.FindProperty("yMargin"), new GUIContent("Height"));

		EditorGUILayout.LabelField("Grid Offset", headerStyle);

		EditorGUILayout.PropertyField (serializedObject.FindProperty("xOffset"), new GUIContent("X Offset"));
		EditorGUILayout.PropertyField (serializedObject.FindProperty("yOffset"), new GUIContent("Y Offset"));

		EditorGUILayout.LabelField("Centering", headerStyle);

		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.PropertyField (serializedObject.FindProperty("autoCenter"), new GUIContent("Auto-Center"));

		if (GUILayout.Button("Center the Grid", GUILayout.Height(20), GUILayout.Width(200))) {

			grid.Center();

		}

		serializedObject.ApplyModifiedProperties ();

		if (EditorGUI.EndChangeCheck()) {

			grid.PositionTiles();

		}

		EditorGUILayout.EndHorizontal ();

//		EditorGUI.BeginChangeCheck ();
//
//		EditorGUILayout.LabelField("Boundings", headerStyle);
//
//		EditorGUILayout.PropertyField(serializedObject.FindProperty("bounded"), new GUIContent("Bounded"));
//
//		serializedObject.ApplyModifiedProperties ();
//
//		if (EditorGUI.EndChangeCheck ()) {
//
//			grid.CreateTiles ();
//
//		}

	}

}
