using UnityEngine;
using System.Threading.Tasks;
public class WorldAct : ActBase
{
    public Player Player;
    public BigWorldPlanet Planet;
    public int PlanetIndex;
    public EWorldType WorldType=>(EWorldType)PlanetIndex;

    GameStatusDM _statusData;

    [SerializeField]
    protected Vector3 _initPos;    

    [SerializeField]
    protected Transform 
        tran_static, 
        tran_entity
        ;
    [SerializeField]
    public Camera Camera;

    public override async Task OnLoad()
    {
        base.OnLoad();
        Debug.Log("setworld!");
        DataModule.Resolve<GamePlayDM>().SetWorld(this);
        _statusData=DataModule.Resolve<GameStatusDM>();
        MoveTo(_initPos);
        Debug.Log($"{ResManager.BigPlanet}_{_statusData.CurSelectedLevel}");
        Planet=await Manager<ResManager>.Inst.LoadGo <BigWorldPlanet >($"{ResManager.BigPlanet}_{PlanetIndex}{_statusData.CurSelectedLevel}",tran_entity);
        Planet.WorldType=(EWorldType)PlanetIndex;

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
