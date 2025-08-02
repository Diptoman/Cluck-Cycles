using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Set X and Y for a transform position without changing z
    /// </summary>
    public static void SetPosXY(this Transform tf, float x, float y)
    {
        var pos = new Vector3(x, y, tf.position.z);
        tf.position = pos;
    }

    /// <summary>
    /// Set X and Y for a transform position without changing z
    /// </summary>
    public static void SetPosXY(this Transform tf, Vector2 pos)
    {
        tf.SetPosXY(pos.x, pos.y);
    }

    public static void SetPosXYLocal(this Transform tf, float x, float y)
    {
        var pos = new Vector3(x, y, tf.localPosition.z);
        tf.localPosition = pos;
    }

    public static void SetPosXYLocal(this Transform tf, Vector2 pos)
    {
        tf.SetPosXYLocal(pos.x, pos.y);
    }
}