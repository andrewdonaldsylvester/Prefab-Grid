//using System;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using UnityEngine;
//
//[Serializable]
//public class SerializableAction
//{
//    [SerializeField]
//    private UnityEngine.Object _target;
//    [SerializeField]
//    private string _methodName = "";
//    [SerializeField]
//    private byte[] _serialData = { };
//
//    public void SetDelegate(Action action) {
//        if (action == null) {
//            _target = null;
//            _methodName = "";
//            _serialData = new byte[] { };
//            return;
//        }
//
//        var delAction = action as Delegate;
//        if (delAction == null) {
//            throw new InvalidOperationException(typeof(Action).Name + " is not a delegate type.");
//        }
//
//
//        _target = delAction.Target as UnityEngine.Object;
//
//        if (_target != null) {
//            _methodName = delAction.Method.Name;
//            _serialData = null;
//        } else {
//            //Serialize the data to a binary stream
//            using (var stream = new MemoryStream()) {
//                (new BinaryFormatter()).Serialize(stream, action);
//                stream.Flush();
//                _serialData = stream.ToArray();
//            }
//            _methodName = null;
//        }
//    }
//
//    public Action CreateDelegate() {
//        if (_serialData.Length == 0 && _methodName == "") {
//            return null;
//        }
//
//        if (_target != null) {
//            return Delegate.CreateDelegate(typeof(Action), _target, _methodName) as Action;
//        }
//
//        using (var stream = new MemoryStream(_serialData)) {
//            return (new BinaryFormatter()).Deserialize(stream) as Action;
//        }
//    }
//}