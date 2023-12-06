using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class WaterRayzer : Item
{
    GameObject _waterPrefab;
    GameObject _spawnWaterPosition;
    [SerializeField] float _armLength = 1.0f;
    Vector3 _shootingDirection;

    float _spriteWidth;
    // 지속시간
    float _coolTime = 10.0f;
    float _activateTime = 1.0f;

    // 딜레이
    bool _bFire;

    // 데미지 배율
    float[] _weight = { 0.0f, 0.8f, 1.0f, 1.2f, 1.3f, 1.5f };

    private void Awake()
    {
        Type = Define.EItemType.Water;

        _comments[0] = "획득";
        _comments[1] = "데미지 증가";
        _comments[2] = "데미지 증가";
        _comments[3] = "데미지 증가";
        _comments[4] = "데미지 증가";

        _spawnWaterPosition = new GameObject("Spawn Water Position");
        _spawnWaterPosition.transform.parent = transform;
        _spawnWaterPosition.transform.localPosition = Vector2.right * _armLength;

        _waterPrefab = Instantiate(Resources.Load<GameObject>("Item/Water"), _spawnWaterPosition.transform);
        _waterPrefab.SetActive(false);

        // Test // Can get with of sprite
        _spriteWidth = (float)_waterPrefab.GetComponent<SpriteRenderer>().sprite.texture.width / 100;
        Debug.Log("Water Width : " + _waterPrefab.GetComponent<SpriteRenderer>().sprite.texture.width);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Upgrade();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _shootingDirection = mousePosition - transform.position;
        _shootingDirection.z = 0;
        _shootingDirection.Normalize();
        transform.rotation = Quaternion.Euler(_shootingDirection);

        float rotZ = Mathf.Atan2(_shootingDirection.y, _shootingDirection.x) * Mathf.Rad2Deg;

        //armRenderer.flipY = Mathf.Abs(rotZ) > 90f;        
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        float dist = Vector2.Distance(mousePosition, _spawnWaterPosition.transform.position);
        //Debug.Log("Dist : " + dist);

        _waterPrefab.transform.localScale = new Vector3((float)dist / _spriteWidth, 1, 1);
    }

    public override void Upgrade()
    {
        if (IsMaxLevel()) return;
        ++Lv;

        if(1 == Lv)
        {
            _bFire = true;
            StartCoroutine(IFire());
        }
        _waterPrefab.GetComponent<Weapon>().Damage = Player.atk * _weight[Lv];
    }

    IEnumerator IFire()
    {
        while (_bFire)
        {
            _waterPrefab.SetActive(true);
            yield return new WaitForSeconds(_activateTime);            
            _waterPrefab.SetActive(false);
            yield return new WaitForSeconds(_coolTime);
        }
    }
}
