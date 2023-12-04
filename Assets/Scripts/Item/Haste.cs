using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class Haste : Item
{
    float _defaultPlayerSpeed;
    //float _speedWeight;

    private void Awake()
    {
        Type = Define.EItemType.Crane;
    }

    private void Start()
    {
        if (Player)
        {
            _defaultPlayerSpeed = Player.speed;
        }
    }

    public override void Upgrade()
    {
        if (IsMaxLevel()) return;
        ++Lv;

        Player.speed += 0.1f;
    }
}
