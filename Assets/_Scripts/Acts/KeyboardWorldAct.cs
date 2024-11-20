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
        Player.SetPlanetCenter(Planet.transform);
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
