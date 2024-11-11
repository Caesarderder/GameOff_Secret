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
        // ����������ϵ�еķ�������ת��Ϊ��Ҿֲ�����ϵ�еķ�������
        localDirection = transform.InverseTransformDirection(_moveInput);

        // ���ֲ�����ϵ�е�z��������Ϊ0��ȷ����ֻ��xyƽ����
        localDirection.z = 0;
        localDirection.Normalize();

        // ת��ΪVector2����ʾ�ֲ�����ϵ�е�xyƽ�淽��
        Vector2 local2D = new Vector2(localDirection.x, localDirection.y);

        // ��������������ҵ��ƶ��ٶ�
        _movement.SetFaceMove(local2D.normalized* MoveSpeed);

        if(_moveInput!=Vector2.zero)
        {
            test.transform.up = worldInput;
        }

        // �������
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




