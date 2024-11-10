using UnityEngine;

public class CWPlayer : Player
{
    public override void OnInteractableTrigger(IPlayerInteractable interactable, bool isEnter)
    {
        base.OnInteractableTrigger(interactable, isEnter);
    }

    public override void OnInteractInput()
    {
        base.OnInteractInput();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Update()
    {
        base.Update();
    }
}
