
using Unity.VisualScripting;
using UnityEngine;

public enum EWorldType
{
    A=1,
    B=2,
}
public enum EItemState
{
    NotExist=0,
    Exist=1,
    NotExsitAndUsedSucessfully=2,
    ExistAndUsedSucessfully=3,
}


public class Item:MonoBehaviour
{
    [HideInInspector]
    public EWorldType BelongWorld;
    protected Transform _interactor;

    public int ItemId;

    public virtual void OnEnable()
    {
        var dm=DataModule.Resolve<GamePlayDM>();
        dm.SetItemState(this,EItemState.Exist);
    }

    public void SetInteractor(Transform inter)
    {
        _interactor = inter;
    }
    public void RemoveInteractor(Transform inter)
    {
        _interactor = null;
    }
}