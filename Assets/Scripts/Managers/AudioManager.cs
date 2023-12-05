using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Initialize()
    {
        bgm = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_1(8Bit)");
        audioSource.clip = bgm;
    }

    public void ChangeBgm()
    {
        audioSource.Stop();
        switch(GameManager.stageCount)
        {
            case 0:
                audioSource.clip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_1(8Bit)");
                break;
            case 1:
                audioSource.clip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_2(8Bit)");
                break;
            case 2:
                audioSource.clip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_3(8Bit)");
                break;
            case 3:
                audioSource.clip = Resources.Load<AudioClip>("GameSound/BackgroundMusic/StageBGM_3");
                break;
        }
        audioSource.Play();
    }
}
