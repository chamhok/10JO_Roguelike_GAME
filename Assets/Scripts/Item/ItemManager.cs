using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Dictionary<string, Item> _items = new Dictionary<string, Item>();

    private void Awake()
    {
        for(int i = 0; i < (int)Define.EItemType.Max; ++i)
        {
            AddItem((Define.EItemType)i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Test Code
        AddOrUpgradeItem(Define.EItemType.PineCone);
    }

    public void AddOrUpgradeItem(int type)
    {
        Define.EItemType itemType = (Define.EItemType)type;
        AddOrUpgradeItem(itemType);
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
        Item item = null;
        
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

            case Define.EItemType.Sun:
                item = go_Item.AddComponent<SunDropper>();
                break;

            case Define.EItemType.PineCone:
                item = go_Item.AddComponent<PineConeThrower>();
                break;

            case Define.EItemType.Water:
                item = go_Item.AddComponent<WaterRayzer>();
                break;
        }

        _items.Add(itemType.ToString(), item);
    }

    private void UpgradeItem(Define.EItemType itemType)
    {
        _items[itemType.ToString()].Upgrade();
    }
}
