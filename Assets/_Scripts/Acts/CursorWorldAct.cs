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
        Player.SetPlanetCenter(Planet.transform);
        // ��ȡ�����ҵ����
        rightCamera = Planet.GetComponentInChildren<Camera>();
        CWPlayer.Init(rightCamera);
          
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

}
