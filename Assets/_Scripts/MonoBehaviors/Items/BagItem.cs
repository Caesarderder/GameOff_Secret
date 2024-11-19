using UnityEngine;

public enum EBagItemState
{
    Normal,
    InBag
}
public class BagItem:Item ,IPlayerInteractable
{
    //˫��ȷ�ϻ���
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

            //��ǰ��������Ʒ
            if ( dm.GetCurBagItem(BelongWorld) != 0 )
            {
                if ( _clickTime + 0.5f > Time.time )
                {
                    //������ǰ������Ʒ
                    if ( dm.TryRemoveItem(BelongWorld) )
                    {
                        //�ɹ�����,ʰȡ���
                        PickUp();
                    }
                }
                else
                {
                    //��Ҫ0.5s��˫����ȷ��ʰȡ
                    _clickTime = Time.time;
                    return;
                }
            }
            else
            {
                //��ǰ����û����Ʒ��ֱ��ʰȡ���item
                PickUp();
            }
        }
    }

    protected virtual void PickUp()
    {
        var dm = DataModule.Resolve<GamePlayDM>();
        //���ݲ�
        //���뱳��
        if(dm.TryAddItem(this))
        {
            _state = EBagItemState.InBag;
            Debug.Log(BelongWorld + "  " + ItemId +"add to bag");
        }
        
       

        //���ֲ�
        //�������
    }

    public virtual void DorpDown()
    {
        if(_state==EBagItemState.InBag)
        {
            var dm = DataModule.Resolve<GamePlayDM>();
            //���ݲ�
            //�ӱ����Ƴ�
            dm.TryRemoveItem(BelongWorld);
            Debug.Log(BelongWorld + "  " + ItemId +"remove to bag");
            _state = EBagItemState.Normal;

            //���ֲ�
            //���ڵ���
        }
    }

    public virtual float Priority()
    {
        if (_interactor != null)
        {
            return Vector3.SqrMagnitude(transform.position-_interactor.position);
        }
        Debug.Log(ItemId+"  "+-1);
        return -1;
    }
}