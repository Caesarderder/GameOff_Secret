using UnityEngine;
using System.Threading.Tasks;
public class CursorWorldAct : WorldAct
{
    public override async Task OnLoad()
    {
        PlanetIndex = 2;
        base.OnLoad();
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
