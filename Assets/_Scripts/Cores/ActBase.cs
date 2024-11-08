using System;
using System.Threading.Tasks;
using UnityEngine;

public class ActBase:MonoBehaviour
{
    // 在 Awake 中自动赋值 Name
    public virtual async Task OnLoad() { EventAggregator.Publish(new SActLoadEvent { ActName=this.GetType().Name});}
    public virtual void OnLoaded() 
    { 
        EventAggregator.Publish(new SActLoadedEvent { ActName = this.GetType().Name  }); 
        Debug.Log($"<color=green>Act:   {GetType().Name}   Loaded</color>");
    }

    public virtual void OnUnload() { EventAggregator.Publish(new SActUnloadEvent { ActName = this.GetType().Name }); }
    public virtual void OnUnloaded() 
    { 
        EventAggregator.Publish(new SActUnloadedEvent { ActName = this.GetType().Name }); 
        Debug.Log($"<color=yellow>Act:   {GetType().Name}   UnLoaded</color>");
    }
}
