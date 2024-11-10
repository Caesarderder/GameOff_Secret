using UnityEngine;

public class KWPlayer : Player
{
    InputDataModule inputDataModule;
    PlayerPlanetMovement _movement;
    InteractableSense _sense;

    protected override void Awake()
    {
        base.Awake();
        inputDataModule = DataModule.Resolve<InputDataModule>();
        Manager<InputManager>.Inst.InputButtonBinding(InputManager.INTERACT, (isPress) =>
        {
            if ( isPress )
                OnInteractInput();

        });
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnInteractableTrigger(IPlayerInteractable interactable, bool isEnter)
    {
        base.OnInteractableTrigger(interactable, isEnter);
    }
}




