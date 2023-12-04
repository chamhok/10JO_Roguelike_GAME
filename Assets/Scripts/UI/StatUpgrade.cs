using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrade : MonoBehaviour
{
    public void HPUpgrade()
    {
        DataManager.Instance.playerData.maxHp += 50;
    }

    public void AttackUpgrade()
    {
        DataManager.Instance.playerData.atk += 1;
    }

    public void SpeedUpgrade()
    {
        DataManager.Instance.playerData.speed += 1;
    }


}
