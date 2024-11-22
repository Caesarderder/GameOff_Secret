using UnityEngine;

public interface IPlayerInteractable
{
    float Priority();
    void EnterTrigger(Transform tran);
    bool CanInteract();
    void Interact();
    void ExitTrigger(Transform tran);
}

public interface IWorldObject
{
    void Init();
    EWorldType WorldType { get; set; }
}
