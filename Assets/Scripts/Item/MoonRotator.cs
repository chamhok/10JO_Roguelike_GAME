using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoonRotator : Item
{
    Vector2[] _offsets = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
    float _armLength = 1.4f;
    GameObject[] _satelite = new GameObject[4];
    int _index = 0;
    public float speed = 10.0f;

    private void Awake()
    {
        Type = Define.EItemType.Moon;

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

    private void Start()
    {
        Debug.Log("Weapon Start");
        UpdateWeaponDamage();
    }

    private void Update()
    {
        // 회전 
        if (0 == _index) return;
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }

    private void UpdateWeaponDamage()
    {
        foreach (var moon in _satelite)
        {
            moon.GetComponent<Weapon>().Damage = 10; // Get Player Attack Status
        }
    }

    public override void Upgrade()
    {
        _satelite[_index++].SetActive(true);
        ++Lv;
    }
}
