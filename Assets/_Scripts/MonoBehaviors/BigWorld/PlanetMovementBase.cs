using UnityEngine;

[RequireComponent(typeof(GroundCollisionSense))]
public class PlanetMovementBase : MonoBehaviour
{
    public float maxGravitySpeed = 5f;
    public float GravityConst = 9.8f;
    public float SpeedChangeRate = 0.6f;
    public float RotateSpeed= 0.5f;
    public float TestRotate= 20f;

    protected Transform _planetCenter;
    protected Transform _entity;

    protected GroundCollisionSense _groundSense;
    protected Rigidbody _rb;

    protected Vector3 _tempVelocity;
    #region Gravity

    protected bool
        _canGravityMove,
        _useGravity;
    protected float
        _gravitySpeed;
    protected Vector3
        _gravityDir => ( -_entity.position + _planetCenter.position ).normalized;

    #endregion

    #region faceDir
    protected bool
        _canRotate;
    protected bool
        _canFaceDirMove;

    private float 
        _faceCurSpeed,
        _faceCurRotation;

    protected float
        _faceTargetSpeed;
    protected Vector2
        _faceTargetDir;
    protected Vector2
        _faceMoveDir2;
    protected Vector3
        _faceMoveDir3;

    #endregion
    protected Vector3
        _lastGravityDir;
    private float _tempRotation;
    Vector3 _tempUp;
    Vector3 _tempForward;

    protected virtual void Awake()
    {
        _entity = transform;
        _rb = GetComponent<Rigidbody>();
        _groundSense = GetComponent<GroundCollisionSense>();
        _useGravity = true;
        _canGravityMove = true;
        _canRotate = true;
        _canFaceDirMove = true;
        _rb.isKinematic = false;
        _rb.useGravity = false;

    }
    protected virtual private void Update()
    {
    }
    protected virtual void FixedUpdate()
    {
        FixStandDir();
        FixVelocity();
        ApplyGravity();
        FaceRotate();
        FaceMove();
        _rb.linearVelocity = _tempVelocity;
    }
    void FixVelocity()
    {
        var velocity = _rb.linearVelocity;
        _gravitySpeed = GravityUtil.GetGravitySpeed(velocity, _gravityDir);
        _groundSense.GravityDir = _gravityDir;
    }

    public void SetPlanetCenter(Transform tran)  {
        _planetCenter = tran;
        _lastGravityDir = _gravityDir;
} 
    public void SetFaceMoveTargetSpeed(float velocity)
    {
        _faceTargetSpeed = velocity;
    }    
    public void SetFaceMoveDir(Vector2 dir)
    {
        _faceMoveDir2= dir.normalized;
    }    
    public void SetFaceMove(Vector2 dir)
    {
        _faceMoveDir2= dir;
        _faceTargetSpeed =dir.magnitude;
        _faceMoveDir2.Normalize();
    }    
    public void SetFaceRotation(Vector2 dir)
    {
        _faceTargetDir = dir;
    }

    protected virtual void FixStandDir()
    {
        //var targetUpDirection=-_gravityDir;
        //// 获取当前的水平 forward 方向（消除与 up 轴的影响）
        //Vector3 forwardOnPlane = Vector3.ProjectOnPlane(transform.forward, targetUpDirection).normalized;

        //// 设置新的旋转，使得 up 轴指向 targetUpDirection，同时保持水平朝向
        //transform.rotation = Quaternion.LookRotation(forwardOnPlane, targetUpDirection);
        //_tempUp= -_gravityDir;

        // 计算一个朝向gravityDir的旋转
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -_gravityDir) * transform.rotation;

        // 应用旋转
        transform.rotation = targetRotation;
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
            var velocity = _rb.linearVelocity;

            _tempVelocity = -_lastGravityDir*_gravitySpeed;
        }

        _lastGravityDir = _gravityDir;
    }

    protected virtual void FaceRotate()
    {
        if ( _canRotate )
        {
            //// 1. 将Vector2输入转换为基于transform.forward和transform.right的3D方向
            Vector3 targetDirection = GravityUtil.GetFaceMoveVelocity(_faceTargetDir, _entity);

            //// 2. 计算目标旋转，使用LookRotation使forward朝向targetDirection，同时保留up方向指向引力方向
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, _entity.forward);

            //// 3. 使用插值旋转逐渐向目标方向旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
            Debug.Log("???");
            //transform.Rotate(transform.up, TestRotate * Time.deltaTime);
        }
        // 使用 LookRotation 创建一个旋转，确保 up 方向符合 newUp
        //transform.rotation = Quaternion.LookRotation(_tempForward, -_gravityDir);

    }

    protected virtual void FaceMove()
    {
        //var velocity= _rb.linearVelocity;
        // a reference to the players current horizontal velocity
        //_faceCurSpeed= GravityUtil.GetFaceVector(velocity,_gravityDir).magnitude;

        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        if ( _faceCurSpeed < _faceTargetSpeed - speedOffset ||
            _faceCurSpeed > _faceTargetSpeed + speedOffset )
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _faceCurSpeed = Mathf.Lerp(_faceCurSpeed, _faceTargetSpeed,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _faceCurSpeed = Mathf.Round(_faceCurSpeed * 1000f) / 1000f;
        }
        else
        {
            _faceCurSpeed = _faceTargetSpeed;
        }

        //_faceCurSpeed = _faceTargetSpeed;

         _faceMoveDir3=GravityUtil.GetFaceMoveVelocity(_faceMoveDir2,_entity);
        _tempVelocity += _faceMoveDir3.normalized*_faceCurSpeed;
    }

    private void OnDrawGizmos()
    {
        if(_entity &&_planetCenter)
        {
            Gizmos.color = Color.green;
            var velocity = _rb.linearVelocity;
            Gizmos.DrawLine(_entity.position, _entity.position + _faceMoveDir3*_faceCurSpeed);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_entity.position, _entity.position +GravityUtil.GetFaceMoveVelocity(_faceMoveDir2, _entity)*_faceTargetSpeed);
            ;
        }


    }
}
