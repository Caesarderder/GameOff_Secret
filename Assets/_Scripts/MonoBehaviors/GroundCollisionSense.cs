using System;
using UnityEngine;

public class GroundCollisionSense : MonoBehaviour
{
    [SerializeField]
    protected LayerMask _wahtIsGround;
    [SerializeField]
    protected float _groundCheckDis = 0.2f;
    public bool IsGrounded;

    public Vector3 GravityDir;
    public Transform CheckPos;

    public void FixedUpdate()
    {
        GroundCheck();
    }
    public virtual void GroundCheck()
    {
        Transform from;
        if (!CheckPos)
            from = transform;
        else
        {
            from = CheckPos;
        }
        var hit = Physics.Raycast(from.position, GravityDir, _groundCheckDis, _wahtIsGround);

        if ( hit )
        {
            IsGrounded = true;
        }
        else
            IsGrounded = false;

    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawRay(CheckPos.position, GravityDir * _groundCheckDis);
    }

}
