using UnityEditor.Rendering;
using UnityEngine;

public class BagManager
{
    private readonly Intent intent;

    public BagManager(Intent intent)
    {
        this.intent = intent;
        intent.AddInt("bagCapacity", 1);   // 默认容量为 1
        intent.AddObject("bagItem", null); // 默认没有物品
    }

    // 添加物品到背包
    public bool AddItem(GameObject item)
    {
        if (intent.GetObject("bagItem") == null)
        {
            intent.AddObject("bagItem", item);
            item.SetActive(false); // 拾取之后，隐藏物品
            Debug.Log($"Added {item.name} to bag.");
            return true;
        }
        else
        {
            RemoveItem();   //背包已满，自动丢弃
            AddItem(item);
            Debug.Log("bag is full!drop!");
            return false;
        }
    }

    // 从背包移除物品
    public GameObject RemoveItem()
    {
        GameObject item = intent.GetObject("bagItem") as GameObject;
        if (item != null)
        {
            intent.AddObject("bagItem", null);
            item.SetActive(true); // 显示物品
            //// 在玩家附近丢弃物品
            //Vector3 dropPosition = player.transform.position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f));
            //item.transform.position = dropPosition;

            Debug.Log($"Removed {item.name} from bag.");
            return item;
        }
        return null;
    }

    // 检查背包是否有物品
    public bool HasItem()
    {
        return intent.GetObject("bagItem") != null;
    }
}
