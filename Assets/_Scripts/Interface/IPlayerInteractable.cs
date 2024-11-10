using UnityEngine;

public interface IPlayerInteractable
{
    float Priority();
    void EnterTrigger(Player player);
    void Interact();
    void ExitTrigger(Player player);
}

