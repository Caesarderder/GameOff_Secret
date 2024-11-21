

using UnityEngine;

public struct InteractTest1Event
{

}
public struct InteractTest2Event
{

}

public struct ChangeGameStateEvent
{
    public EWorldType WorldType;
    public EGameMode GameMode;
}

public struct PlanetRoatateEvent
{
    public EWorldType WorldType;
    public Vector3 RotateAxis;
    public float Intensity;
}
