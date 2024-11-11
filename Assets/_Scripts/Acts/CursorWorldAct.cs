using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem.XR;
public class CursorWorldAct : WorldAct
{
    CWPlayer CWPlayer => Player as CWPlayer;
    // �����ҵ����
    public Camera rightCamera;


    public override async Task OnLoad()
    {
        PlanetIndex = 2;
        await base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<CWPlayer>(ResManager.CWPlayer, tran_entity);
        Debug.Log("???");
        Player.SetPlanetCenter(Planet.transform);
        // ��ȡ�����ҵ����
        rightCamera = Planet.GetComponentInChildren<Camera>();
    }

    

    public override async void OnLoaded()
    {
        base.OnLoaded();
    }

    public override void OnUnload()
    {
        base.OnUnload();
    }

    public override void OnUnloaded()
    {
        base.OnUnloaded();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > 960)
        {


            // ��ȡ�������Ļ�ϵ�λ�ò�����Ϊ�Ұ�������
            Vector3 localPos = Input.mousePosition - Vector3.right * 960f;

            // ʹ���Ҳ�����ӵ��λ�÷�������
            Ray ray = rightCamera.ScreenPointToRay(localPos);

            // ���߼����3D��Ʒ�Ľ���
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("yes");
            }

        }
    }
}
