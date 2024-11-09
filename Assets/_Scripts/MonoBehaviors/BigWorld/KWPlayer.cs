using UnityEngine;

public class KWPlayer : MonoBehaviour
{
    InputDataModule inputDataModule;
    PlayerPlanetMovement _movement;
    InteractableSense _sense;

    public Vector3 Dir;
    public bool IsMove;
    public bool InteractInput;

    private void Awake()
    {
        inputDataModule = DataModule.Resolve<InputDataModule>();
        _movement=GetComponent<PlayerPlanetMovement>();
        _sense=GetComponentInChildren<InteractableSense>();
        _sense.Listener += OnInteractableTrigger;

        Manager<InputManager>.Inst.InputButtonBinding(InputManager.INTERACT,(isPress) =>
        {
            if(isPress)
                OnInteractInput();
        });
    }
    private void OnDestroy()
    {
        _sense.Listener -= OnInteractableTrigger;
        
    }

    private void Update()
    {
    }

    public void SetPlanetCenter(Transform center)
    {
        _movement.SetPlanetCenter(center);
    }    

    public void OnInteractInput()
    {
        if(_sense.TryGetInteractable(out var interactable))
        {
            interactable.Interact();
        }
    }

    private void OnInteractableTrigger(IPlayerInteractable interactable,bool isEnter)
    {
        if ( isEnter )
        {
            interactable.EnterTrigger(this);
        }
        else { 
            interactable.ExitTrigger(this);
        }
    }


}
