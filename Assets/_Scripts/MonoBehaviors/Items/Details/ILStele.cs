using DG.Tweening;
using UnityEngine;

public class ILStele : ItemList,IPlayerInteractable
{
    Collider[] _colliders;
    public override void Start()
    {
        base.Start();
        _colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in _colliders)
        {
            collider.enabled = false;
        }
        EventAggregator.Subscribe<SItemInteractEvent>(OnInteractEvent);
    }
    public void OnDestroy()
    {
        EventAggregator.Unsubscribe<SItemInteractEvent>(OnInteractEvent);
    }
    void OnInteractEvent(SItemInteractEvent evt)
    {
        if(evt.OldId==3001&&evt.CurId==3002&&ItemId==2001)
        {
            DoMove();
        }
        if(evt.OldId==3002&&evt.CurId==3003&&ItemId==2001)
        {
            ChangeState(2004);
        }
    }

    void DoMove()
    {
        transform.DOLocalMoveY(transform.localPosition.y+ 1.5f, 2f).OnComplete(() => { 
        foreach (Collider collider in _colliders)
        {
            collider.enabled = true;
        }
        });
    }

    //2001,2004
    public override void ChangeState(int id)
    {
        //去除当前状态
        switch ( ItemId )
        {
            case 2001:
                break;
            case 2004:
                break;
            default:
                break;
        }


        //更新现在状态
        switch ( id )
        {
            case 2001:
                break;
            case 2004:
                break;
            default:
                break;
        }
        ItemId = id;

    }    
    public override void EnterTrigger(Transform tran)
    {
        base.EnterTrigger(tran);
    }

    public override bool CanInteract()
    {
        switch ( ItemId )
        {
            case 3001:
                if(DataModule.Resolve<GamePlayDM>().GetCurBagItemId(WorldType)==4001)
                {
                    return true;
                }
                break;
            case 3002:
                if(DataModule.Resolve<GamePlayDM>().GetCurBagItemId(WorldType)==1001)
                {
                    return true;
                }
                break;
            case 3003:
                break;
            default:
                break;
        }
        return false;
    }

    public override void ExitTrigger(Transform tran)
    {
        base.ExitTrigger(tran);
    }

    public override void Interact()
    {
        base.Interact();
        var dm = DataModule.Resolve<GamePlayDM>();
        switch ( ItemId )
        {
            case 2001:
                DataModule.Resolve<GamePlayDM>().GenerateItemInBag(WorldType, 1003, transform.position, _interactor);
                break;
            case 2004:
                DataModule.Resolve<GamePlayDM>().GenerateItemInBag(WorldType, 1004, transform.position, _interactor);
                break;
            default:
                break;
        }
    }


    public override float Priority()
    {
        return base .Priority();
    }
}