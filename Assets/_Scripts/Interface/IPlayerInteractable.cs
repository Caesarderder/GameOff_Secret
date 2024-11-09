using UnityEngine;

public interface IPlayerInteractable
{
    float Priority();
    void EnterTrigger(KWPlayer player);
    void Interact();
    void ExitTrigger(KWPlayer player);
}

