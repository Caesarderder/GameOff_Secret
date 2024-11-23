using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(GroundCollisionSense))]
[RequireComponent(typeof(Rigidbody))]
public class PlanetMovementBase : MonoBehaviour ,IWorldObject
{
    public float maxGravitySpeed =3f;
    public float maxFaceSpeed=1f;
    public float GravityConst = 5f;
    public float SpeedChangeRate = 1f;
    public float RotateSpeed= 10f;

    public Transform PlanetCenter;
    protected Transform _entity;

    protected GroundCollisionSense _groundSense;
     public  Rigidbody Rb;

    protected Vector3 _tempFaceVelocity;
    protected Vector3 _tempGravityVelocity;
    public Vector3 Velocity=>_tempFaceVelocity+_tempGravityVelocity;
    #region Gravity

    public bool
        _canGravityMove,
        _useGravity;
    protected float
        _gravitySpeed;

    protected Vector3
        _gravityDir {
        get
        {
            if (PlanetCenter)
            {
                
                return  ( -_entity.position + PlanetCenter.position ).normalized;
            }
            else
            {
                return Vector3.zero;
            }
        }
        
    }


    #endregion

    #region faceDir
    public bool IsTargetMoving;
    protected bool
        _isRotating;
    protected bool
        _canFaceDirMove;

    private float 
        _faceCurSpeed,
        _faceCurRotation;

    //��縳ֵ
    protected Vector3
        _faceTargetPos;
    protected float
        _faceTargetSpeed;
    protected Vector3
        _faceTargetDir;

    //�ڲ�����
    protected Vector2
        _faceMoveLocalDir2;
    protected Vector3
        _faceMoveDir3;

    #endregion
    protected Vector3
        _lastGravityDir;
    private float _tempRotation;
    Vector3 _tempUp;
    Vector3 _tempForward;

    private void Awake()
    {
        _entity = transform;
        Rb = GetComponent<Rigidbody>();
        _groundSense = GetComponent<GroundCollisionSense>();
        Rb.useGravity = false;
        Rb.isKinematic = false;
        _useGravity = true;
        _canGravityMove = true;
        if (DataModule.Resolve<GameStatusDM>().CurGameState == EGameState.GamePlay1)
        {
            
            Init();
            _isRotating = true;
            _canFaceDirMove = true;
            IsTargetMoving=true;
        }
    }

    protected virtual void Start()
    {
        


    }
    protected virtual private void Update()
    {
    }
    protected virtual void FixedUpdate()
    {
        FixStandDir();
        FixVelocity();
        ApplyGravity();
        if(IsTargetMoving)
        {
            FixFaceMoveDir();
            CaculateFaceMoveSpeed();
        }
        FaceRotate();
        Move();
    }
    void FixVelocity()
    {
        var velocity = Rb.linearVelocity;
        _gravitySpeed = GravityUtil.GetGravitySpeed(velocity, _gravityDir);
        _groundSense.GravityDir = _gravityDir;
    }


    public void SetFaceMoveTargetSpeed(float velocity)
    {
        _faceTargetSpeed = velocity;
    }    
    public void SetFaceTargetPos(Vector3 pos)
    {
        IsTargetMoving = true;
        _faceTargetPos= pos;
    }
    
    public void Stop()
    {
        SetFaceMoveTargetSpeed(0f);
        SetFaceTargetPos(_entity.position);
        SetFaceRotation(_entity.transform.forward);
    }

    public void SetFaceRotation(Vector3 dir)
    {
        _faceTargetDir = dir;
        _isRotating = true;
    }
    public void DisableFaceRotation()
    {
        _isRotating = false;
    }


    protected virtual void FixStandDir()
    {
        // ����һ������gravityDir����ת
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -_gravityDir) * transform.rotation;

        // Ӧ����ת
        transform.rotation = targetRotation;
    }

    protected virtual void FixFaceMoveDir()
    {
        //�ƶ�������Ŀ��λ��
        //var localDir= transform.InverseTransformDirection(_faceTargetPos);
        //localDir.z = 0f;

        _faceMoveDir3 = GravityUtil.GetFaceMoveDir(_faceTargetPos-_entity.transform.position,_entity).normalized ;
        
    }

    protected virtual void ApplyGravity()
    {
        if ( _useGravity )
        {
            if ( _gravitySpeed> -maxGravitySpeed )
            {
                _gravitySpeed-= GravityConst* Time.deltaTime;
            }
            else
            {
                _gravitySpeed=-maxGravitySpeed;
            }
        }
        if ( _groundSense.IsGrounded )
            _gravitySpeed = 0f;
        if(_canGravityMove)
        {
            var velocity = Rb.linearVelocity;

            _tempGravityVelocity = -_lastGravityDir*_gravitySpeed;
        }

        _lastGravityDir = _gravityDir;
    }

    protected virtual void FaceRotate()
    {
        if ( _isRotating )
        {
            // ��ȡ��ǰ�ķ��򲢲�ֵ��Ŀ�귽��
            Vector3 smoothedDirection = Vector3.Lerp(transform.forward, _faceMoveDir3, RotateSpeed* Time.deltaTime);

            Quaternion targetRotation = Quaternion.FromToRotation(transform.forward, smoothedDirection) * transform.rotation;

            transform.rotation = targetRotation;
        }

    }

    protected virtual void CaculateFaceMoveSpeed()
    {

        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        if ( _faceCurSpeed < _faceTargetSpeed - speedOffset
             )
        {
            _faceCurSpeed = Mathf.Lerp(_faceCurSpeed, _faceTargetSpeed,
                Time.deltaTime * SpeedChangeRate);

            _faceCurSpeed = Mathf.Round(_faceCurSpeed * 1000f) / 1000f;
        }
        else if(_faceCurSpeed > _faceTargetSpeed + speedOffset)
        {
            _faceCurSpeed = Mathf.Lerp(_faceCurSpeed, _faceTargetSpeed,
                Time.deltaTime * SpeedChangeRate*4f);

            // round speed to 3 decimal places
            _faceCurSpeed = Mathf.Round(_faceCurSpeed * 1000f) / 1000f;
        }
        else
        {
            _faceCurSpeed = _faceTargetSpeed;
        }

        if(IsTargetMoving)
        {
            if(Vector3.Angle(_faceTargetPos-PlanetCenter.position,-_gravityDir)<2f)
            {
                IsTargetMoving = false;
                _faceCurSpeed = 0f;
            }
        }    

        _tempFaceVelocity = _faceMoveDir3.normalized*_faceCurSpeed;
    }

    protected virtual void Move()
    {
        Rb.linearVelocity = _tempFaceVelocity+_tempFaceVelocity;
    }
    private void OnDrawGizmos()
    {
        if(_entity &&PlanetCenter)
        {
            Gizmos.color = Color.green;
            var velocity = Rb.linearVelocity;
            Gizmos.DrawLine(_entity.position, _entity.position + _faceMoveDir3*_faceCurSpeed);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_entity.position, _entity.position +GravityUtil.GetFaceMoveVelocity(_faceMoveLocalDir2, _entity)*_faceTargetSpeed);
            ;
        }


    }

    public void Init()
    {
        
        var world= GetComponentInParent<WorldAct>();
        if ( world )
        {
            WorldType = world.WorldType;
            PlanetCenter = DataModule.Resolve<GamePlayDM>().GetPlanet(WorldType).transform;
            _lastGravityDir = _gravityDir;
        }
        
    }

    public EWorldType WorldType { get; set; }
}
