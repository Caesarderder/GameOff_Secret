using UnityEngine;
using UnityEngine.UI;
public class GameRuneWidget :MonoBehaviour 
{
    EWorldType _worldType;
    [SerializeField]
    Button btn_close;
    public void Init(EWorldType type)
    {
        _worldType = type;
        btn_close.onClick.AddListener(Close);

        Close();
    }
    public void Open()
    {
        gameObject.SetActive(true);

        EventAggregator.Publish(new ChangeGameStateEvent() { 
            WorldType = _worldType,
            GameMode=EGameMode.RunePanel
        });

        Manager<InputManager>.Inst.InputButtonBinding(InputManager.ESC,
            x => {
                if ( x )
                    Close();
            });

    }

    public void Close()
    {
        gameObject.SetActive(false);

        EventAggregator.Publish(new ChangeGameStateEvent() { 
            WorldType = _worldType,
            GameMode=EGameMode.Normal
        });
    }
}
