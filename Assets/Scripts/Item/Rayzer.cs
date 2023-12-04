using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayzer : MonoBehaviour
{
    BoxCollider2D _boxCollider2D;
    float _blinkTerm = 0.1f;
    bool _bActivate = false;

    public float Damage
    {
        get;
        set;
    }

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.enabled = false;
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
            _boxCollider2D.enabled = true;
            yield return new WaitForSeconds(_blinkTerm);
            _boxCollider2D.enabled = false;
            yield return new WaitForSeconds(_blinkTerm);            
        }
    }

    private void OnDisable()
    {
        _bActivate = false;
        StopCoroutine(IBlink());
    }
}
