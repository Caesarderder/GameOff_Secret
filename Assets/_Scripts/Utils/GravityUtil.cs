using UnityEngine;

public static class GravityUtil
{
    public static Vector3 GetFaceVector(Vector3 vec,Vector3 gravityDir)
    {
        Vector3 normalizedGravityDir = gravityDir.normalized;
        Vector3 projectionOnGravity = Vector3.Project(vec, normalizedGravityDir);
        Vector3 faceVector = vec - projectionOnGravity;

        return faceVector;
    }
    public static Vector3 GetGravityVector(Vector3 vec,Vector3 gravityDir)
    {
        Vector3 normalizedGravityDir = gravityDir.normalized;
        return Vector3.Project(vec, normalizedGravityDir);
    }
    public static float GetGravitySpeed(Vector3 vec,Vector3 gravityDir)
    {
        var gravity = GetGravityVector(vec, gravityDir);
        if ( Vector3.Dot(gravity, gravityDir) > 0 )
        { 
        return -gravity.magnitude;
}
        return gravity.magnitude;
    }
}
