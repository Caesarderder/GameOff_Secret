using UnityEngine;

public class IIRiver : InteractItem,IPlayerInteractable
{
    bool _canInteract;
    private void Awake()
    {
        gameObject.SetActive(false);
        _canInteract = false;

        EventAggregator.Subscribe<SItemInteractEvent>(OnRise);
    }
    private void OnDestroy()
    {
        EventAggregator.Unsubscribe<SItemInteractEvent>(OnRise);
    }
    public void OnRise(SItemInteractEvent evt)
    {
        if(evt.CurId==2003)
        {
            gameObject.SetActive(true); 
            _canInteract = true;
        }
    }

    public override bool CanInteract()
    {
        return _canInteract;
    }

    public override void EnterTrigger(Transform tran)
    {
        base.EnterTrigger(tran);
    }

    public override void ExitTrigger(Transform tran)
    {
        base.ExitTrigger(tran);
    }

    public override void Interact()
    {
        
        DataModule.Resolve<GamePlayDM>().GenerateItemInBag(WorldType,1001,transform.position,_interactor);
    }


    public override float Priority()
    {
        return base.Priority();
    }
}