using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

class Turtle : Item
{
    int _deffensableCount = 0;
    // int _maxDeffensableCount = 5;
    CircleCollider2D _circleCollider2D;
    GameObject _turtle;
    private void Awake()
    {
        Type = Define.EItemType.Turtle;

        _circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        if(_circleCollider2D == null) _circleCollider2D = gameObject.AddComponent<CircleCollider2D>();

        _circleCollider2D.isTrigger = true;
        _turtle = Instantiate(Resources.Load<GameObject>("Item/Turtle"));
        _turtle.transform.parent = this.transform;
        CheckDeffensable();
    }

    public override void Upgrade()
    {
        ++_deffensableCount;
        CheckDeffensable();
    }

    private void CheckDeffensable()
    {
        _circleCollider2D.enabled = _deffensableCount > 0;
        _turtle.SetActive(_deffensableCount > 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Turtle Trigger");
        //if(collision.gameObject.tag.Compare())
        --_deffensableCount;
        CheckDeffensable();
    }
}
