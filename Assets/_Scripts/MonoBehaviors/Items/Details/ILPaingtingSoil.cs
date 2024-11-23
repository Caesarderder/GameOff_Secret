using UnityEngine;

public class ILPaingtingSoil:ItemList,IPlayerInteractable
{
    public override void ChangeState(int id)
    {
        //去除当前状态
        switch ( ItemId )
        {
            case 3001:
                break;
            case 3002:
                break;
            case 3003:
                break;
            default:
                break;
        }


        //更新现在状态
        switch ( id )
        {
            case 3001:
                break;
            case 3002:
                break;
            case 3003:
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
            case 3001:
                if(dm.TryGetCurBagItem(WorldType,out var item))
                {
                    item.Use(transform.position,()=>ChangeState(3002));
                }
                break;
            case 3002:
                if(dm.TryGetCurBagItem(WorldType,out var item1))
                {
                    item1.Use(transform.position,()=>ChangeState(3003));
                }
                break;
            case 3003:
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