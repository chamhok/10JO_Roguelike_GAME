using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PineConeThrower : Item
{
    GameObject _pineConePrefab;
    GameObject _stoneSpawnPosition;
    float _armLength = 1.0f;
    Vector3 _shootingDirection;
    float _projectileSpeed = 1.0f;
    bool _bFire = false;
    float _fireDelay = 1.0f;

    private void Awake()
    {
        Type = Define.EItemType.PineCone;

        _pineConePrefab = Resources.Load<GameObject>("Item/PineCone");
        _stoneSpawnPosition = new GameObject("StoneSpawnPosition");
        _stoneSpawnPosition.transform.parent = this.transform;
        _stoneSpawnPosition.transform.localPosition = Vector2.right * _armLength;
    }

    private void Start()
    {
        //Upgrade();
    }

    private void Update()
    {
        // PlayerInput ¾µ·Á³ª.

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _shootingDirection = mousePosition - transform.position;
        _shootingDirection.z = 0;
        _shootingDirection.Normalize();
        transform.rotation = Quaternion.Euler(_shootingDirection);

        float rotZ = Mathf.Atan2(_shootingDirection.y, _shootingDirection.x) * Mathf.Rad2Deg;

        //armRenderer.flipY = Mathf.Abs(rotZ) > 90f;        
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }


    public override void Upgrade()
    {
        ++Lv;
        if(1 == Lv)
        {
            _bFire = true;
            StartCoroutine(IFire());
        }
    }

    IEnumerator IFire()
    {
        while (_bFire)
        {
            yield return new WaitForSeconds(_fireDelay);
            SpawnPineCone();
        }
    }

    void SpawnPineCone()
    {
        var go_PineCone = Instantiate(_pineConePrefab, _stoneSpawnPosition.transform.position, transform.rotation);
        go_PineCone.GetComponent<Projectile>().SetForward(_shootingDirection * _projectileSpeed);
    }
}
