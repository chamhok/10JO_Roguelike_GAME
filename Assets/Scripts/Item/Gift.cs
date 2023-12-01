using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Gift : Item
{
    ItemManager _itemManager;

    private void Awake()
    {
        Type = Define.EItemType.Mountine;

        _itemManager = GetComponentInParent<ItemManager>();
    }

    public override void Upgrade()
    {
        _itemManager.AddOrUpgradeItem(Define.EItemType.ElixirHerbs);
        _itemManager.AddOrUpgradeItem(Define.EItemType.Stone);
        _itemManager.AddOrUpgradeItem(Define.EItemType.PineCone);
    }
}
