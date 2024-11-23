using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem.XR;
public class CursorWorldAct : WorldAct
{
    BPlayer PlayerB => Player as BPlayer;


    public override async Task OnLoad()
    {
        PlanetIndex = 2;
        await base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<BPlayer>(ResManager.CWPlayer, tran_entity);
        if(Physics.Raycast(Camera.transform.position,Camera.transform.forward,out var hit)) {

            Player.transform.position = hit.point;
        }
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
