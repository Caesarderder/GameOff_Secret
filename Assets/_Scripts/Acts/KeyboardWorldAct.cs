using UnityEngine;
using System.Threading.Tasks;
public class KeyboardWorldAct : WorldAct
{
    public override async Task OnLoad()
    {
        PlanetIndex = 1;
        base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<KWPlayer>(ResManager.KWPlayer, tran_entity);
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
