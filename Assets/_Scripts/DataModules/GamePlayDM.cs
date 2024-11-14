using Unity.VisualScripting;
using UnityEngine;

public class GamePlayDM : DataModule
{
    #region Fileds 亻尔 女子

    //关卡状态
    public Intent Intent;


    #endregion


    #region Init
    public override void OnCreate()
    {
        base.OnCreate();
        EventAggregator.Subscribe<SActLoadEvent>(OnEnterGame);
        EventAggregator.Subscribe<SActUnloadedEvent>(OnExitGame);
    }
    public override void OnDestory()
    {
        base.OnDestory();
        EventAggregator.Unsubscribe<SActLoadEvent>(OnEnterGame);
        EventAggregator.Unsubscribe<SActUnloadedEvent>(OnExitGame);
    }

    public void OnEnterGame(SActLoadEvent evt)
    {
        if ( evt.ActName == typeof(KeyboardWorldAct).Name )
        {
            Intent = Intent.Get();
        }
    }

    public void OnExitGame(SActUnloadedEvent evt)
    {
        if ( evt.ActName == typeof(KeyboardWorldAct).Name )
        {
            Intent.Clear();
            Intent = null;
        }
    }

    #endregion

    #region Method

    #endregion
}
