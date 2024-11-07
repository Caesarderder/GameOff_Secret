using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;
using System;

public class InputManager 
{
    public static string MOVE="Move";
    public static string INTERACT="Interact";

    public void InputBinding<T>(string name,Action<bool,T> listener) where T : struct
    {
        var action=InputSystem.actions.FindAction(name);
        if ( action != null )
        {
            action.performed += (x)=> {
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

    public InputManager()
    {
    }
}
