using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableSense : MonoBehaviour
{
    public List<IPlayerInteractable> Interactables=new(capacity:2);

    public Action<IPlayerInteractable,bool> Listener;

    public bool TryGetInteractable(out IPlayerInteractable interactable)
    {
        Interactables.Sort(Order);
        if(Interactables.Count>0)
        {
            foreach (var item in Interactables)
            {
                if (item.CanInteract())
                {
                    interactable = item;
                    return true;
                }
            }
        }
        interactable = default;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IPlayerInteractable>(out var inter))
        {
            if(!Interactables.Contains(inter))
            {
                Interactables.Add(inter);
                Listener.Invoke(inter,true);
            }    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<IPlayerInteractable>(out var inter))
        {
            if(Interactables.Contains(inter))
            {
                Interactables.Remove(inter);
                Listener.Invoke(inter, false);
            }    
        }
    }


    public int Order(IPlayerInteractable a,IPlayerInteractable b)
    {
        return a.Priority()-b.Priority()<0?1:-1;
    }



}
