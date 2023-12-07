using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrade : MonoBehaviour
{
    int price = 100;

    //일정 금액을 내고 업그레이드 가능

    public void HPUpgrade()
    {
        int hpPrice = price * (DataManager.Instance.playerData.upgradeLevel[0] + 1);
        if (DataManager.Instance.playerData.upgradeLevel[0] < 5 && DataManager.Instance.playerData.money >= hpPrice)
        {
            DataManager.Instance.playerData.maxHp += 50;
            DataManager.Instance.playerData.upgradeLevel[0]++;
            DataManager.Instance.playerData.money -= hpPrice;
            DataManager.Instance.SaveData();
        }

    }

    public void AttackUpgrade()
    {
        int atkPrice = price * (DataManager.Instance.playerData.upgradeLevel[1] + 1);
        if (DataManager.Instance.playerData.upgradeLevel[1] < 5 && DataManager.Instance.playerData.money >= atkPrice)
        {
            DataManager.Instance.playerData.atk += 5;
            DataManager.Instance.playerData.upgradeLevel[1]++;
            DataManager.Instance.playerData.money -= atkPrice;
            DataManager.Instance.SaveData();
        }

    }

    public void SpeedUpgrade()
    {
        int speedPrice = price * (DataManager.Instance.playerData.upgradeLevel[2] + 1);
        if (DataManager.Instance.playerData.upgradeLevel[2] < 5 && DataManager.Instance.playerData.money >= speedPrice)
        {
            DataManager.Instance.playerData.speed += 0.1f;
            DataManager.Instance.playerData.upgradeLevel[2]++;
            DataManager.Instance.playerData.money -= speedPrice;
            DataManager.Instance.SaveData();
        }

    }
}
