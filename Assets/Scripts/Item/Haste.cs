using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class Haste : Item
{
    float _defaultPlayerSpeed;

    private void Awake()
    {
        Type = Define.EItemType.Crane;

        _comments[0] = "이동 속도 증가";
        _comments[1] = "이동 속도 증가";
        _comments[2] = "이동 속도 증가";
        _comments[3] = "이동 속도 증가";
        _comments[4] = "이동 속도 증가";

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
