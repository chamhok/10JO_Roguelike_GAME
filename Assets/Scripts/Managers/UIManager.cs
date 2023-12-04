using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIManager : MonoBehaviour
{
    private Canvas CurrentUI;
    private Canvas curUI;
    public static bool isUpgrade = false;
    private int currentStage;
    private int LvFlag;
    private bool isAlive = true;
    private string[] itemNames = new string[] { " ", "돌", "사슴", "해", "달", "두루미", "소나무", "물", "거북이", "불로초", "산" };
    private void Awake()
    {
        currentStage = GameManager.stageCount;
        if (currentStage == 0)
        {
            if (isUpgrade)
            {
                CurrentUI = Resources.Load<Canvas>("UI\\UpgradeUI");
            }
            else
            {
                CurrentUI = Resources.Load<Canvas>("UI\\StartSceneUI");
            }
        }
        else
        {
            CurrentUI = Resources.Load<Canvas>("UI\\StageUI");
        }
    }

    private void Start()
    {
        curUI = Instantiate(CurrentUI);
        if (currentStage == 0)
        {

        }
        else
        {
            StartCoroutine(ShowStageName());
        }
        if (currentStage > 0)
        {
            LvFlag = GameManager.Instance.player.level;
        }
    }

    private void Update()
    {
        if (currentStage == 0)
        {
            if (isUpgrade)
            {
                ShowUpgradeData();
            }
            else
            {
                ShowStartData();
            }
        }
        else
        {
            ShowStageData();

            if (LvFlag != GameManager.Instance.player.level)
            {
                LvFlag = GameManager.Instance.player.level;
                SelectItem();
            }

            if (GameManager.Instance.player.hp <= 0&&isAlive == true)
            {
                isAlive = false;
                ShowGameOver();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && isAlive == false)
            {
                GameManager.stageCount = 0;
                SceneManager.LoadScene("GameStartScene");
            }
        }
    }

    private void ShowStageData()
    {
        ChangeStageName();
        ShowTime();
        ShowExp();
        ShowGold();
        ShowLevel();
        ShowItem();
    }

    private void ShowStartData()
    {
        ShowGold();
    }

    private void ShowUpgradeData()
    {
        ShowGold();
        ShowUpgradeLevel();
    }

    public void ChangeStageName()
    {
        switch (GameManager.stageCount)
        {
            case 1:
                curUI.GetComponentInChildren<TMP_Text>().text = "검수지옥";
                break;
            case 2:
                curUI.GetComponentInChildren<TMP_Text>().text = "나태지옥";
                break;
            case 3:
                curUI.GetComponentInChildren<TMP_Text>().text = "흑암지옥";
                break;
            default: break;
        }
    }

    //업그레이드 레벨 슬라이더 조정
    private void ShowUpgradeLevel()
    {
        //데이터매니저에서 만들어지면 추가 가능.
        //curUI.GetComponentsInChildren<Slider>()[0].value = DataManager.Instance.playerData.UpgradeLv[0];
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
            GoldText.text = DataManager.Instance.playerData.money.ToString();
            //GoldText.text = GameManager.Instance.player.money.ToString();
        }
        else
        {
            Debug.Log("Gold is null");
        }
    }

    //가지고 있는 아이템 좌상단에 출력
    private void ShowItem()
    {
        GameObject item = curUI.transform.Find("Items").gameObject;

        //foreach(Define.EItemType i in GameManager.Instance.items)
        //{
        //    Image nitem = item.transform.Find($"Item{(int)i}").GetComponent<Image>();
        //    nitem.transform.Find("ItemImage").gameObject.SetActive(true);
        //}

        for (int i = 1; i < 9; i++)
        {
            Image nitem = item.transform.Find($"Item{i}").GetComponent<Image>();
            var obj = nitem.transform.Find("ItemImage").gameObject;
            if (obj != null)
            {
                obj.SetActive(true);
            }
            else Debug.Log($"{i}번째에서 문제 발생");
        }
    }

    //레벨업시 아이템 선택창 출력
    private void SelectItem()
    {
        Time.timeScale = 0;
        curUI.transform.Find("SelectItem").gameObject.SetActive(true);

        //랜덤 아이템 3개 생성을 위한 숫자 생성
        int[] randomItemNum = new int[3];
        int index = 0;
        while (index < 3)
        {
            int rnum = UnityEngine.Random.Range(1, 11);
            if (Array.IndexOf(randomItemNum, rnum) == -1)
            {
                randomItemNum[index] = rnum;
                index++;
            }
        }

        var select = curUI.transform.Find("SelectItem").gameObject;
        for (int i = 1; i < 4; i++)
        {
            //아이템 이미지 변경
            Image selectItem = select.transform.Find($"Item{i}Border").GetComponent<Image>();
            var obj = selectItem.transform.Find("ItemImage").gameObject.GetComponent<Image>();
            if (obj != null)
            {
                obj.sprite = Resources.Load<Sprite>($"UI\\Item\\Item{randomItemNum[i - 1]}");
            }

            //아이템 이름 변경
            TMP_Text itemName = selectItem.transform.Find("ItemText").GetComponent<TMP_Text>();
            itemName.text = itemNames[randomItemNum[i-1]];

            //선택하면 창 닫히도록
            Button selectButton = select.transform.Find($"Item{i}Border").GetComponent<Button>();
            selectButton.onClick.AddListener(ChoiceItem);
        }
    }

    //선택시 아이템 장착, 창 닫음
    private void ChoiceItem()
    {
        Time.timeScale = 1;
        curUI.transform.Find("SelectItem").gameObject.SetActive(false);
    }

    private void ShowGameOver()
    {
        Time.timeScale = 0;
        curUI.transform.Find("GameOver").gameObject.SetActive(true);
    }
    private IEnumerator ShowStageName()
    {
        if (curUI != null)
        {
            TMP_Text StageName = curUI.GetComponentInChildren<TMP_Text>();
            if (StageName != null)
            {
                yield return new WaitForSecondsRealtime(2f);

                StageName.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Text 못찾음");
            }
        }
        else
        {
            Debug.LogError("curUI is null.");
        }
    }
}

