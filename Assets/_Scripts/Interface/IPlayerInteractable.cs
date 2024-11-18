using UnityEngine;

public interface IPlayerInteractable
{
    float Priority();
    void EnterTrigger(Transform tran);
    void Interact();
    void ExitTrigger(Transform tran);
}

