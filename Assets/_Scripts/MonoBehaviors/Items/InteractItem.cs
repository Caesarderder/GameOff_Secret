using UnityEngine;

public class InteractItem:Item ,IPlayerInteractable
{
    public virtual void EnterTrigger(Transform tran)
    {
        SetInteractor(tran);
    }

    public virtual bool CanInteract()
    {
        return true;
    }

    public virtual void ExitTrigger(Transform tran)
    {
        SetInteractor(tran);
    }

    public virtual void Interact()
    {
        Debug.Log(WorldType+" Itemact :"+ItemId);
    }


    public virtual float Priority()
    {
        if ( _interactor != null &&CanInteract())
        {
            Debug.Log(ItemId + " priority  " + 100 / Vector3.SqrMagnitude(transform.position - _interactor.position));
            return 100 / Vector3.SqrMagnitude(transform.position - _interactor.position);
        }
        Debug.Log(ItemId + " priority  " + -1);
        return -1;
    }
}