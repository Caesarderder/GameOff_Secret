using Unity.VisualScripting;
using UnityEngine;

public class CWPlayer : Player
{
    private BagManager bagManager;  // 声明一个私有字段 bagManager

    private float lastClickTime = 0f;  // 上一次点击的时间
    private float doubleClickThreshold = 0.3f;  // 双击间隔的最大时间（单位：秒）

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

        // 初始化玩家背包
        Intent playerIntent = Intent.Get();
        bagManager = new BagManager(playerIntent);
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
        // 绑定鼠标左键点击事件，用于拾取物品
        Manager<InputManager>.Inst.InputButtonBinding("MouseLeft", x =>
        {
            if (x)  // 按下鼠标左键时
            {
                TryPickUpItem();
            }
        });

        // 绑定丢弃功能
        Manager<InputManager>.Inst.InputButtonBinding(InputManager.DROP, x =>
            {
                if (x)  // 双击鼠标左键
                {
                    float timeSinceLastClick = Time.time - lastClickTime;

                    // 如果两次点击之间的时间小于双击阈值，则视为双击
                    if (timeSinceLastClick <= doubleClickThreshold)
                    {
                        DropItem();  // 执行丢弃物品操作
                        Debug.Log("Double click detected. Item dropped.");
                    }

                    // 更新上一次点击的时间
                    lastClickTime = Time.time;
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

    private void TryPickUpItem()
    {
        // 射线检测是否有物品可拾取
        if (Input.mousePosition.x > 960)
        {
            // 获取鼠标在屏幕上的位置并调整为右半屏坐标
            Vector3 localPos = Input.mousePosition - Vector3.right * 960f;

            // 使用右侧相机从点击位置发射射线
            Ray ray = _cam.ScreenPointToRay(localPos);

            // 射线检测与3D物品的交互
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("yes");
                if (hit.transform.tag == "Ground")
                {
                    Debug.Log("Move");
                    _movement.SetFaceTargetPos(hit.point);
                    _movement.SetFaceRotation(hit.point);
                    _movement.SetFaceMoveTargetSpeed(MoveSpeed);
                }
                // 可被拾取的物品
                if (hit.transform.tag == "PickupItem")
                {
                    GameObject item = hit.transform.gameObject;
                    if (bagManager.AddItem(item))
                    {
                        Debug.Log($"{item.name} picked up!");
                    }
                    else
                    {
                        Debug.Log("Backpack is full!");
                    }
                }
            }

        }
    }
    private void DropItem()
    {
        // 从背包中移除物品并丢弃到玩家前方
        GameObject item = bagManager.RemoveItem();
        if (item != null)
        {
            item.transform.position = transform.position + transform.forward; // 丢弃在玩家前方
            Debug.Log($"{item.name} dropped!");
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
