using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider2D;
    [SerializeField] float _damage;
    
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{gameObject.name} Hit {collision.gameObject.name} : {Damage}");
    }
}
