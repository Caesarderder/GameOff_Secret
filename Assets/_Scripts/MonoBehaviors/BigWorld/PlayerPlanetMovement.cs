using UnityEngine;

public class PlayerPlanetMovement : PlanetMovementBase,IWorldObject
{
    public float ScreenSafeRadius=5f;
    EWorldType _worldType;
    public EWorldType WorldType { get=>_worldType; set { Debug.Log("setWOrld!!!!"); _worldType=value; }  }

    Camera _camera;
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
            //����ƶ�
            Rb.linearVelocity = Velocity;
        }
        else
        {

            Rb.linearVelocity = _tempGravityVelocity;
            EventAggregator.Publish(new PlanetRoatateEvent()
            {
                WorldType = _worldType,
                RotateAxis =-Vector3.Cross(_faceMoveDir3,_gravityDir).normalized,
                Intensity= _tempFaceVelocity.magnitude/maxFaceSpeed,
            });
        }

    }

    private bool CheckScreenSafe()
    {
        if(Raycast(out var hit))
        {
            //�ҵ���Ļ����������ĵ�
            var center=_camera.WorldToScreenPoint(hit.point); 
            var curPos=_camera.WorldToScreenPoint(_entity.position);
            var curDis = Vector3.Distance(center, curPos);
            Debug.Log("��������"+curDis);
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
                    Debug.Log("�������ƶ�");
                    return true;
                }
                else
                {
                    Debug.Log("Σ���ƶ�");
                    return false;
                }
            }
        }
        else
        {
            Debug.Log("δ�ҵ�����Σ���ƶ�");
            return false;
        }
      

        //��ȡ��������Ļ����

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
