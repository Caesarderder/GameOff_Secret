using System;
using DG.Tweening;
using UnityEngine;

public enum EBagItemState
{
    Normal,
    InBag
}

public enum EBagItemInBagMode
{
    Idle,
    Move,
}
[RequireComponent(typeof(PlanetMovementBase))]
public class BagItem:Item ,IPlayerInteractable
{
    //˫��ȷ�ϻ���
    float _clickTime;
    protected EBagItemState _state=EBagItemState.Normal;
    
    //InBag
    protected EBagItemInBagMode _inBagMode=EBagItemInBagMode.Idle;
    protected BagItemFollowSpace _followSpace;
    private PlanetMovementBase _move;
    [SerializeField]
    protected float _followRange=0.1f;

    private Vector3 _followTargetPos => _followSpace.GetTargetPosByOffset(_followTargetOffset);

    protected Vector3 _followTargetOffset;

    protected override void Start()
    {
        base.Start();
        _move = GetComponent<PlanetMovementBase>();
        
        _move.Rb.isKinematic = true;
        ChangeState(EBagItemState.Normal);
    }

    public virtual void EnterTrigger(Transform tran)
    {
        SetInteractor(tran);
    }

    public virtual void ExitTrigger(Transform tran)
    {
        SetInteractor(tran);
    }

    public virtual bool CanInteract()
    {
        return _state== EBagItemState.Normal;
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
            _followSpace = _interactor.GetComponentInChildren<BagItemFollowSpace>();
            _inBagMode = EBagItemInBagMode.Idle;
            ChangeState(EBagItemState.InBag);
            Debug.Log(BelongWorld + "  " + ItemId +"add to bag");
        }
        
       

        //���ֲ�
        
        //�������
    }

    protected virtual void Update()
    {
        if (_state == EBagItemState.InBag)
        {
            if (_inBagMode == EBagItemInBagMode.Idle)
            {
                if (!_followSpace.CheckInSafeArea(transform.position, _followTargetPos, _followRange))
                {
                    _inBagMode = EBagItemInBagMode.Move;
                    DoMove();
                }
            }
            else if (_inBagMode == EBagItemInBagMode.Move)
            {
                if (_followSpace.CheckInSafeArea(transform.position, _followTargetPos, _followRange))
                {
                    _inBagMode = EBagItemInBagMode.Idle;
                    transform.DOKill();
                }
            }
        }
    }

    void ChangeState(EBagItemState state)
    {
        switch (state)
        {
            case EBagItemState.Normal:
                _move._canGravityMove = true;
                _move._useGravity= true;
                break;
            case EBagItemState.InBag:
                _move._canGravityMove= false;
                _move._useGravity= false;
                break;
            default:
                break;
        }

    }

    
    void DoMove()
    {
        _followTargetOffset = _followSpace.GetRandomOffset();
        transform.DOMove(_followTargetPos,1f).SetEase(Ease.OutBounce).OnComplete(DoMove);
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
            ChangeState(EBagItemState.Normal);

            //���ֲ�
            //���ڵ���
        }
    }

    public virtual float Priority()
    {
        if (_state == EBagItemState.InBag)
            return 0;
        if (_interactor != null)
        {
            return 100/Vector3.SqrMagnitude(transform.position-_interactor.position);
        }
        Debug.Log(ItemId+"  "+-1);
        return -1;
    }

    protected void FollowPlayer()
    {
        
    }
    
    
}