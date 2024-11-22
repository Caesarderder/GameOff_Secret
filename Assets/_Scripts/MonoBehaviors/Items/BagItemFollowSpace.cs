using UnityEngine;

public class BagItemFollowSpace : MonoBehaviour
{
    [SerializeField] private Bounds _bound;

    public Vector3 GetTargetPosByOffset(Vector3 offset)
    {

        return transform.position + offset;
        
    }
    public Vector3 GetRandomOffset()
    {
        return new Vector3(
            UnityEngine.Random.Range(-_bound.extents.x, _bound.extents.x),
            UnityEngine.Random.Range(-_bound.extents.y, _bound.extents.y),
            UnityEngine.Random.Range(-_bound.extents.z, _bound.extents.z)
        );
    }

    public bool CheckInSafeArea(Vector3 position,Vector3 followPos,float safeRange=0.5f)
    {
        return _bound.Contains(position)&&Vector3.Distance(position,followPos)>safeRange;
    }
    
    
    private void OnDrawGizmos()
    {
        _bound.center = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_bound.center, _bound.size);
    }
}
