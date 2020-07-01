using System;
using UnityEngine;
 
public class DelegateBehavior : MonoBehaviour, ISerializationCallbackReceiver {
 
    [SerializeField]
    SerializableAction _serializer = new SerializableAction();
    public Action DoStuff;
 
	// Use this for initialization
	void Start () {
        DoStuff = () => Debug.Log("Hey, this works!");
	}  
 
    void OnGUI() {
        if (GUILayout.Button("Test")) {
            DoStuff();
        }
    }
 
    public void OnBeforeSerialize() {
        _serializer.SetDelegate(DoStuff);
    }
 
    public void OnAfterDeserialize() {
        DoStuff = _serializer.CreateDelegate();
    }
}