using UnityEngine;
using System.Threading.Tasks;
public class BigWorldAct : ActBase
{
    public BigWorldPlayer Player;
    public BigWorldPlanet Planet;

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
        Planet=await Manager<ResManager>.Inst.LoadGo <BigWorldPlanet >($"{ResManager.BigPlanet}_{_statusData.CurSelectedLevel}");
        Player=await Manager<ResManager>.Inst.LoadGo < BigWorldPlayer>(ResManager.BigPlayer);;
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
