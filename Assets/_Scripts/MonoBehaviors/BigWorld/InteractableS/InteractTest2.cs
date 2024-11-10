using UnityEngine;

public class InteractTest2: MonoBehaviour, IPlayerInteractable
{
    KWPlayer player;
    private void Awake()
    {
        EventAggregator.Subscribe<InteractTest1Event>(OnScale);
    }
    private void OnDestroy()
    {
        EventAggregator.Unsubscribe<InteractTest1Event>(OnScale);
    }
    public void EnterTrigger(Player player)
    {
        Debug.Log(GetType().Name+"Enter");
    }

    public void ExitTrigger(Player player )
    {
        Debug.Log(GetType().Name+"Exit");
    }

    public void Interact()
    {
        Debug.Log(GetType().Name+"INteract");
        EventAggregator.Publish(new InteractTest2Event());
    }

    public void OnScale(InteractTest1Event @event)
    {
        Debug.Log(GetType().Name+"Smaller");
        transform.localScale*=0.9f  ;
    }
    public float Priority()
    {
        return (player.transform.position-transform.position).magnitude;
    }
}