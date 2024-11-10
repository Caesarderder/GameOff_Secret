using UnityEngine;
using System.Threading.Tasks;
public class WorldAct : ActBase
{
    public KWPlayer Player;
    public BigWorldPlanet Planet;
    public int PlanetIndex;

    GameStatusDataModule _statusData;

    [SerializeField]
    protected Vector3 _initPos;    

    [SerializeField]
    protected Transform 
        tran_static, 
        tran_entity
        ;

    public override async Task OnLoad()
    {
        base.OnLoad();

        _statusData=DataModule.Resolve<GameStatusDataModule>();
        MoveTo(_initPos);
        Debug.Log($"{ResManager.BigPlanet}_{_statusData.CurSelectedLevel}");
        Planet=await Manager<ResManager>.Inst.LoadGo <BigWorldPlanet >($"{ResManager.BigPlanet}_{PlanetIndex}{_statusData.CurSelectedLevel}",tran_entity);

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
