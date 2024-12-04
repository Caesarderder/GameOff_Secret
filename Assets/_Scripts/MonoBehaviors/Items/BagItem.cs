using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

public enum EBagItemState
{
    Normal,
    InBag,
    Using,
}

public enum EBagItemInBagMode
{
    Idle,
    Move,
}
public class BagItem:Item ,IPlayerInteractable
{
    //˫��ȷ�ϻ���
    float _clickTime;
    protected EBagItemState _state=EBagItemState.Normal;
    
    //InBag
    protected EBagItemInBagMode _inBagMode=EBagItemInBagMode.Idle;
    protected BagItemFollowSpace _followSpace;
    //private PlanetMovementBase _move;
    [SerializeField]
    protected float _followRange=0.1f;
    Collider[] _colliders;

    private Vector3 _followTargetPos => _followSpace.GetTargetPosByOffset(_followTargetOffset);

    protected Vector3 _followTargetOffset;

    public override void Start()
    {
        base.Start();
        //_move = GetComponent<PlanetMovementBase>();
        //_move.IsTargetMoving = false;
        _colliders = GetComponentsInChildren<Collider>();
        //ChangeState(EBagItemState.Normal);
    }

    public virtual void EnterTrigger(Transform tran)
    {
        SetInteractor(tran);
    }

    public virtual void ExitTrigger(Transform tran)
    {
        Debug.Log("???");
        SetInteractor(tran);
    }

    public virtual bool CanInteract()
    {
        return _state== EBagItemState.Normal;
    }

    public virtual void Interact()
    {
        Debug.Log(WorldType+" Itemact :"+ItemId);
        if(_state==EBagItemState.Normal)
        {

            var dm = DataModule.Resolve<GamePlayDM>();

            //��ǰ��������Ʒ
            if ( dm.GetCurBagItemId(BelongWorld) != 0 )
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
                    DoInBagMove();
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
        transform.DOKill();
        switch ( state)
        {
            case EBagItemState.Normal:
                //_move._canGravityMove = true;
                //_move._useGravity= true;
                SetCollider(true);
                break;
            case EBagItemState.InBag:
                if(this.GetType() is BIRune)
                { }
                    else
                    Manager<AudioManager>.Inst.PlaySoundEffect(8);
                //_move._canGravityMove= false;
                //_move._useGravity= false;
                SetCollider(false);
                break;
                case EBagItemState.Using:
                    Manager<AudioManager>.Inst.PlaySoundEffect(8);
                break;
            default:
                break;
        }
        _state = state;

    }

    void SetCollider(bool enable)
    {
        foreach (var item in _colliders)
        {
            item.enabled = enable;
        }
    }
    
    void DoInBagMove()
    {
        if(_state==EBagItemState.InBag)
        {
            _followTargetOffset = _followSpace.GetRandomOffset();
            transform.DOMove(_followTargetPos, 0.5f).SetEase(Ease.InOutCirc).OnComplete(DoInBagMove);

        }
    }
    void DoNormalMove()
    {
        if(_state==EBagItemState.Normal)
        {
            var center = DataModule.Resolve<GamePlayDM>().GetPlanetCenter(WorldType);
            if ( Physics.Raycast(transform.position, center.position-transform.position,out var hit) ) 
            {
                transform.DOMove(hit.point, 0.5f).SetEase(Ease.InOutCirc);
            }
        }
    }

    public virtual void DropDown()
    {
        if(_state==EBagItemState.InBag)
        {
            var dm = DataModule.Resolve<GamePlayDM>();
            //���ݲ�
            //�ӱ����Ƴ�
            dm.TryRemoveItem(BelongWorld);
            ChangeState(EBagItemState.Normal);

            //���ֲ�
            //���ڵ���
        }
    }

    public virtual float Priority()
    {
        if ( transform == null )
            return -10;
        if (_state == EBagItemState.InBag)
        {

            return 0;
        }
        if ( _interactor != null )
        {
            return 100 / Vector3.SqrMagnitude(transform.position - _interactor.position);
        }
        return -1;
    }

    //移动至目标位置，并且从背包中删除，移动后销户物品
    public virtual void Use(Vector3 pos, Action callBack)
    {
        ChangeState(EBagItemState.Using);
        transform.DOMove(pos, 1f).SetEase(Ease.OutCirc).OnComplete(()=> { 
            callBack?.Invoke();
            EventAggregator.Publish(new CollisionRemoveEvent()
            {
                WorldType= WorldType,
                interactable = this
            });
            Destroy(gameObject);
        });
        DataModule.Resolve<GamePlayDM>().TryRemoveItem(WorldType);
    }

    

}
public struct CollisionRemoveEvent
{
    public EWorldType WorldType;
    public IPlayerInteractable interactable;
}

