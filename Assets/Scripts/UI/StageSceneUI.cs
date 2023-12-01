using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageSceneUI : MonoBehaviour
{
    Stage currentStage;
    private Canvas curUI;
    public void ShowData()
    {
        ChangeStageName();
        ShowTime();
        ShowExp();
        ShowGold();
        ShowLevel();
    }
    public void ChangeStageName()
    {
        switch (currentStage)   //GameManager의 변수 Static이라서 접근 불가..
        {
            case Stage.Stage1:
                curUI.GetComponentInChildren<TMP_Text>().text = "검수지옥";
                break;
            case Stage.Stage2:
                curUI.GetComponentInChildren<TMP_Text>().text = "나태지옥";
                break;
            case Stage.Stage3:
                curUI.GetComponentInChildren<TMP_Text>().text = "흑암지옥";
                break;
            default: break;
        }
    }

    //시간 표시
    private void ShowTime()
    {
        int remainedtime = (int)(120f - GameManager.Instance.stageLapseTime);
        curUI.transform.Find("Timer").GetComponent<TMP_Text>().text = (remainedtime / 60).ToSafeString() + " : " + (remainedtime % 60).ToSafeString();
    }

    //레벨 표시
    private void ShowLevel()
    {
        curUI.GetComponentInChildren<Slider>().transform.Find("Level").GetComponent<TMP_Text>().text = "LV " + GameManager.Instance.player.level.ToString();
    }

    //경험치 표시, 최대 경험치 필요함. 일단 10으로 가정.
    private void ShowExp()
    {
        curUI.GetComponentInChildren<Slider>().value = GameManager.Instance.player.exp / GameManager.Instance.player.level * 5;
    }

    //골드 표시
    private void ShowGold()
    {
        GameObject gold = curUI.transform.Find("Gold").gameObject;
        if (gold != null)
        {
            TMP_Text GoldText = gold.GetComponentInChildren<TMP_Text>();
            GoldText.text = GameManager.Instance.player.money.ToString();
        }
        else
        {
            Debug.Log("Gold is null");
        }

    }
}
