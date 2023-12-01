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

    // 지속시간

    // 딜레이
    bool _bFire;

    private void Awake()
    {
        _spawnWaterPosition = new GameObject("Spawn Water Position");
        _spawnWaterPosition.transform.parent = transform;
        _spawnWaterPosition.transform.localPosition = Vector2.right * _armLength;

        _waterPrefab = Instantiate(Resources.Load<GameObject>("Item/Water"), _spawnWaterPosition.transform);
        _waterPrefab.SetActive(false);

        // Test // Can get with of sprite
        Debug.Log("Water Width : " + _waterPrefab.GetComponent<SpriteRenderer>().sprite.texture.width);
    }

    // Start is called before the first frame update
    void Start()
    {
        Upgrade();
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
            _waterPrefab.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            _waterPrefab.SetActive(false);
        }
    }
}
