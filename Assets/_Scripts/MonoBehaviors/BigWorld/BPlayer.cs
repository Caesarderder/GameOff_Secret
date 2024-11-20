using UnityEngine;

public class BPlayer : Player
{
    private BagManager bagManager;  // ����һ��˽���ֶ� bagManager

    private float lastClickTime = 0f;  // ��һ�ε����ʱ��
    private float doubleClickThreshold = 0.3f;  // ˫����������ʱ�䣨��λ���룩

    public override void OnInteractableTrigger(IPlayerInteractable interactable, bool isEnter)
    {
        base.OnInteractableTrigger(interactable, isEnter);
    }

    public override void OnInteractInput()
    {
        base.OnInteractInput();
    }


    protected override void Awake()
    {
        BelongWorld = EWorldType.B;
        base.Awake();
        Manager<InputManager>.Inst.InputButtonBinding(InputManager.CURSORRIGHT, x =>
              {
                  if ( x &&CanInput)
                  {
                      OnMoveCLick();
                  }

              });
        // ������������¼�������ʰȡ��Ʒ
        Manager<InputManager>.Inst.InputButtonBinding(InputManager.CURSORLEFT, x =>
        {
            if (x&&CanInput)  
            {
                OnInteractClick();
            }
        });
    }

    void OnMoveCLick()
    {
        if(Raycast(out var hit,LayerMask.GetMask("Ground")))
        {
            _movement.SetFaceTargetPos(hit.point);
            _movement.SetFaceRotation(hit.point);
            _movement.SetFaceMoveTargetSpeed(MoveSpeed);
        }
    }

    void OnInteractClick()
    {
        if(Raycast(out var hit,LayerMask.GetMask("Interactable")))
        {
            if(hit.transform.TryGetComponent<IPlayerInteractable>(out var interactable))
            {
                if ( _sense.Iteractables.Contains(interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }

    bool Raycast(out RaycastHit hit,LayerMask mask=default,float maxDis=100f)
    {
        var screenX= Screen.width;
        var screenY= Screen.height;
        if ( Input.mousePosition.x > screenX/2)
        {
            // ��ȡ�������Ļ�ϵ�λ�ò�����Ϊ�Ұ�������
            Vector3 localPos = Input.mousePosition - Vector3.right * screenX/2;

            // ʹ���Ҳ�����ӵ��λ�÷�������
            Ray ray = _camera.ScreenPointToRay(localPos);

            // ���߼����3D��Ʒ�Ľ���
            if(mask==default)
            {
                if ( Physics.Raycast(ray, out hit) )
                {

                    return true;
                }
            }
            else
            {
                if ( Physics.Raycast(ray, out hit, maxDis, mask) )
                {
                    return true;
                }
            }

        }
        hit = default;
        return false;
    }


    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

}
