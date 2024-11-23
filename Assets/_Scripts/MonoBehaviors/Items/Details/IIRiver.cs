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

    public override async void Interact()
    {
        var planet=DataModule.Resolve<GamePlayDM>().GetPlanet(WorldType);
        var bagItem = await Manager<ResManager>.Inst.LoadGo<BagItem>("item_1001", planet.transform,transform.position);
        bagItem.Start();
        bagItem.EnterTrigger(_interactor);
        bagItem.Interact();
    }


    public override float Priority()
    {
        return base.Priority();
    }
}