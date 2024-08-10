using UnityEngine;

public static class MySpecialClass
{
    public static float GetAngleTarget(Vector3 vector1, Vector3 vector2)
    {
        Vector3 deltaVector = vector2 - vector1;
        float tga = Mathf.Abs(deltaVector.z) / Mathf.Abs(deltaVector.x);
        float a = Mathf.Atan(tga);
        float angle = (180 * a) / Mathf.PI;
        if (Mathf.Abs(vector1.z + 10000) - Mathf.Abs(vector2.z + 10000) < 0
            && Mathf.Abs(vector1.x + 10000) - Mathf.Abs(vector2.x + 10000) < 0)
        {
            return 360 - angle;
        }
        else if (Mathf.Abs(vector1.z + 10000) - Mathf.Abs(vector2.z + 10000) < 0
            && Mathf.Abs(vector1.x + 10000) - Mathf.Abs(vector2.x + 10000) > 0)
        {
            return 180 + angle;
        }
        else if (Mathf.Abs(vector1.z + 10000) - Mathf.Abs(vector2.z + 10000) > 0
            && Mathf.Abs(vector1.x + 10000) - Mathf.Abs(vector2.x + 10000) > 0)
        {
            return 180 - angle;
        }
        else
        {
            return angle;
        }
    }
}

