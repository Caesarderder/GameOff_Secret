using UnityEditor.Build.Pipeline;
using UnityEngine;

public class IIWand: InteractItem,IPlayerInteractable
{
    private void Awake()
    {
    }
    private void OnDestroy()
    {
    }

    public override bool CanInteract()
    {
        var dm=DataModule.Resolve<GamePlayDM>();
        if ( dm.GetCurBagItemId(WorldType) == 1002 )
        {
            return true;
        }
        return false;
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
        if ( DataModule.Resolve<GamePlayDM>().TryGetCurBagItem(WorldType, out var item) )
        {
            item.Use(transform.position, () =>
            {
                DataModule.Resolve<GameStatusDM>().UpdateRuneState(1002, true);
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