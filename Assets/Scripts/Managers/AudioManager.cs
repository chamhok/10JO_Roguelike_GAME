using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip bgm;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject(nameof(AudioManager));
                instance = obj.AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    private AudioManager() { }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            instance.Initialize();
            audioSource.playOnAwake = true;
            ChangeBgm();
            Debug.Log("오디오 초기화");
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (instance != this)
            Destroy(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (SceneManager.GetActiveScene().name != "UpgradeScene")
            ChangeBgm();
    }
    private void Initialize()
    {
        bgm = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_1(8Bit)");
        audioSource.clip = bgm;
    }

    public void ChangeBgm()
    {
        AudioClip newClip = null;

        if(SceneManager.GetActiveScene().name == "GameStartScene")
        {
            newClip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/BGM_1");
        }
        else if(SceneManager.GetActiveScene().name == "StageScene")
        {
            switch (GameManager.stageCount)
            {
                case 1:
                    newClip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_1(8Bit)");
                    break;
                case 2:
                    newClip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_2(8Bit)");
                    break;
                case 3:
                    newClip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_3(8Bit)");
                    break;
                default:
                    newClip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_1(8Bit)");
                    break;
            }
        }
        else if(SceneManager.GetActiveScene().name == "StoryScene")
        {
            newClip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_1");
        }

        if (audioSource?.clip?.name != newClip?.name)
        {
            audioSource.clip = newClip;
            audioSource.Stop();
            audioSource.Play();
        }
    }
}
