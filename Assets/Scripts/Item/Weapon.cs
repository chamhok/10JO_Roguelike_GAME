using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float _colliderRadius;

    CircleCollider2D _weaponCollider;

    public float Damage
    {
        get;
        set;
    }


    private void Awake()
    {
        _weaponCollider = GetComponent<CircleCollider2D>();
        if(_weaponCollider == null) _weaponCollider = gameObject.AddComponent<CircleCollider2D>();
        _weaponCollider.isTrigger = true;
        _weaponCollider.radius = _colliderRadius;
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if( collision.gameObject.tag.Compare() )
        // monster.ApplyDamage();
        Debug.Log($"{gameObject.name} Hit {collision.gameObject.name}");
    }
}
