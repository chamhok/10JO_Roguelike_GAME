using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

class StoneItem : Item
{
    Thrower _thrower;
    string[] _prefabNames = { "Stone", "DoubleStone", "TripleStone" };
    int _nameIndex = 0;
    [SerializeField] float _armLength = 1.0f;
    [SerializeField] float _throwingSpeed = 1.5f;
    [SerializeField] float _power = 2.0f;

    private void Awake()
    {
        Type = Define.EItemType.Stone;

        _comments[0] = "ȹ��";
        _comments[1] = "���� ��ȭ";
        _comments[2] = "���� ��ȭ";
        _comments[3] = "���� ��ȭ";
        _comments[4] = "���� ��ȭ";

        _thrower = gameObject.AddComponent<Thrower>();
        _thrower.ProjectilePrefabName = _prefabNames[_nameIndex++];
        _thrower.ArmLength = _armLength;
        _thrower.FireRate = _throwingSpeed;
    }

    private void Start()
    {
        Upgrade();
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
        // ����ӵ�, ������, ����
        int random = Random.Range(0, 3);
        RandomUpgrade(random);
    }

    private void RandomUpgrade(int type)
    {
        Debug.Log("Stone Upgrade : " + type);
        switch (type)
        {
            case 0: // �ӵ�
                _property += 100;
                PowerUp();
                break;

            case 1: // ������
                _property += 10;
                SpeedUp();
                break;

            case 2: // ����
                _property += 1;
                QuantityUp();
                break;
        }
    }

    private void PowerUp()
    {
        _power += 0.2f;
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

    public override void SetProperty(int val)
    {
        Debug.Log($"Stone Set {val}");
        ++Lv;
        int[] levels = new int[3];
        levels[0] = val / 100;
        val %= 100;
        levels[1] = val / 10;
        val %= 10;
        levels[2] = val / 1;

        for(int i = 0; i < 3; ++i)
        {
            for(int j = 0; j < levels[i]; ++j)
            {
                RandomUpgrade(i);
                ++Lv;
            }
        }
        Debug.Log($"Stone Level {Lv}");
        _thrower.ThrowObject();
    }
}
