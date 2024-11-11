using UnityEngine;
using System.Threading.Tasks;
public class KeyboardWorldAct : WorldAct
{
    KWPlayer KWPlayer=>Player as KWPlayer;
    public override async Task OnLoad()
    {
        PlanetIndex = 1;
        await base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<KWPlayer>(ResManager.KWPlayer, tran_entity);
        Player.SetPlanetCenter(Planet.transform);
        KWPlayer.Init(Planet.Camera);
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
