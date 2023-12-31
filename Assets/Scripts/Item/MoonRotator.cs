using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoonRotator : Item
{
    Vector2[] _offsets = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
    float _armLength = 2.0f;
    GameObject[] _satelite = new GameObject[4];
    int _index = 0;
    public float speed = 350.0f;
    float[] _weight = { 1.0f, 1.5f, 2.0f };

    private void Awake()
    {
        Type = Define.EItemType.Moon;

        _comments[0] = "획득";
        _comments[1] = "달 1개 추가";
        _comments[2] = "데미지 증가";
        _comments[3] = "달 2개 추가";
        _comments[4] = "데미지 증가";

        // 자신의 상 하 좌 우 위치에 인스턴스 생성 및 보관
        for (int i = 0; i < 4; ++i)
        {
            GameObject go_Moon = Instantiate(Resources.Load<GameObject>("Item/Moon"));
            go_Moon.transform.parent = this.transform;
            go_Moon.transform.localPosition = (Vector3)_offsets[i] * _armLength;
            Debug.Log("Weapon Awake");
            _satelite[i] = go_Moon;
        }
                
        // 모두 비활성화
        foreach(var go in _satelite)
        {
            go.SetActive(false);
        }
    }

    private void Update()
    {
        // 회전 
        if (0 == _index) return;
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }

    private void UpdateWeaponDamage(int step)
    {
        foreach (var moon in _satelite)
        {
            moon.GetComponent<Weapon>().Damage = Player.atk * _weight[step];
        }
    }

    public override void Upgrade()
    {
        if (IsMaxLevel()) return;

        switch (++Lv)
        {
            case 1:
                _satelite[_index++].SetActive(true);
                UpdateWeaponDamage(0);
                break;

            case 2:
                _satelite[_index++].SetActive(true);
                break;

            case 3:
                UpdateWeaponDamage(1);
                break;

            case 4:
                _satelite[_index++].SetActive(true);
                _satelite[_index++].SetActive(true);
                break;

            case 5:
                UpdateWeaponDamage(2);
                break;
        }
    }
}
