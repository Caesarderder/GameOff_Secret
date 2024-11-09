using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractTest1 : MonoBehaviour, IPlayerInteractable
{
    KWPlayer player;
    private void Awake()
    {
        EventAggregator.Subscribe<InteractTest2Event>(OnMove);
    }
    private void OnDestroy()
    {
        EventAggregator.Unsubscribe<InteractTest2Event>(OnMove);
    }
    public void EnterTrigger(KWPlayer player)
    {
        Debug.Log("Enter");
    }

    public void ExitTrigger(KWPlayer player )
    {
        Debug.Log("Exit");
    }

    public void Interact()
    {
        Debug.Log("INteract");
        EventAggregator.Publish(new InteractTest1Event());
        //
    }
    public void OnMove(InteractTest2Event @event)
    {
        Debug.Log(GetType().Name + "Move");
        transform.position+= transform.forward*0.2f;
    }

    public float Priority()
    {
        return (player.transform.position-transform.position).magnitude;
    }
}
