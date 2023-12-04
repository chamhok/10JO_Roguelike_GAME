using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    public static int stageCount = 0;
    private static GameObject nextStagePrefab = null;

    public float stageLapseTime;

    /// <summary>
    /// player 객체 참조 <br/>
    /// 추후에 자료형을 플레이어 클래스로 바꿔야함.
    /// </summary>
    public Player player;

    /// <summary>
    /// stage에 생성된 monster들을 담을 리스트 <br/>
    /// 추후에 자료형을 몬스터들의 최상위 클래스로 바꿔야함.
    /// </summary>
    public List<Monster> monsters;

    /// <summary>
    /// stage에 생성된 item들을 담을 리스트 <br/>
    /// 추후에 자료형을 droppable item들의 상위 클래스로 바꿔야함.
    /// </summary>
    public List<DroppableItem> items;

    public PoolManager poolManager;

    [Header("Events")]
    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;
    public UnityEvent OnStageClear;
    public UnityEvent OnStageFail;

    [Header("Prefabs")]
    public GameObject poolManagerPrefab;
    public GameObject UIManagerPrefab;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Data Manager면 몰라도 얘는 생성할 필요 없을 듯?
            }
            return instance;
        }
    }

    private GameManager() { }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
        }
    }

    private void Initialize()
    {
        Debug.Log($"{nameof(GameManager)} call {nameof(Initialize)}.");
        OnGameStart.AddListener(LoadStage);
        OnStageClear.AddListener(ToNextStage);
    }

    protected virtual void Start()
    {
        Time.timeScale = 1f;
        OnGameStart?.Invoke();
    }

    protected virtual void Update()
    {
        stageLapseTime += Time.deltaTime;

        // Debug Code
        if (Input.GetKeyDown(KeyCode.Space))
            GameOver(true);
    }

    public virtual void GameOver(bool isGameClear = false)
    {
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
        if (isGameClear) OnStageClear?.Invoke();
        else OnStageFail?.Invoke();
    }

    void PlayerInstatiate()
    {
        var obj = Resources.Load<GameObject>("Player");
        player = Instantiate(obj).GetComponent<Player>();
    }

    void StageInstantiate()
    {
        if (nextStagePrefab != null)
            Instantiate(nextStagePrefab);
    }

    private void LoadStage()
    {
        StageInstantiate();
        PlayerInstatiate();
        poolManager = Instantiate(poolManagerPrefab).GetComponent<PoolManager>();
        Instantiate(UIManagerPrefab);
        Resources.UnloadUnusedAssets();
    }

    public static void ToNextStage()
    {
        stageCount++;

        string path = "Prefab/Stage/" + $"Stage{stageCount}Grid";
        nextStagePrefab = Resources.Load<GameObject>(path);
        if (nextStagePrefab != null)
            SceneManager.LoadScene("StageScene");
        else
        {
            stageCount = 0;
            SceneManager.LoadScene("GameStartScene");
        }
    }
}