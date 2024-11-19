using UnityEngine;

public class IIPlane:InteractItem,IPlayerInteractable
{
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
        var penel=DataModule.Resolve<GamePlayDM>().GetGameRunePanel();
        penel.OpenHalf(BelongWorld);
    }


    public override float Priority()
    {
        return base.Priority();
    }
}