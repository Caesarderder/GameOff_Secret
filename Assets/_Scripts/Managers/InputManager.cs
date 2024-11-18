using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager
{
    public static string MOVE = "Move";
    public static string INTERACT = "Interact";
    public static string CURSORLEFT = "CursorLeft";
    public static string CURSORRIGHT = "CursorRight";
    public static string DROP = "Drop"; 

    // 用于绑定并监听值类型的输入（比如鼠标坐标或其他结构体）
    public void InputValueBinding<T>(string name, Action<bool, T> listener) where T : struct
    {
        var action = InputSystem.actions.FindAction(name);
        if (action != null)
        {
            action.started += (x) => {
                var data = x.ReadValue<T>();
                listener?.Invoke(true, data);
            };
            action.performed += (x) => {
                var data = x.ReadValue<T>();
                listener?.Invoke(true, data);
            };

            action.canceled += (x) => {
                var data = x.ReadValue<T>();
                listener?.Invoke(false, data);
            };
        }
        else
            Debug.Log("Input action cannot find Action: " + name);
    }

    // 用于绑定并监听按钮类型的输入（如点击）
    public void InputButtonBinding(string name, Action<bool> listener)
    {
        var action = InputSystem.actions.FindAction(name);
        if (action != null)
        {
            action.started += (x) => {
                listener?.Invoke(true);
            };

            action.performed += (x) => {
                listener?.Invoke(true);
            };

            action.canceled += (x) => {
                listener?.Invoke(false);
            };
        }
        else
            Debug.Log("Input action cannot find Action: " + name);
    }

    public InputManager() { }
}