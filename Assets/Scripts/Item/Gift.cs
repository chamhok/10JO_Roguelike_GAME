using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Gift : Item
{
    ItemManager _itemManager;
    Define.EItemType[] _types = new Define.EItemType[] {
        Define.EItemType.Stone, 
        Define.EItemType.ElixirHerbs, 
        Define.EItemType.PineCone };

    private void Awake()
    {
        Type = Define.EItemType.Mountine;

        _itemManager = GetComponentInParent<ItemManager>();
    }

    public override void Upgrade()
    {
        foreach(var type in _types)
        {
            _itemManager.AddOrUpgradeItem(type);
        }
    }
}
