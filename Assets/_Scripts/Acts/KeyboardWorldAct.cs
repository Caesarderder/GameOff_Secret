using UnityEngine;
using System.Threading.Tasks;
public class KeyboardWorldAct : ActBase
{
    public KWPlayer Player;
    public KWPlanet Planet;

    GameStatusDataModule _statusData;

    public override async Task OnLoad()
    {
        base.OnLoad();
        _statusData=DataModule.Resolve<GameStatusDataModule>();
    }

    public override async void OnLoaded()
    {
        base.OnLoaded();
        Debug.Log($"{ResManager.BigPlanet}_{_statusData.CurSelectedLevel}");
        Planet=await Manager<ResManager>.Inst.LoadGo <KWPlanet >($"{ResManager.BigPlanet}_{_statusData.CurSelectedLevel}");
        Player=await Manager<ResManager>.Inst.LoadGo < KWPlayer>(ResManager.BigPlayer);;
        Player.SetPlanetCenter(Planet.transform);
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
