using System.Threading.Tasks;
public class HomeAct : ActBase
{
    TestDataModule _testDataModule;
    public override async Task OnLoad()
    {
        _=base.OnLoad();
        //获取引用
        _testDataModule = DataModule.Resolve<TestDataModule>();

        var uiManager = Manager<UIManager>.Inst;

        //获取数据
        var num = _testDataModule.GetTestSavedData();

        //加载HomePanel
        var panel = await uiManager.ShowUI<HomePanel>();
        //传数据
        panel.Init(num);
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
