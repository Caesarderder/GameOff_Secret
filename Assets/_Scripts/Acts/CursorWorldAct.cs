using UnityEngine;
using System.Threading.Tasks;
public class CursorWorldAct : WorldAct
{
    public override async Task OnLoad()
    {
        PlanetIndex = 2;
        base.OnLoad();
        //获取Planet的transform中的相机
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
