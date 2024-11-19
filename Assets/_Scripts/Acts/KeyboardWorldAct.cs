using UnityEngine;
using System.Threading.Tasks;
public class KeyboardWorldAct : WorldAct
{
    PlayerA PlayerA=>Player as PlayerA;
    public override async Task OnLoad()
    {
        PlanetIndex = 1;
        await base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<PlayerA>(ResManager.KWPlayer, tran_entity);
        Player.SetPlanetCenter(Planet.transform);
        PlayerA.Init(Planet.Camera);
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
