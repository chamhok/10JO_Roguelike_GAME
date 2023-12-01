using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Recovery : Item
{
    private void Awake()
    {
        Type = Define.EItemType.ElixirHerbs;
    }

    public override void Upgrade()
    {
        Player.hp = Player.maxHp;
    }
}
