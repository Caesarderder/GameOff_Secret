using UnityEngine;

public class BigWorldPlanet : MonoBehaviour
{
    public Transform _toFixDirGosParent;
    public EWorldType WorldType;
    
    public virtual void Start()
    {
        FixAllGosDir();
        DataModule.Resolve<GamePlayDM>().SetPlanet(this);
    }

    public void AddGosOnEarth(Transform tran)
    {
        tran.parent = _toFixDirGosParent;
        FixAllGosDir();
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
