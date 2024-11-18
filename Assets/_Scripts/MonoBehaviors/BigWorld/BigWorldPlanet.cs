using UnityEngine;

public class BigWorldPlanet : MonoBehaviour
{
    [HideInInspector]
    public Transform _toFixDirGosParent;
    [HideInInspector]
    public Camera Camera;
    [HideInInspector]
    public EWorldType WorldType;
    

    public void SetWorldType(EWorldType type)
    {
        WorldType = type;
        SetItemBelongWorld();
    }


    public void SetItemBelongWorld()
    {
        var components=GetComponentsInChildren<Item>();
        foreach (var item in components)
        {
            item.BelongWorld = WorldType;
        }
    }

    public void Awake()
    {
    }

    public void Start()
    {
        FixAllGosDir();
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
