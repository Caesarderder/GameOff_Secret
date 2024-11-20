using UnityEngine;

public class PlayerPlanetMovement : PlanetMovementBase,IWorldObject
{
    public float ScreenSafeRadius=5f;
    EWorldType _worldType;
    public EWorldType WorldType { get=>_worldType; set { Debug.Log("setWOrld!!!!"); _worldType=value; }  }

    Camera _camera;
    protected override void Awake()
    {
        base.Awake();
    }
    public void Init()
    {
        var world= GetComponentInParent<WorldAct>();
        if ( world )
        {
            WorldType = world.WorldType;
        }
        else
            Debug.LogError("No world?");
    }

    void Start()
    {
        Init();
        _camera = DataModule.Resolve<GamePlayDM>().GetWorld(WorldType).Camera;
    }
    protected override void CaculateFaceMoveSpeed()
    {
        base.CaculateFaceMoveSpeed();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private protected override void Update()
    {
        base.Update();
    }

    protected override void FaceMove()
    {
        if(CheckScreenSafe())
        {
            //玩家移动
            _rb.linearVelocity = Velocity;
        }
        else
        {

            _rb.linearVelocity = _tempGravityVelocity;
            //星球旋转
        }

    }

    private bool CheckScreenSafe()
    {
        if(Raycast(out var hit))
        {
            //找到屏幕中星球的中心点
            var center=_camera.WorldToScreenPoint(hit.point); 
            var curPos=_camera.WorldToScreenPoint(_entity.position);
            var curDis = Vector3.Distance(center, curPos);
            Debug.Log("距离中心"+curDis);
            if ( curDis<=ScreenSafeRadius)
            {
                return true;
            }
            else
            {
                var angle=Vector3.Angle(_faceMoveDir3,  hit.point - _entity.position);
                Debug.Log(angle);
                if ( angle<=90f)
                {
                    Debug.Log("向中心移动");
                    return true;
                }
                else
                {
                    Debug.Log("危险移动");
                    return false;
                }
            }
        }
        else
        {
            Debug.Log("未找到星球危险移动");
            return false;
        }
      

        //获取这个点的屏幕坐标

    }
    bool Raycast(out RaycastHit hit, float maxDis = 100f)
    {

        var tran = _camera.transform;
        if(Physics.Raycast(tran.position,tran.forward,out hit,maxDis,LayerMask.GetMask("Ground")))
        {
            return true;
        }

        hit = default;
        return false;

    }


}
