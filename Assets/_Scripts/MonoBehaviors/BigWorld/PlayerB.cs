using Unity.VisualScripting;
using UnityEngine;

public class PlayerB : Player
{
    private BagManager bagManager;  // ����һ��˽���ֶ� bagManager

    private float lastClickTime = 0f;  // ��һ�ε����ʱ��
    private float doubleClickThreshold = 0.3f;  // ˫����������ʱ�䣨��λ���룩

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

        // ��ʼ����ұ���
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
        // ������������¼�������ʰȡ��Ʒ
        Manager<InputManager>.Inst.InputButtonBinding("MouseLeft", x =>
        {
            if (x)  // ����������ʱ
            {
                TryPickUpItem();
            }
        });

        // �󶨶�������
        Manager<InputManager>.Inst.InputButtonBinding(InputManager.DROP, x =>
            {
                if (x)  // ˫��������
                {
                    float timeSinceLastClick = Time.time - lastClickTime;

                    // ������ε��֮���ʱ��С��˫����ֵ������Ϊ˫��
                    if (timeSinceLastClick <= doubleClickThreshold)
                    {
                        DropItem();  // ִ�ж�����Ʒ����
                        Debug.Log("Double click detected. Item dropped.");
                    }

                    // ������һ�ε����ʱ��
                    lastClickTime = Time.time;
                }
            });
    }

    void OnMoveCLick()
    {
        var screenX= Screen.width;
        var screenY= Screen.height;
        Debug.Log(screenX);
        if ( Input.mousePosition.x > screenX/2)
        {
            // ��ȡ�������Ļ�ϵ�λ�ò�����Ϊ�Ұ�������
            Vector3 localPos = Input.mousePosition - Vector3.right * screenX/2;

            // ʹ���Ҳ�����ӵ��λ�÷�������
            Ray ray = _cam.ScreenPointToRay(localPos);

            // ���߼����3D��Ʒ�Ľ���
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if ( hit.transform.tag == "Ground" )
                {
                    _movement.SetFaceTargetPos(hit.point); 
                    _movement.SetFaceRotation(hit.point); 
                    _movement.SetFaceMoveTargetSpeed(MoveSpeed); 
                }
            }
        }

    }

    private void TryPickUpItem()
    {
        // ���߼���Ƿ�����Ʒ��ʰȡ
        if (Input.mousePosition.x > 960)
        {
            // ��ȡ�������Ļ�ϵ�λ�ò�����Ϊ�Ұ�������
            Vector3 localPos = Input.mousePosition - Vector3.right * 960f;

            // ʹ���Ҳ�����ӵ��λ�÷�������
            Ray ray = _cam.ScreenPointToRay(localPos);

            // ���߼����3D��Ʒ�Ľ���
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
                // �ɱ�ʰȡ����Ʒ
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
        // �ӱ������Ƴ���Ʒ�����������ǰ��
        GameObject item = bagManager.RemoveItem();
        if (item != null)
        {
            item.transform.position = transform.position + transform.forward; // ���������ǰ��
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
