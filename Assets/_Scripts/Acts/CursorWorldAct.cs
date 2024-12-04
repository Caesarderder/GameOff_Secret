using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem.XR;
using System.Diagnostics.Tracing;
public class CursorWorldAct : WorldAct
{
    BPlayer PlayerB => Player as BPlayer;
    AudioShot _audioShot;


    public override async Task OnLoad()
    {
        PlanetIndex = 2;
        await base.OnLoad();
        Player = await Manager<ResManager>.Inst.LoadGo<BPlayer>(ResManager.CWPlayer, tran_entity);
        if(Physics.Raycast(Camera.transform.position,Camera.transform.forward,out var hit)) {

            Player.transform.position = hit.point;
        }
    }
    

    public override async void OnLoaded()
    {
        base.OnLoaded();
        _audioShot=GetComponent<AudioShot>();
        EventAggregator.Subscribe<SItemInteractEvent>(OnChangeWindMusic);
    }

    public override void OnUnload()
    {
        base.OnUnload();
        EventAggregator.Unsubscribe<SItemInteractEvent>(OnChangeWindMusic);
    }
    void OnChangeWindMusic(SItemInteractEvent evt)
    {
        if(evt.CurId==1003)
        {
            _audioShot.ChangeAudioClipIndex(3);
            _audioShot.Play();
            Debug.Log("ÇÐ»»·çÉù");
        }
    }

    public override void OnUnloaded()
    {
        base.OnUnloaded();
    }

}
