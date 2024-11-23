using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSense : MonoBehaviour
{
    public List<IPlayerInteractable> Interactables=new(capacity:2);

    public Action<IPlayerInteractable,bool> Listener;
    private EWorldType WorldType;
    private void Start()
    {
        WorldType = GetComponentInParent < WorldAct>().WorldType;
        EventAggregator.Subscribe<CollisionRemoveEvent>(OnCollsionRemove);
    }
    private void OnDestroy()
    {
        EventAggregator.Unsubscribe<CollisionRemoveEvent>(OnCollsionRemove);
    }

    void OnCollsionRemove(CollisionRemoveEvent evt)
    {
        if(WorldType==evt.WorldType)
        {
            if(Interactables.Contains(evt.interactable))
                Interactables.Remove(evt.interactable);
        }
    }

    public bool TryGetInteractable(out IPlayerInteractable interactable)
    {
        Sort();
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
        if ( other.TryGetComponent<IPlayerInteractable>(out var inter) )
        {
            if ( !Interactables.Contains(inter) )
            {
                Interactables.Add(inter);
                Listener.Invoke(inter, true);
            }
        }
        else
        {
            var inter2 = other.GetComponentInParent<IPlayerInteractable>();

            if ( inter2!=null && !Interactables.Contains(inter2) )
            {
                Interactables.Add(inter2);
                Listener.Invoke(inter2, true);
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
        else
        {
            var inter2 = other.GetComponentInParent<IPlayerInteractable>();

            if ( inter2!=null && Interactables.Contains(inter2) )
            {
                Interactables.Remove(inter2);
                Listener.Invoke(inter2, false);
            }
        }
    }


    public int Order(IPlayerInteractable a,IPlayerInteractable b)
    {
        return a?.Priority()-b?.Priority()<0?1:-1;
    }


    public void Sort()
    {
        for (int i = Interactables.Count-1; i >=0 ; i--)
        {
            if ( Interactables[i] is null )
            {
                Interactables.RemoveAt(i);
            }
        }
        Interactables.Sort(Order);
    }

}
