using UnityEngine;

/// <summary>
/// Provides types and extension methods to allow various aspects of
/// the game world to be saved.
/// </summary>
public static class SerializationAssistant
{
    /// <summary>
    /// A wrapper class for the UnityEngine Vector3 that can be serialized.
    /// Should be converted between a Vector3 using 
    /// <see cref="ToSerializable(Vector3)"/> 
    /// and <see cref="ToVector3(SerializableVector3)"/>.
    /// </summary>
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

    /// <summary>
    /// Converts a Vector3 to a SerializableVector3.
    /// </summary>
    /// <param name="v">The Vector3 to convert</param>
    /// <returns>The equivalent SerializableVector3</returns>
    public static SerializableVector3 ToSerializable(this Vector3 v)
    {
        return new SerializableVector3(v);
    }

    /// <summary>
    /// Converts a SerializableVector3 to a Vector3.
    /// </summary>
    /// <param name="v">The SerializableVector3 to convert</param>
    /// <returns>The equivalent Vector3</returns>
    public static Vector3 ToVector3(this SerializableVector3 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }
}
