using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class Haste : Item
{
    int _defaultPlayerSpeed;
    //float _speedWeight;

    private void Awake()
    {
        Type = Define.EItemType.Crane;

        if (Player)
        {
            _defaultPlayerSpeed = Player.speed;
        }
    }

    public override void Upgrade()
    {
        ++Lv;
        //Player.speed = _defaultPlayerSpeed * _speedWeight;
        Player.speed += 1;
    }
}
