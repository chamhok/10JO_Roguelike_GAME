using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DeerItem : Item
{
    Spreader[] _throwers = new Spreader[3];

    [SerializeField] string _throwingPrefabName = "Horn";
    [SerializeField] float _armLength = 1.0f;
    [SerializeField] float _throwingSpeed = 2.5f;
    [SerializeField] float _power = 0.8f;

    Vector3[] _offsets = new Vector3[3];

    private void Awake()
    {
        Type = Define.EItemType.Deer;

        _offsets[0] = new Vector3(1, 1, 0);
        _offsets[0].Normalize();
        _offsets[1] = Vector3.right;
        _offsets[2] = new Vector3(1, -1, 0);
        _offsets[2].Normalize();

        for (int i = 0; i < _throwers.Length; ++i)
        {
            _throwers[i] = gameObject.AddComponent<Spreader>();
            _throwers[i].ProjectilePrefabName = _throwingPrefabName;
            _throwers[i].ArmLength = _armLength;
            _throwers[i].FireRate = _throwingSpeed;
            _throwers[i].Offset = _offsets[i];
        }
    }

    public override void Upgrade()
    {
        if (IsMaxLevel()) return;

        ++Lv;
        switch(Lv)
        {
            case 1:
                UpgradeDamage();
                foreach(var thrower in _throwers)
                {
                    thrower.ThrowObject();
                }
                break;

            case 2:
                _power += 0.2f;
                UpgradeDamage();
                break;

            case 3:
                UpgradeThrowingSpeed();
                break;

            case 4:
                _power += 0.3f;
                UpgradeDamage();
                break;

            case 5:                
                UpgradeThrowingSpeed();
                break;
        }
    }

    private void UpgradeDamage()
    {
        float damage = Player.atk * _power;
        foreach (var thrower in _throwers)
        {
            thrower.Power = damage;
        }
    }

    private void UpgradeThrowingSpeed()
    {
        _throwingSpeed += 0.25f;
        foreach (var thrower in _throwers)
        {
            thrower.FireRate = _throwingSpeed;
        }
    }
}
