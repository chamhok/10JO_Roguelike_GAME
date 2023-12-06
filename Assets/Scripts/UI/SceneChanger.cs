using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ToStageScene()
    {
        DataManager.Instance.playerData.SetDefaultInStageData();
        DataManager.Instance.ItemDict = null;
        SceneManager.LoadScene("StoryScene");
    }

    public void ToUpgradeScene()
    {
        UIManager.isUpgrade = true;
        SceneManager.LoadScene("UpgradeScene");
    }

    public void ToStartScene()
    {
        UIManager.isUpgrade = false;
        SceneManager.LoadScene("GameStartScene");
    }
}
