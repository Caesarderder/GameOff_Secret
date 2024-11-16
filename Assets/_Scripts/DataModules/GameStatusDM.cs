using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

public enum EWorldType
{
    None=-1,
    KW=0,
    CW=1,
}

public enum EGameState
{
    Home,
    GamePlay1,
}

public class GameStatusDM: DataModule
{
    #region Fileds 亻尔 女子
    public GameStatusSavedData Data;

    public int CurSelectedLevel;
    public EGameState CurGameState;


    #endregion

    #region Methods
    public override void OnCreate()
    {
        base.OnCreate();
        Data = DataFabUtil.LocalLoad<GameStatusSavedData>();
        CurSelectedLevel = 1;

        EventAggregator.Subscribe<SActLoadEvent>(OnEnterAct);
    }
    public override void OnDestory()
    {
        base.OnDestory();
        EventAggregator.Unsubscribe<SActLoadEvent>(OnEnterAct);
    }



    public void OnEnterAct(SActLoadEvent evt)
    {
        if ( evt.ActName == typeof(KeyboardWorldAct).Name )
        {
            CurGameState=EGameState.GamePlay1;
            Debug.Log("change Game state" +EGameState.GamePlay1);
        }
        else if( evt.ActName == typeof(HomeAct).Name )
        {
            CurGameState=EGameState.Home;
            Debug.Log("change Game state" +EGameState.Home);
        }
    }


    #endregion
}