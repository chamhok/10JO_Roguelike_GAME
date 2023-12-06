using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public PlayerData playerData;

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject(nameof(DataManager));
                instance = obj.AddComponent<DataManager>();
                instance.Initialize();
            }
            return instance;
        }
    }

    private DataManager() { }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Initialize();
        }
    }

    private void Start()
    {
        if (instance != this)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            Debug.Log($"{name} call {nameof(OnDestroy)}");
            SaveData();
        }
    }

    private void Initialize()
    {
        // 각종 정보들을 초기화
        if (!LoadData())
        {
            // 불러올 수 없다면 기본값으로 세팅
            Debug.Log($"{name} call {nameof(LoadData)} is fail. redirect {nameof(CreateDefaultData)}...");
            CreateDefaultData();
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool SaveData()
    {
        try
        {
            // Save data..
            var playerDataJson = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(nameof(playerData), playerDataJson);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        Debug.Log($"{name} call {nameof(SaveData)} is success");
        return true;
    }

    public bool LoadData()
    {
        // Set default data..
        playerData = new();
        // Load data..
        var playerDataJson = PlayerPrefs.GetString(nameof(playerData));
        var loadedPlayerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
        if (loadedPlayerData != null)
            playerData = loadedPlayerData;
        Debug.Log($"{name} call {nameof(LoadData)} is success");
        return true;
    }

    public void CreateDefaultData()
    {
        // 모든 데이터를 기본값으로 설정...
        playerData = new();
    }
}

/// <summary>
/// 플레이어 캐릭터와 관련된 정보 중에 저장이 필요할 것 같으면 여기에 추가해주시면 됩니다.
/// </summary>
[System.Serializable]
public class PlayerData
{
    public float maxHp;
    public float currentHp;
    public float atk;
    public float speed;
    public int level;
    public int currentExp;
    public int money;
    public int[] upgradeLevel = new int[3] { 0, 0, 0 };

    public PlayerData()
    {
        maxHp = 100;
        level = 1;
        currentHp = 100;
        atk = 1;
        speed = 1;
        currentExp = 0;
        money = 0;
    }
}