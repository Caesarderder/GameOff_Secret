using UnityEngine;
using System.Threading.Tasks;
public class CursorWorldAct : WorldAct
{
    public override async Task OnLoad()
    {
        PlanetIndex = 2;
        base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<KWPlayer>(ResManager.CWPlayer, tran_entity);
        Player.SetPlanetCenter(Planet.transform);
        //��ȡPlanet��transform�е����
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
