using Unity.VisualScripting;
using UnityEngine;

public class CWPlayer : Player
{

    Camera _cam;
    public override void OnInteractableTrigger(IPlayerInteractable interactable, bool isEnter)
    {
        base.OnInteractableTrigger(interactable, isEnter);
    }

    public override void OnInteractInput()
    {
        base.OnInteractInput();
    }

    public void Init(Camera cam)
    {
        _cam = cam;
    }

    protected override void Awake()
    {
        base.Awake();
        Manager<InputManager>.Inst.InputButtonBinding(InputManager.CURSORRIGHT, x =>
              {
                  if ( x )
                  {
                      OnMoveCLick();
                  }
              });
    }

    void OnMoveCLick()
    {
        if ( Input.mousePosition.x > 960)
        {
            // ��ȡ�������Ļ�ϵ�λ�ò�����Ϊ�Ұ�������
            Vector3 localPos = Input.mousePosition - Vector3.right * 960f;

            // ʹ���Ҳ�����ӵ��λ�÷�������
            Ray ray = _cam.ScreenPointToRay(localPos);

            // ���߼����3D��Ʒ�Ľ���
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("yes");
                if ( hit.transform.tag == "Ground" )
                {
                    Debug.Log("Move");
                    _movement.SetFaceTargetPos(hit.point); 
                    _movement.SetFaceRotation(hit.point); 
                    _movement.SetFaceMoveTargetSpeed(MoveSpeed); 
                }
            }
        }

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
