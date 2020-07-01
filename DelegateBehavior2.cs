using System;
using UnityEngine;
using System.Reflection;

public class DelegateBehavior2 : MonoBehaviour, ISerializationCallbackReceiver
{
	
	public System.Object script;

    [Serializable]
    private class NumericOperationSerializer : SerializableDelegate<NumericOperation> { }

    [SerializeField, HideInInspector]
    NumericOperationSerializer _serializer = new NumericOperationSerializer();

    public delegate int NumericOperation(int x, int y);

    public NumericOperation Adder;
    
    // Use this for initialization   
    void Start() {
		
		Adder = (x, y) => x + y;


    }   
      
    void OnGUI() {
        if (GUILayout.Button("Test")) {
            Debug.Log(Adder(5, 10));
        }
    }  

    public void OnBeforeSerialize() {
        _serializer.SetDelegate(Adder);
    }

    public void OnAfterDeserialize() {
		Adder = _serializer.CreateDelegate();
    }
}