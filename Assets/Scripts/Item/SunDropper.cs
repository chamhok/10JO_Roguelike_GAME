using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SunDropper : Item
{
    float _dropDelay = 1.0f;
    GameObject _sunPrefab;

    private void Awake()
    {
        Type = Define.EItemType.Sun;

        _sunPrefab = Resources.Load<GameObject>("Item/Sun");
    }

    public override void Upgrade()
    {
        Lv++;
        if (1 == Lv)
        {            
            StartCoroutine(IFire());            
        }
    }

    IEnumerator IFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(_dropDelay);
            Instantiate(_sunPrefab, transform.position, Quaternion.identity);
        }
    }
}
