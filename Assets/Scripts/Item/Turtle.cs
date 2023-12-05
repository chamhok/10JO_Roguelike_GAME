using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

class Turtle : Item
{
    int _deffensableCount = 0;
    public int Count
    {
        get         
        {
            if (_deffensableCount == 0) return 0;
            int count = _deffensableCount--;
            CheckDeffensable();
            return count; 
        }
    }

    GameObject _turtle;
    private void Awake()
    {
        Type = Define.EItemType.Turtle;

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
        Debug.Log($"Shield [{_deffensableCount}]");
        //_circleCollider2D.enabled = _deffensableCount > 0;
        _turtle.SetActive(_deffensableCount > 0);
    }
}
