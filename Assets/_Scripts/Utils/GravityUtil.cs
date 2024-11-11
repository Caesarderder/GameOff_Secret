using UnityEngine;

public static class GravityUtil
{
    public static int IsSameDir(Vector2 a,Vector2 b)
    {
         var judge=Vector2.Dot(a, b);

        if(judge == 0) return 0;
        if(judge > 0) return 1;
         return -1;
    }
    public static int IsSameDir(Vector3 a,Vector3 b)
    {
         var judge=Vector3.Dot(a, b);

        if(judge == 0) return 0;
        if(judge > 0) return 1;
         return -1;
    }
    
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

    public static Vector3 GetFaceMoveVelocity(Vector2 velocity,Transform tran)
    {
        // 将 Vector2 的 x 分量应用在 tran.right 方向
        Vector3 rightVelocity = tran.right * velocity.x;

        // 将 Vector2 的 y 分量应用在 tran.forward 方向
        Vector3 forwardVelocity = tran.forward * velocity.y;

        // 返回组合后的 Vector3 速度
        return rightVelocity + forwardVelocity;
    }

    public static Vector3 GetFaceMoveDir(Vector3 dir,Transform tran)
    {
        // 获取 Transform 的 forward 和 right 方向向量
        Vector3 forward = tran.forward;
        Vector3 right = tran.right;

        // 计算 vector 在 forward 和 right 方向上的投影分量
        Vector3 projectionOnForward = Vector3.Project(dir, forward);
        Vector3 projectionOnRight = Vector3.Project(dir, right);

        // 将这两个分量相加，得到投影在 forward 和 right 组成的平面上的向量
        Vector3 projectedVector = projectionOnForward + projectionOnRight;

        return projectedVector.normalized;
    }
}
