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

    public bool IsMaxLevel()
    {
        return Lv == MaxLevel;
    }

    public abstract void Upgrade();
}
