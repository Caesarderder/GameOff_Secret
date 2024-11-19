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
        PlayerA.Init(Planet.Camera);
        await Manager<UIManager>.Inst.ShowUI<GamePlayPanel>();  //动态加载GamePlayPanel
        Manager<UIManager>.Inst.ShowUI<GameRunePanel>();  //动态加载GamePlayPanel
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
