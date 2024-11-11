using UnityEngine;

public class KWPlayer : Player
{
    InputDataModule inputDataModule;
    InteractableSense _sense;
    public float MoveSpeed=5f;
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
        var worldInput = new Vector3(_moveInput.x,  _moveInput.y,0f);
        // 将世界坐标系中的方向向量转换为玩家局部坐标系中的方向向量
        localDirection = transform.InverseTransformDirection(_moveInput);

        // 将局部坐标系中的z分量设置为0，确保它只在xy平面内
        localDirection.z = 0;
        localDirection.Normalize();

        // 转换为Vector2，表示局部坐标系中的xy平面方向
        Vector2 local2D = new Vector2(localDirection.x, localDirection.y);

        // 调整方向并设置玩家的移动速度
        _movement.SetFaceMove(local2D.normalized* MoveSpeed);

        if(_moveInput!=Vector2.zero)
        {
            test.transform.up = worldInput;
        }

        // 调试输出
        Debug.Log($"old:{_moveInput} new {local2D}");


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




