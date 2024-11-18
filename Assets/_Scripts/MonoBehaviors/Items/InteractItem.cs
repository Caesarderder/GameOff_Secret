using UnityEngine;

public class InteractItem:Item ,IPlayerInteractable
{
    public virtual void EnterTrigger(Transform tran)
    {
        SetInteractor(tran);
    }

    public virtual void ExitTrigger(Transform tran)
    {
        SetInteractor(tran);
    }

    public virtual void Interact()
    {
    }


    public virtual float Priority()
    {
        if(_interactor != null)
            return Vector3.SqrMagnitude(transform.position - _interactor.position);
        return -1;
    }
}