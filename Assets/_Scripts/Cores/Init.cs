using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Init : MonoBehaviour
{

    [SerializeField]
    List<GameObject> _initGos;

    #region Methods
    public async void Awake()
    {
        await ContainerInit();
        LoadMainAct();
        _initGos.Add(gameObject);
        foreach ( GameObject go in _initGos ) {
            Destroy(go);
        }
    }

    private async Task ContainerInit()
    {
        #region Register      
        var tableManager = new TableManager();
        await tableManager.Init();
        Debug.Log("Table Init time:"+Time.time);

        var uiManager=new UIManager();
        await uiManager.Init();


        GContext.RegisterSingleton<TableManager>(tableManager);
        GContext.RegisterSingleton<UIManager>(uiManager);
        GContext.RegisterSingleton<ActManager>();
        GContext.RegisterSingleton<ResManager>();
        GContext.RegisterSingleton<InputManager>();

        GContext.RegisterMoudle<InputDataModule>();

        Debug.Log("GContext register time:"+Time.time);

        #endregion
    }

    private void LoadMainAct()
    {
        _=Manager<ActManager>.Inst.LoadAct(EAct.BigWorldAct);
    }

    #endregion
}
