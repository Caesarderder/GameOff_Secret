using UnityEngine;
using System.Threading.Tasks;
public class WorldAct : ActBase
{
    public KWPlayer Player;
    public BigWorldPlanet Planet;

    GameStatusDataModule _statusData;

    [SerializeField]
    Vector3 _initPos;    

    [SerializeField]
    Transform 
        tran_static, 
        tran_entity
        ;

    public override async Task OnLoad()
    {
        base.OnLoad();

        _statusData=DataModule.Resolve<GameStatusDataModule>();
        MoveTo(_initPos);
        Debug.Log($"{ResManager.BigPlanet}_{_statusData.CurSelectedLevel}");
        Planet=await Manager<ResManager>.Inst.LoadGo <BigWorldPlanet >($"{ResManager.BigPlanet}_{_statusData.CurSelectedLevel}",tran_entity);
        Player=await Manager<ResManager>.Inst.LoadGo < KWPlayer>(ResManager.BigPlayer,tran_entity);
        Player.SetPlanetCenter(Planet.transform);

    }

    public override async void OnLoaded()
    {
        base.OnLoaded();
    }

    public void MoveTo(Vector3 pos)
    {
        transform.position = pos;
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
