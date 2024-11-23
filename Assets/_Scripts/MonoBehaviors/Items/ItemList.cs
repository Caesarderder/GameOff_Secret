using UnityEngine;

public class ItemList:InteractItem,IPlayerInteractable
{
    public virtual void ChangeState(int id)
    {
    }    
    public override void EnterTrigger(Transform tran)
    {
        base.EnterTrigger(tran);
    }

    public override bool CanInteract()
    {
        return true;
    }

    public override void ExitTrigger(Transform tran)
    {
        base.ExitTrigger(tran);
    }

    public override void Interact()
    {
        base.Interact();
    }


    public override float Priority()
    {
        return base .Priority();
    }
}