using UnityEngine;

public class GamePlayDM : DataModule
{
    #region Fileds 亻尔 女子
    const string Bag = "Bag";

    //关卡状态
    private Intent _intent;


    #endregion


    #region Init
    public override void OnCreate()
    {
        base.OnCreate();
        _intent = Intent.Get();
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
            if (_intent != null)
            {
                _intent.Clear();
                _intent=null;
            }
            _intent = Intent.Get();
        }
    }

    public void OnExitGame(SActUnloadedEvent evt)
    {
        if ( evt.ActName == typeof(KeyboardWorldAct).Name )
        {
            _intent.Clear();
            _intent = null;
        }
    }

    #endregion

    #region Bag

    //Bag
    //0:没有 other:itemid
    public int GetCurBagItem(EWorldType worldType)
    {
        return _intent.GetInt(Bag + worldType.ToString());
    }

    public bool TryAddItem(BagItem item)
    {
        if ( GetCurBagItem(item.BelongWorld) != 0 )
        {
            return false;
        }
        else
        {
            _intent.AddInt(Bag + item.BelongWorld.ToString(), item.ItemId);
            _intent.AddObject(Bag + item.BelongWorld.ToString(), item);
            return true;
        }
    }

    public bool TryRemoveItem(EWorldType world)
    {
            if(_intent.TryGetValue(Bag + world.ToString(),out BagItem bagItem))
            {
                _intent.AddObject(Bag + world.ToString(),null);
                bagItem.DorpDown();
                _intent.AddInt(Bag + world.ToString(), 0);
                return true;
            }
        return false;
    }

    public void SetItemState(Item item,EItemState state)
    {
        var key = $"{item.ItemId}{item.BelongWorld}";
        if(_intent.GetInt(key)>=(int)state)
            return;
        _intent.AddInt(key,(int)state);
    }


    #endregion

    #region GameRunePanel
    public void SetGameRunePanel(GameRunePanel panel)
    {
        _intent.AddObject("GameRunePanel", panel);
    }
    public GameRunePanel GetGameRunePanel()
    {
        return _intent.GetObject("GameRunePanel") as GameRunePanel;
    }
    public void RemoveGameRunePanel()
    {
        _intent.RemoveObject("GameRunePanel");
    }

    #endregion
}
