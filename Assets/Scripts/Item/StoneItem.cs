using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

class StoneItem : Item
{
    Thrower _thrower;
    string[] _prefabNames = { "Stone", "DoubleStone", "TripleStone" };
    int _nameIndex = 0;
    [SerializeField] string _throwingPrefabName = "Stone";
    [SerializeField] float _armLength = 1.0f;
    [SerializeField] float _throwingSpeed = 1.0f;
    [SerializeField] float _power = 1.0f;

    private void Awake()
    {
        Type = Define.EItemType.Stone;

        _thrower = gameObject.AddComponent<Thrower>();
        _thrower.ProjectilePrefabName = _prefabNames[_nameIndex++];
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
            RandomUpgrade();
        }
    }

    private void RandomUpgrade()
    {
        // 연사속도, 데미지, 수량
        int random = Random.Range(0, 3);
        Debug.Log("Stone Upgrade : " + random);
        switch(random)
        {
            case 0: // 속도
                PowerUp();
                break;

            case 1: // 데미지
                SpeedUp();
                break;

            case 2: // 수량
                QuantityUp();
                break;
        }
    }

    private void PowerUp()
    {
        _power += 0.1f;
        float damage = Player.atk * _power;
        _thrower.Power = damage;
    }

    private void SpeedUp()
    {
        _throwingSpeed *= 1.5f;
        _thrower.FireRate = _throwingSpeed;
    }

    private void QuantityUp()
    {
        if(_nameIndex == 3)
        {
            RandomUpgrade();
            return;
        }
        _thrower.ProjectilePrefabName = _prefabNames[_nameIndex++];
        _thrower.Init();
    }
}
