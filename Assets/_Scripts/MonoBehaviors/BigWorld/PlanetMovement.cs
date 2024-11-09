using UnityEngine;

[RequireComponent (typeof(GroundCollisionSense))]
public class PlanetMovement : MonoBehaviour
{
    const float maxGravitySpeed=5f;
    const float GravityConst=9.8f;

    private Transform _planetCenter;
    private Transform _entity;

    GroundCollisionSense _groundSense;
    Rigidbody _rb;

    #region Gravity

    bool
        _canGravityMove,
        _useGravity;
    float
        _gravitySpeed;
    Vector3
        _gravityDir => (-_entity.position + _planetCenter.position).normalized;

    #endregion

    #region faceDir
    bool
        _canFaceDirMove;
    float
        _faceSpeed;
    Vector3
        _curFaceMoveDir;

     Vector3 
        _faceDir
    {
        get
        {
            var gravityDir = _gravityDir;
            Vector3 parallelComponent = Vector3.Dot(_entity.forward, gravityDir) * gravityDir;
            Vector3 projection = _entity.forward - parallelComponent;
            return projection.normalized;
        }
    }

    #endregion

    private void Awake()
    {
        _entity = transform;
        _rb = GetComponent<Rigidbody>();
        _groundSense = GetComponent<GroundCollisionSense>();
        _useGravity = true;
        _canGravityMove= true;
        _rb.isKinematic = false;
        _rb.useGravity = false;
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        FixStandDir();
        FixVelocity();
        ApplyGravity();
        FaceMove();
    }
    void FixVelocity()
    {
        var velocity = _rb.linearVelocity;
        _gravitySpeed=GravityUtil.GetGravitySpeed(velocity,_gravityDir);
        _faceSpeed=GravityUtil.GetFaceVector(velocity,_gravityDir).magnitude;

        _groundSense.GravityDir = _gravityDir;
    }

    public void SetPlanetCenter(Transform tran) => _planetCenter = tran;

    public void SetFaceMoveSpeed(float velocity)
    {
        _faceSpeed = velocity;
    }    
    public void SetFaceMoveDir(Vector3 dir)
    {
        _curFaceMoveDir = dir.normalized;
    }    
    public void SetFaceMove(Vector3 dir)
    {
        _curFaceMoveDir = dir;
        _faceSpeed = dir.magnitude;
        _curFaceMoveDir.Normalize();
    }    

    protected virtual void FixStandDir()
    {
        transform.up = -_gravityDir;
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
            _rb.linearVelocity = GravityUtil.GetFaceVector(velocity, _gravityDir)-_gravityDir*_gravitySpeed;
        }
    }

    protected virtual void FaceMove()
    {
        //_entity.transform.position += _curFaceMoveDir*_faceSpeed*Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if(_entity &&_planetCenter)
        {
            Gizmos.DrawLine(_entity.position, _entity.position + _faceDir * 3);

            Gizmos.color = Color.red;
            var velocity = _rb.linearVelocity;
            Gizmos.DrawLine(_entity.position, _entity.position + GravityUtil.GetFaceVector(velocity, _gravityDir));
            Gizmos.DrawLine(_entity.position, _entity.position + GravityUtil.GetFaceVector(velocity,_gravityDir * _gravitySpeed));

        }


    }
}
