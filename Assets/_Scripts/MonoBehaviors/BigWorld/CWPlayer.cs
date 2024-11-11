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
            // 获取鼠标在屏幕上的位置并调整为右半屏坐标
            Vector3 localPos = Input.mousePosition - Vector3.right * 960f;

            // 使用右侧相机从点击位置发射射线
            Ray ray = _cam.ScreenPointToRay(localPos);

            // 射线检测与3D物品的交互
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
