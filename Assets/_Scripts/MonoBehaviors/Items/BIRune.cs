using DG.Tweening;
using System;
using UnityEngine;

public class BIRune:BagItem,IPlayerInteractable
{

    public override void Start()
    {
        base.Start();
    }

    public override void EnterTrigger(Transform tran)
    {
        base.EnterTrigger(tran);
    }

    public override void ExitTrigger(Transform tran)
    {
        base .ExitTrigger(tran);
    }

    public override bool CanInteract()
    {
        return _state== EBagItemState.Normal;
    }

    public override void Interact()
    {
        base.Interact();
    }

    protected override void PickUp()
    {
        base.PickUp();
    }

    protected override void Update()
    {
        base.Update();
    }


    public override void DropDown()
    {
        base.DropDown();
    }

    public override float Priority()
    {
        return base.Priority();
    }

    //移动至目标位置，并且从背包中删除，移动后销户物品,解锁符文
    public override void Use(Vector3 pos,Action callBack)
    {
        base.Use(pos,callBack);
        //数据层解锁符文
        DataModule.Resolve<GameStatusDM>().UpdateRuneState(ItemId, true);
    }

}