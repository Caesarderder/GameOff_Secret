using UnityEngine;

public class Player : MonoBehaviour
{
    protected KWPlayerPlanetMovement _movement;
    InteractableSense _sense;

    public Vector3 Dir;
    public bool IsMove;
    public bool InteractInput;

    protected virtual void Awake()
    {
        _movement=GetComponent<KWPlayerPlanetMovement>();
        _sense=GetComponentInChildren<InteractableSense>();
        _sense.Listener += OnInteractableTrigger;
    }
    protected virtual void OnDestroy()
    {
        _sense.Listener -= OnInteractableTrigger;
        
    }

    protected virtual void Update()
    {
    }

    public void SetPlanetCenter(Transform center)
    {
        _movement.SetPlanetCenter(center);
    }

    public virtual void OnInteractInput()
    {
        if(_sense.TryGetInteractable(out var interactable))
        {
            interactable.Interact();
        }
    }

    public virtual void OnInteractableTrigger(IPlayerInteractable interactable,bool isEnter)
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
