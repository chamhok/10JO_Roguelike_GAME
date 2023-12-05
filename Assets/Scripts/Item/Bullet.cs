using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Bullet : Weapon
{
    private void Awake()
    {
        _collider2D = GetComponent<CircleCollider2D>();       
        if( _collider2D != null )
        {
            _collider2D.isTrigger = true;
        }
    }
}
