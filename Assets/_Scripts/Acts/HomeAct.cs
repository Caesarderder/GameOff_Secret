using System.Threading.Tasks;
public class HomeAct : ActBase
{
    public override async Task OnLoad()
    {
        _=base.OnLoad();

        var uiManager = Manager<UIManager>.Inst;


        //º”‘ÿHomePanel
        var panel = await uiManager.ShowUI<HomePanel>();
    }

    public override async void OnLoaded()
    {
        base.OnLoaded();
    }

    public override void OnUnload()
    {
        base.OnUnload();
        Manager<UIManager>.Inst.DestroyUI<HomePanel>();
        //Manager<UIManager>.Inst.ShowUI<ProfilerPanel>();
    }

    public override void OnUnloaded()
    {
        base.OnUnloaded();
    }
}
