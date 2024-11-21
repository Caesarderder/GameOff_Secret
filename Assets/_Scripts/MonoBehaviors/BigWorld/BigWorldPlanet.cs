using System;
using UnityEngine;

public class BigWorldPlanet : MonoBehaviour
{
    public Transform _toFixDirGosParent;
    public EWorldType WorldType;
    public float RotateSpeed=10f;
    
    public virtual void Start()
    {
        FixAllGosDir();
        DataModule.Resolve<GamePlayDM>().SetPlanet(this);
        EventAggregator.Subscribe<PlanetRoatateEvent>(RotateTick);
    }

    private void OnDestroy()
    {
        EventAggregator.Unsubscribe<PlanetRoatateEvent>(RotateTick);
    }

    void RotateTick(PlanetRoatateEvent evt)
    {
        if (WorldType == evt.WorldType)
        {
            transform.Rotate(evt.RotateAxis, evt.Intensity*RotateSpeed * Time.deltaTime,Space.World);
        }
    }
    

    private void FixAllGosDir()
    {
        var childCount = _toFixDirGosParent.childCount;
        for ( int i = 0; i < childCount; i++ )
        {
            var child = _toFixDirGosParent.GetChild(i);
            child.up=child.position-transform.position;
        }
    }
    private void FixGoDir(Transform tran)
    {
        
    }
}
