using UnityEngine;

public class ILPaingtingSoil:ItemList,IPlayerInteractable
{
    public Animator animator;
    public GameObject go_seed;
    public GameObject go_flower;
    public override void ChangeState(int id)
    {
        var oldItemId = ItemId;
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
                go_seed.SetActive(true);
                animator.Play("SeedDown");
                break;
            case 3003:
                go_seed.SetActive(false);
                go_flower.SetActive(true);
                animator.Play("FlowerUp");
                break;
            default:
                break;
        }
        ItemId = id;
        EventAggregator.Publish(new SItemInteractEvent()
        {
            OldId= oldItemId,
            CurId=ItemId
        });

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
                    
                    if (item.ItemId == 4001)  
                    {
                       
                        item.Use(transform.position, () => ChangeState(3002));
                    }
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