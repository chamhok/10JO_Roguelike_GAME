using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Dictionary<string, Item> _items = new Dictionary<string, Item>();

    // Start is called before the first frame update
    void Start()
    {
        // Test Code
        AddOrUpgradeItem(Define.EItemType.Stone);
    }

    public void AddOrUpgradeItem(Define.EItemType itemType)
    {
        if(_items.ContainsKey(itemType.ToString()))
        {
            UpgradeItem(itemType);
        }
        else
        {
            AddItem(itemType);
        }
    }

    private void AddItem(Define.EItemType itemType)
    {
        GameObject go_Item = new GameObject(itemType.ToString());
        go_Item.transform.parent = transform;
        Item? item = null;
        
        switch(itemType)
        {
            case Define.EItemType.Moon:
                item = go_Item.AddComponent<MoonRotator>();
                Debug.Log("Add Component - Moon");
                break;

            case Define.EItemType.Turtle:
                item = go_Item.AddComponent<Turtle>();                
                break;

            case Define.EItemType.Stone:
                item = go_Item.AddComponent<StoneThrower>();
                break;
        }

        _items.Add(itemType.ToString(), item);
    }

    private void UpgradeItem(Define.EItemType itemType)
    {
        _items[itemType.ToString()].Upgrade();
    }
}
