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
    public int GetCurBagItemId(EWorldType worldType)
    {
        return _intent.GetInt(Bag + worldType.ToString());
    }
    public bool TryGetCurBagItem(EWorldType worldType,out BagItem item)
    {
        item=_intent.GetObject(Bag + worldType.ToString()) as BagItem;
        if(item)
            return true;
        return false;
        
    }

    public bool TryAddItem(BagItem item)
    {
        if ( GetCurBagItemId(item.BelongWorld) != 0 )
        {
            return false;
        }
        else
        {
            Debug.Log(item.WorldType+ "  " + item.ItemId+"add to bag");
            _intent.AddInt(Bag + item.BelongWorld.ToString(), item.ItemId);
            _intent.AddObject(Bag + item.BelongWorld.ToString(), item);
            return true;
        }
    }

    public bool TryRemoveItem(EWorldType world)
    {
        if ( _intent.TryGetValue(Bag + world.ToString(), out BagItem bagItem) )
        {
            _intent.AddObject(Bag + world.ToString(), null);
            bagItem.DropDown();
            _intent.AddInt(Bag + world.ToString(), 0);
            Debug.Log(bagItem.WorldType + "  " + bagItem.ItemId + "remove from bag");
            return true;
        }
        return false;
    }

    public async void GenerateItemInBag(EWorldType WorldType,int itemId,Vector3 pos,Transform interactor)
    {
        var planet=DataModule.Resolve<GamePlayDM>().GetPlanet(WorldType);
        var bagItem = await Manager<ResManager>.Inst.LoadGo<BagItem>($"item_{itemId}", planet.transform,pos);
        bagItem.Start();
        TryRemoveItem(WorldType);
        bagItem.EnterTrigger(interactor);
        bagItem.Interact();
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

    #region Planet
    public void SetPlanet(BigWorldPlanet planet)
    {
        _intent.AddObject("Planet"+planet.WorldType, planet);
    }
    public BigWorldPlanet GetPlanet(EWorldType type)
    {
        return _intent.GetObject("Planet"+type) as BigWorldPlanet;
    }

    public Transform GetPlanetCenter(EWorldType type)
    {
        var planet=GetPlanet(type);
        return planet.transform;
    }

    public void SetWorld(WorldAct planet)
    {
        _intent.AddObject("World"+(int)planet.WorldType, planet);
    }
    public WorldAct GetWorld(EWorldType type)
    {
        return _intent.GetObject("World"+(int)type) as WorldAct;
    }
    #endregion
}
