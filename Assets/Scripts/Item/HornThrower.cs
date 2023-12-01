using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

class HornThrower : Item
{
    GameObject _spawnPosition;
    bool _bFire;
    GameObject _hornPrefab;
    float _armLength = 1.0f;
    Vector3 _shootingDirection;
    float _projectileSpeed = 1.0f;

    private void Awake()
    {
        Type = Define.EItemType.Deer;
        _hornPrefab = Resources.Load<GameObject>("Item/Horn");

        _spawnPosition = new GameObject();
        _spawnPosition.transform.parent = transform;
        _spawnPosition.transform.localPosition = Vector3.right * _armLength;
    }

    private void Update()
    {
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
        while(_bFire)
        {
            yield return new WaitForSeconds(1.0f);
            var go_Horn = Instantiate(_hornPrefab, _spawnPosition.transform.position, _spawnPosition.transform.rotation);
            go_Horn.GetComponent<Projectile>().SetVelocity(_shootingDirection * _projectileSpeed);
        }
    }
}
