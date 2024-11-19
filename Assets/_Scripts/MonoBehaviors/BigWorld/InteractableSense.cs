using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableSense : MonoBehaviour
{
    public List<IPlayerInteractable> Iteractables=new(capacity:2);

    public Action<IPlayerInteractable,bool> Listener;

    public bool TryGetInteractable(out IPlayerInteractable interactable)
    {
        Iteractables.Sort(Order);
        if(Iteractables.Count>0)
        {
            interactable= Iteractables[0];
            return true;
        }
        interactable = default;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IPlayerInteractable>(out var inter))
        {
            if(!Iteractables.Contains(inter))
            {
                Iteractables.Add(inter);
                Listener.Invoke(inter,true);
            }    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<IPlayerInteractable>(out var inter))
        {
            if(Iteractables.Contains(inter))
            {
                Iteractables.Remove(inter);
                Listener.Invoke(inter, false);
            }    
        }
    }


    public int Order(IPlayerInteractable a,IPlayerInteractable b)
    {
        return a.Priority()-b.Priority()<0?-1:1;
    }



}
