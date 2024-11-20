using UnityEngine;

public class Player : MonoBehaviour
{
    protected PlayerPlanetMovement _movement;
    protected InteractableSense _sense;
    protected 
     Camera _camera;

    public float MoveSpeed=5f;
    public bool CanInput;
    public EWorldType BelongWorld;

    protected virtual void Awake()
    {
        CanInput=true;
        _movement=GetComponent<PlayerPlanetMovement>();
        _sense=GetComponentInChildren<InteractableSense>();
        _sense.Listener += OnInteractableTrigger;
        _camera = DataModule.Resolve<GamePlayDM>().GetWorld(BelongWorld).Camera;

        EventAggregator.Subscribe<ChangeGameStateEvent>(OnGameStateChange);
    }
    protected virtual void OnDestroy()
    {
        _sense.Listener -= OnInteractableTrigger;
        EventAggregator.Unsubscribe<ChangeGameStateEvent>(OnGameStateChange);
        
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
            interactable.EnterTrigger(transform);
        }
        else { 
            interactable.ExitTrigger(transform);
        }
    }

    public void OnGameStateChange(ChangeGameStateEvent evt)
    {
        if(BelongWorld==evt.WorldType)
        {
            switch ( evt.GameMode )
            {
                case EGameMode.Normal:
                    CanInput = true;
                    break;
                case EGameMode.RunePanel:
                    CanInput = false;
                    _movement.Stop();
                    break;
                default:
                    break;
            }
        }
    }

}
