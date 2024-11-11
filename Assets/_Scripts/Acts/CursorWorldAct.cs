using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem.XR;
public class CursorWorldAct : WorldAct
{
    CWPlayer CWPlayer => Player as CWPlayer;
    // 鼠标玩家的相机
    public Camera rightCamera;


    public override async Task OnLoad()
    {
        PlanetIndex = 2;
        await base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<CWPlayer>(ResManager.CWPlayer, tran_entity);
        Debug.Log("???");
        Player.SetPlanetCenter(Planet.transform);
        // 获取鼠标玩家的相机
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


            // 获取鼠标在屏幕上的位置并调整为右半屏坐标
            Vector3 localPos = Input.mousePosition - Vector3.right * 960f;

            // 使用右侧相机从点击位置发射射线
            Ray ray = rightCamera.ScreenPointToRay(localPos);

            // 射线检测与3D物品的交互
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("yes");
            }

        }
    }
}
