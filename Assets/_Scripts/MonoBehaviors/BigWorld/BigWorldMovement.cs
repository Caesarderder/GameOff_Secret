using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class BigWorldMovement : MonoBehaviour
{
    const float maxGravitySpeed=5f;
    const float GravityConst=9.8f;

    private Transform _planetCenter;
    private Transform _entity;

    #region Gravity

    bool
        _canGravityMove,
        _useGravity;
    float
        _gravitySpeed;
    Vector3
        _gravityDir => -_entity.position + _planetCenter.position;

    #endregion

    #region faceDir
    bool
        _canFaceDirMove;
    float
        _faceDirSpeed;
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
        _useGravity = true;
    }
    private void Update()
    {
        FixStandDir();
        ApplyGravity();
        FaceMove();
    }
    public void SetPlanetCenter(Transform tran) => _planetCenter = tran;

    public void SetFaceMoveSpeed(float velocity)
    {
        _faceDirSpeed = velocity;
    }    
    public void SetFaceMoveDir(Vector3 dir)
    {
        _curFaceMoveDir = dir.normalized;
    }    
    public void SetFaceMove(Vector3 dir)
    {
        _curFaceMoveDir = dir;
        _faceDirSpeed = dir.magnitude;
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
    }

    protected virtual void FaceMove()
    {
        _entity.transform.position += _curFaceMoveDir*_faceDirSpeed*Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if(_entity &&_planetCenter) 
        Gizmos.DrawLine(_entity.position, _entity.position + _faceDir * 3);
    }
}
