using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Laser : Weapon
{
    float _blinkTerm = 0.1f;
    bool _bActivate = false;

    private void Awake()
    {
        _collider2D = GetComponent<BoxCollider2D>();
        if( _collider2D != null )
        {
            _collider2D.isTrigger = true;
            _collider2D.enabled = false;
        }
    }
    private void OnEnable()
    {
        _bActivate = true;
        StartCoroutine(IBlink());
    }

    IEnumerator IBlink()
    {
        while (_bActivate)
        {
            _collider2D.enabled = true;
            yield return new WaitForSeconds(_blinkTerm);
            _collider2D.enabled = false;
            yield return new WaitForSeconds(_blinkTerm);
        }
    }

    private void OnDisable()
    {
        _bActivate = false;
        StopCoroutine(IBlink());
    }
}
