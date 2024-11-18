using UnityEngine;

public enum EBagItemState
{
    Normal,
    InBag
}
public class BagItem:Item ,IPlayerInteractable
{
    //双击确认机制
    float _clickTime;
    EBagItemState _state=EBagItemState.Normal;

    public virtual void EnterTrigger(Transform tran)
    {
        SetInteractor(tran);
    }

    public virtual void ExitTrigger(Transform tran)
    {
        SetInteractor(tran);
    }

    public virtual void Interact()
    {
        if(_state==EBagItemState.Normal)
        {

            var dm = DataModule.Resolve<GamePlayDM>();

            //当前背包有物品
            if ( dm.GetCurBagItem(BelongWorld) == 0 )
            {
                if ( _clickTime + 0.5f > Time.time )
                {
                    //丢弃当前背包物品
                    if ( dm.TryRemoveItem(BelongWorld) )
                    {
                        //成功丢弃,拾取这个
                        PickUp();
                    }
                }
                else
                {
                    //需要0.5s内双击以确认拾取
                    _clickTime = Time.time;
                    return;
                }
            }
            else
            {
                //当前背包没有物品，直接拾取这个item
                PickUp();
            }
        }
    }

    protected virtual void PickUp()
    {
        var dm = DataModule.Resolve<GamePlayDM>();
        //数据层
        //放入背包
        if(dm.TryAddItem(this))
        {
            _state = EBagItemState.InBag;
        }
        
       

        //表现层
        //跟随玩家
    }

    public virtual void DorpDown()
    {
        if(_state==EBagItemState.InBag)
        {

            var dm = DataModule.Resolve<GamePlayDM>();
            //数据层
            //从背包移除
            dm.TryRemoveItem(BelongWorld);

            //表现层
            //丢在地上
        }
    }

    public virtual float Priority()
    {
        if(_interactor != null)
        return Vector3.SqrMagnitude(transform.position-_interactor.position);
        return -1;
    }
}