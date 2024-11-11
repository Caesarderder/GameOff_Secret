using UnityEngine;
using System.Threading.Tasks;
public class CursorWorldAct : WorldAct
{
    CWPlayer CWPlayer => Player as CWPlayer;
    public override async Task OnLoad()
    {
        PlanetIndex = 2;
        await base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<CWPlayer>(ResManager.CWPlayer, tran_entity);
        Debug.Log("???");
        Player.SetPlanetCenter(Planet.transform);
        //获取Planet的transform中的相机
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
