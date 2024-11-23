using UnityEngine;
using System.Threading.Tasks;
public class KeyboardWorldAct : WorldAct
{
    APlayer PlayerA=>Player as APlayer;
    public override async Task OnLoad()
    {
        PlanetIndex = 1;
        await base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<APlayer>(ResManager.KWPlayer, tran_entity);
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
