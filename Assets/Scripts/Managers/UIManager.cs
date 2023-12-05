using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<Define.EItemType> myItems = new List<Define.EItemType>() { Define.EItemType.Stone };
    private bool isAlive = true;
    private GameObject square;
    private string[] itemNames = new string[] { "돌", "달", "거북이", "해", "소나무", "물", "두루미", "사슴", "불로초", "산" };
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
            if (currentStage == 2)
            {
                square = Instantiate((GameObject)Resources.Load("UI\\Square"));
            }
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

            if (currentStage == 2)
            {
                square.transform.Rotate(Vector3.back, 10f * Time.deltaTime);
            }

            if (GameManager.Instance.player.hp <= 0 && isAlive == true)
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

    #region StartScene
    private void ShowStartData()
    {
        ShowGold();
    }
    #endregion

    #region UpgradeScene
    private void ShowUpgradeData()
    {
        ShowGold();
        ShowUpgradeLevel();
    }

    //업그레이드 레벨 슬라이더 조정
    private void ShowUpgradeLevel()
    {
        Slider[] upgrade = curUI.GetComponentsInChildren<Slider>();

        for (int i = 0; i < upgrade.Length; i++)
        {
            if (upgrade[i] != null)
            {
                upgrade[i].value = DataManager.Instance.playerData.upgradeLevel[i];
            }
            else
            {
                Debug.Log("is Null");
            }

            TMP_Text[] info = upgrade[i].GetComponentsInChildren<TMP_Text>();
            info[0].text = "LV." + DataManager.Instance.playerData.upgradeLevel[i].ToString();
            info[2].text = ((DataManager.Instance.playerData.upgradeLevel[i] + 1) * 100).ToString();
        }


    }
    #endregion

    #region StageScene
    private void ShowStageData()
    {
        ChangeStageName();
        ShowTime();
        ShowExp();
        ShowGold();
        ShowLevel();
        ShowItem();
    }

    //스테이지 이름 출력
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

    //시간 표시
    private void ShowTime()
    {
        //2분을 시작으로 줄어들도록 설정
        int remainedtime = (int)(120f - GameManager.Instance.stageLapseTime);
        remainedtime = remainedtime <= 0 ? 0 : remainedtime;
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
        curUI.GetComponentInChildren<Slider>().value = GameManager.Instance.player.exp;
        curUI.GetComponentInChildren<Slider>().maxValue = GameManager.Instance.player.level * 5;
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
        foreach (Define.EItemType i in myItems)
        {
            Image nitem = item.transform.Find($"Item{(int)i + 1}").GetComponent<Image>();
            nitem.transform.Find("ItemImage").gameObject.SetActive(true);
        }
    }

    //레벨업시 아이템 선택창 출력
    private void SelectItem()
    {
        Time.timeScale = 0;
        curUI.transform.Find("SelectItem").gameObject.SetActive(true);

        //업그레이드 가능한 아이템 배열 가져오기
        Define.EItemType[] upitems = GameManager.Instance.player.GetComponent<ItemManager>().GetUpgradableItems();

        //랜덤 아이템 3개 생성
        System.Random random = new System.Random();
        upitems = upitems.OrderBy(x => random.Next()).Take(3).ToArray();

        var select = curUI.transform.Find("SelectItem").gameObject;
        for (int i = 0; i < upitems.Length; i++)
        {
            //아이템 이미지 변경
            Image selectItem = select.transform.Find($"Item{i + 1}Border").GetComponent<Image>();
            var obj = selectItem.transform.Find("ItemImage").gameObject.GetComponent<Image>();
            if (obj != null)
            {
                obj.sprite = Resources.Load<Sprite>($"UI\\Item\\Item{(int)upitems[i] + 1}");
            }

            //아이템 이름 변경
            TMP_Text itemName = selectItem.transform.Find("ItemText").GetComponent<TMP_Text>();
            itemName.text = itemNames[(int)upitems[i]];

            //선택한 아이템 장착
            Button selectButton = select.transform.Find($"Item{i + 1}Border").GetComponent<Button>();
            int index = i;
            selectButton.onClick.AddListener(() => ChoiceItem(upitems[index]));
        }
    }

    //선택시 아이템 장착, 창 닫음
    private void ChoiceItem(Define.EItemType item)
    {
        Time.timeScale = 1;
        curUI.transform.Find("SelectItem").gameObject.SetActive(false);
        //아이템 장착
        GameManager.Instance.player.GetComponent<ItemManager>().AddOrUpgradeItem(item);
        if (!myItems.Contains(item) && (int)item < 8)
        {
            myItems.Add(item);
        }

    }

    //게임오버 판넬
    private void ShowGameOver()
    {
        Time.timeScale = 0;
        GameObject gameover = curUI.transform.Find("GameOver").gameObject;

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
    #endregion
}

