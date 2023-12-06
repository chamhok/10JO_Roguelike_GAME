using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Item : MonoBehaviour
{
    public static int MaxLevel = 5;

    public Define.EItemType Type
    {
        get;
        protected set;
    }

    public int Lv
    {
        get;
        protected set;
    }

    public Player Player
    {
        get;
        set;
    }

    protected int _property = 0;

    public int Property
    {
        get
        {
            if (0 == _property) return Lv;
            else return _property;
        }
    }

    private void Start()
    {
        GameManager.Instance.OnGameOver.AddListener(StopOperation);
        GameManager.Instance.OnStageClear.AddListener(StopOperation);
        GameManager.Instance.OnStageFail.AddListener(StopOperation);
    }

    private void StopOperation()
    {
        gameObject.SetActive(false);
    }

    public bool IsMaxLevel()
    {
        return Lv == MaxLevel;
    }

    public abstract void Upgrade();

    public virtual void SetProperty(int val)
    {
        for(int i = 0; i < val; ++i)
        {
            Upgrade();
        }        
    }
}
