using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject[] StageGrid;
    private Canvas StageUI;
    private Canvas curUI;
    private Stage currentStage = Stage.Stage1;

    private void Awake()
    {
        StageGrid = Resources.LoadAll<GameObject>("UI\\Stage");
        StageUI = Resources.Load<Canvas>("UI\\StageUI");
    }

    private void Start()
    {
        curUI = Instantiate(StageUI);
        Instantiate(StageGrid[(int)currentStage]);

        StartCoroutine(ShowStageName());

        GameManager.Instance.OnStageClear.AddListener(StageUp);
        GameManager.Instance.OnStageFail.AddListener(StageReset);
    }

    private void StageUp()
    {
        currentStage++;
    }

    private void StageReset()
    {
        currentStage = Stage.Stage1;
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
                Debug.LogError("Text ¸øÃ£À½");
            }
        }
        else
        {
            Debug.LogError("curUI is null.");
        }
    }
}

enum Stage
{
    Stage1,
    Stage2,
    Stage3
}
