using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SunItem : Item
{
    Thrower _thrower;
    [SerializeField] string _throwingPrefabName = "Sun";
    [SerializeField] float _throwingSpeed = 0.1f;
    [SerializeField] float _power = 2.0f;

    private void Awake()
    {
        Type = Define.EItemType.Sun;

        _thrower = gameObject.AddComponent<Thrower>();
        _thrower.ProjectilePrefabName = _throwingPrefabName;
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
            _power += 0.2f;
            float damage = Player.atk * _power;
            _thrower.Power = damage;
        }        
    }
}
