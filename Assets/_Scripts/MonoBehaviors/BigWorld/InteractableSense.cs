using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableSense : MonoBehaviour
{
    List<IPlayerInteractable> _iteractables=new(capacity:2);

    public Action<IPlayerInteractable,bool> Listener;

    public bool TryGetInteractable(out IPlayerInteractable interactable)
    {
        _iteractables.Sort(Order);
        if(_iteractables.Count>0)
        {
            interactable= _iteractables[0];
            return true;
        }
        interactable = default;
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IPlayerInteractable>(out var inter))
        {
            if(!_iteractables.Contains(inter))
            {
                _iteractables.Add(inter);
                Listener.Invoke(inter,true);
            }    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<IPlayerInteractable>(out var inter))
        {
            if(_iteractables.Contains(inter))
            {
                _iteractables.Remove(inter);
                Listener.Invoke(inter, false);
            }    
        }
    }


    public int Order(IPlayerInteractable a,IPlayerInteractable b)
    {
        return a.Priority()-b.Priority()<0?-1:1;
    }



}
