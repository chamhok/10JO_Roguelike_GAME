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
            instance = this;
        instance.Initialize();
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
        // ���� �������� �ʱ�ȭ
        LoadData();
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

    public void LoadData()
    {
        // Set default data..
        playerData = new();
        // Load data..
        var playerDataJson = PlayerPrefs.GetString(nameof(playerData));
        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
        Debug.Log($"{name} call {nameof(LoadData)} is success");
    }
}

/// <summary>
/// �÷��̾� ĳ���Ϳ� ���õ� ���� �߿� ������ �ʿ��� �� ������ ���⿡ �߰����ֽø� �˴ϴ�.
/// </summary>
[System.Serializable]
public class PlayerData
{
    public float maxHp;
    public float atk;
    public float speed;
    public int level;
    public int currentExp;
    public int money;
    public int[] upgradeLevel = new int[3];

    public PlayerData()
    {
        maxHp = 100;
        level = 1;
        atk = 1;
        speed = 1;
        currentExp = 0;
        money = 0;
    }
}