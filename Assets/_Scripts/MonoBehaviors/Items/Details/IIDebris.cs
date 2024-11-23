using UnityEngine;

public class IIDebris : InteractItem,IPlayerInteractable
{
    public Animator animator;
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
        Destroy(gameObject);
        EventAggregator.Publish(new SItemInteractEvent() { 
            OldId = ItemId,
            CurId = ItemId,
        });
        animator.enabled = true;
    }


    public override float Priority()
    {
        return base.Priority();
    }
}

public struct SItemInteractEvent
{
    public int OldId;
    public int CurId;
}