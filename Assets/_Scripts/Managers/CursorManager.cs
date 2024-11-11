using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CursorManager
{
    #region Fields

    private Camera mainCamera;
    private Transform cursorRoot;
    

    #endregion

    #region Constructor & Initialization

    public void Init(Camera camera)
    {
        mainCamera = camera;
        if (cursorRoot != null)
            return;

        // Set up a root object for cursor management in the scene
        cursorRoot = new GameObject("CursorRoot").transform;
        GameObject.DontDestroyOnLoad(cursorRoot);

    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 使用鼠标控制与3D对象交互
    /// </summary>
    public void InteractWithObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //var interactable = hit.transform.GetComponent<IPlayerInteractable>();
                //interactable?.OnInteract();
            }
        }
    }


    #endregion
}

