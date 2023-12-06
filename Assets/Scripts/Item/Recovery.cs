using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Recovery : Item
{
    private void Awake()
    {
        Type = Define.EItemType.ElixirHerbs;

        _comments[0] = "체력 100% 회복";
        _comments[1] = "체력 100% 회복";
        _comments[2] = "체력 100% 회복";
        _comments[3] = "체력 100% 회복";
        _comments[4] = "체력 100% 회복";
    }

    public override void Upgrade()
    {
        Player.hp = Player.maxHp;
    }
}
