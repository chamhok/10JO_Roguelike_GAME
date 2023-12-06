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

        _comments[0] = "돌 강화\n이동 속도 증가\n체력 회복";
        _comments[1] = "돌 강화\n이동 속도 증가\n체력 회복";
        _comments[2] = "돌 강화\n이동 속도 증가\n체력 회복";
        _comments[3] = "돌 강화\n이동 속도 증가\n체력 회복";
        _comments[4] = "돌 강화\n이동 속도 증가\n체력 회복";

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
