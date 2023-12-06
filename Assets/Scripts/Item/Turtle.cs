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

        _comments[0] = "보호막 1회 추가";
        _comments[1] = "보호막 1회 추가";
        _comments[2] = "보호막 1회 추가";
        _comments[3] = "보호막 1회 추가";
        _comments[4] = "보호막 1회 추가";

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
        Lv = _deffensableCount > 0 ? 1 : 0;
        _property = _deffensableCount;
        _turtle.SetActive(_deffensableCount > 0);
    }

    public override void SetProperty(int val)
    {
        _deffensableCount = val;
        CheckDeffensable();
    }
}
