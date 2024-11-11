using UnityEngine;

public class BigWorldPlanet : MonoBehaviour
{
    public Transform _toFixDirGosParent;
    public Camera Camera;

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
