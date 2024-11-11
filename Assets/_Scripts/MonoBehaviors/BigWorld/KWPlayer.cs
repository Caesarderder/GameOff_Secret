using UnityEngine;
using UnityEngine.InputSystem;

public class KWPlayer : Player
{
    InputDataModule inputDataModule;
    Camera _camera;
    Vector2 _moveInput;

    //Test
    Vector3 localDirection;
    [SerializeField]
    GameObject test;

    protected override void Awake()
    {
        base.Awake();
        inputDataModule = DataModule.Resolve<InputDataModule>();
        Manager<InputManager>.Inst.InputButtonBinding(InputManager.INTERACT, (isPress) =>
        {
            if ( isPress )
                OnInteractInput();

        });
        Manager<InputManager>.Inst.InputValueBinding<Vector2>(InputManager.MOVE,(isPress, data) =>
        {
            ProcessMoveInput(data);
            //SetFaceRotation(_moveInput.normalized );
        });
    }

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    private void ProcessMoveInput(Vector2 data)
    {
        _moveInput = data;

        if(_moveInput!=Vector2.zero)
        {
            Vector3 local3D = _movement.PlanetCenter.position + _camera.transform.up * _moveInput.y * 10f + _camera.transform.right * _moveInput.x * 10f;

            test.transform.up = local3D - test.transform.position;
            // 调整方向并设置玩家的移动速度
            _movement.SetFaceTargetPos(local3D);
            _movement.SetFaceMoveTargetSpeed(MoveSpeed);
            _movement.SetFaceRotation(local3D);
            Debug.Log("MOve To" + local3D);
        }
        else
        {
            //_movement.SetFaceTargetPos(_movement.transform.position);
            _movement.SetFaceMoveTargetSpeed(0f);
            _movement.DisableFaceRotation();
        }

    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnInteractableTrigger(IPlayerInteractable interactable, bool isEnter)
    {
        base.OnInteractableTrigger(interactable, isEnter);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + localDirection * 2f);
        Gizmos.color = Color.black;
        var dir = localDirection;
        dir.z = 0;
        Gizmos.DrawLine(transform.position, transform.position +  dir* 2f);
    }
}




