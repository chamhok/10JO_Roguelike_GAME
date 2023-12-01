using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToStageScene()
    {
        GameManager.stageCount = 1;
        SceneManager.LoadScene("StageScene");
    }

    public void ToUpgradeScene()
    {
        SceneManager.LoadScene("UpgradeScene");
    }

    public void ToStartScene()
    {
        SceneManager.LoadScene("GameStartScene");
    }
}
