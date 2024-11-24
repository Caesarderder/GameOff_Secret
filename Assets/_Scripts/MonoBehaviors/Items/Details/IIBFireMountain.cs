using DG.Tweening;
using UnityEngine;

public class IIBFireMountain : InteractItem,IPlayerInteractable
{
    public override bool CanInteract()
    {
        return DataModule.Resolve<GamePlayDM>().GetCurBagItemId(WorldType)==1003;
    }

    public override void EnterTrigger(Transform tran)
    {
        base.EnterTrigger(tran);
    }

    public override void ExitTrigger(Transform tran)
    {
        base.ExitTrigger(tran);
    }

    public override void Interact()
    {
        if(DataModule.Resolve<GamePlayDM>().TryGetCurBagItem(WorldType,out var item))
        {
            item.Use(transform.position, () => {
                DataModule.Resolve<GameStatusDM>().UpdateRuneState(1003, true);
                transform.DOLocalMoveX(transform.localPosition.x - 0.5f, 1f);
                EventAggregator.Publish(new SItemInteractEvent()
                {
                    CurId = ItemId
                });
            });
        }
    }


    public override float Priority()
    {
        return base.Priority();
    }
}