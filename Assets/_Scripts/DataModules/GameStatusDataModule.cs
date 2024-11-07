using UnityEngine;

public class GameStatusDataModule: DataModule
{
    #region Fileds 亻尔 女子
    public GameStatusSavedData Data;

    public int CurSelectedLevel;


    #endregion

    #region Methods
    public override void OnCreate()
    {
        base.OnCreate();
        Data = DataFabUtil.LocalLoad<GameStatusSavedData>();
        CurSelectedLevel = 1;
    }

    #endregion
}
