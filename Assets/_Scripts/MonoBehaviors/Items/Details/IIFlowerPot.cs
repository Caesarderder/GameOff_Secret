using UnityEngine;

public class IIFlowerPot : InteractItem, IPlayerInteractable
{
    public Animator animator;
    public GameObject flower;
    public override bool CanInteract()
    {
        return DataModule.Resolve<GamePlayDM>().GetCurBagItemId(WorldType) == 1004;
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
        if (DataModule.Resolve<GamePlayDM>().TryGetCurBagItem(WorldType, out var item))
        {
            item.Use(transform.position, () => {
                flower.SetActive(true);
                animator.Play("FlowerUp");

                DataModule.Resolve<GameStatusDM>().UpdateRuneState(1004, true);
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
