using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;
using System;

public class InputManager 
{
    public static string MOVE="Move";
    public static string INTERACT="Interact";

    public void InputValueBinding<T>(string name,Action<bool,T> listener) where T : struct
    {
        var action=InputSystem.actions.FindAction(name);
        if ( action != null )
        {
            action.started+= (x)=> {
                var data = x.ReadValue<T>();
                listener.Invoke(true,data);
            };

            action.canceled += (x)=> {
                var data = x.ReadValue<T>();
                listener.Invoke(false, data);
            };
        }
        else
            Debug.Log("Input action can not Find Action:" + name);
    }
    public void InputButtonBinding(string name,Action<bool> listener) 
    {
        var action=InputSystem.actions.FindAction(name);
        if ( action != null )
        {
            action.started+= (x)=> {
                listener.Invoke(true);
            };

            action.canceled += (x)=> {
                listener.Invoke(false);
            };
        }
        else
            Debug.Log("Input action can not Find Action:" + name);
    }

    public InputManager()
    {
    }
}
