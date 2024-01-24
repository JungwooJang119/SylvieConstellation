using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SerializationAssistant
{
    [System.Serializable]
    public class SerializableVector3
    {
        public float x, y, z;

        public SerializableVector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
    }

    public static SerializableVector3 ToSerializable(this Vector3 v)
    {
        return new SerializableVector3(v);
    }

    public static Vector3 ToVector3(this SerializableVector3 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }
}
