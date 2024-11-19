using UnityEngine;

public class GameRunePanel : ViewBase
{
    GamePlayDM _gamePlayDM;
    GameStatusDM _gameStatusDM;
    int _curLevel;

    [SerializeField]
    GameRuneWidget
        widget_A,
        widget_B 
        ;

    #region Unity

    private void Update()
    {

    }
    public override void Load()
    {
        base.Load();
        _gamePlayDM = DataModule.Resolve<GamePlayDM>();
        _gamePlayDM.SetGameRunePanel(this);

        _gameStatusDM = DataModule.Resolve<GameStatusDM>();
        _curLevel=_gameStatusDM.CurSelectedLevel;

        widget_A.Init(EWorldType.A);
        widget_B.Init(EWorldType.B);
    }

    public override void Destroy()
    {
        base.Destroy();
        _gamePlayDM.RemoveGameRunePanel();
    }

    public void OpenHalf(EWorldType worldType)
    {
        switch ( worldType )
        {
            case EWorldType.A:
                widget_A.Open();
                break;
            case EWorldType.B:
                widget_B.Open();
                break;
            default:
                break;
        }
    }
    public void CloseHalf(EWorldType worldType)
    {
        switch ( worldType )
        {
            case EWorldType.A:
                widget_A.Close();
                break;
            case EWorldType.B:
                widget_B.Close();
                break;
            default:
                break;
        }
    }


    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }

    #endregion


}
