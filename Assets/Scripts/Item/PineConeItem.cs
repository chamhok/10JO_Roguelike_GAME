using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

class PineConeItem : Item
{
    Thrower _thrower;
    [SerializeField] string _throwingPrefabName = "PineCone";
    [SerializeField] float _armLength = 1.0f;
    [SerializeField] float _throwingSpeed = 0.5f;
    [SerializeField] float _power = 1.0f;

    private void Awake()
    {
        Type = Define.EItemType.PineCone;

        _comments[0] = "획득";
        _comments[1] = "데미지 증가\n연사 속도 증가";
        _comments[2] = "데미지 증가\n연사 속도 증가";
        _comments[3] = "데미지 증가\n연사 속도 증가";
        _comments[4] = "데미지 증가\n연사 속도 증가";

        _thrower = gameObject.AddComponent<Thrower>();
        _thrower.ProjectilePrefabName = _throwingPrefabName;
        _thrower.ArmLength = _armLength;
        _thrower.FireRate = _throwingSpeed;
    }

    public override void Upgrade()
    {
        if (IsMaxLevel()) return;

        ++Lv;
        if (1 == Lv)
        {
            float damage = Player.atk * _power;
            _thrower.Power = damage;
            _thrower.ThrowObject();
        }
        else
        {
            _power += 0.25f;
            float damage = Player.atk * _power;

            _throwingSpeed += 0.25f;
            _thrower.FireRate = _throwingSpeed;
        }        
    }
}
