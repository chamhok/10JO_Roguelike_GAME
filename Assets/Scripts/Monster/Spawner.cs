using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoint; // ���� ��ġ�� �����ϴ� �迭
    public static List<SpawnData[]> StageSpawnDatas = new List<SpawnData[]>(); // �� ���������� ���� �����͸� �����ϴ� ����Ʈ
    public SpawnData[] StageOneSpawnData; // �������� 1 ���� ������
    public SpawnData[] StageTwoSpawnData; // �������� 2 ���� ������
    public SpawnData[] StageThreeSpawnData; // �������� 3 ���� ������

    int level; // ���� ����
    float timer; // Ÿ�̸�
    GameObject boss;
    // �ʱ� ����
    private void Awake()
    {
        SpawnPoint = GetComponentsInChildren<Transform>();
        StageSpawnDatas.Add(StageOneSpawnData);
        StageSpawnDatas.Add(StageTwoSpawnData);
        StageSpawnDatas.Add(StageThreeSpawnData);
    }

    void Update()
    {
        if (GameManager.stageCount > 0)
        {
            if (GameManager.Instance.player.isDead) return; // �÷��̾ �׾����� �ƹ��͵� �������� ����
            if (GameManager.Instance.bossZen)
                return; // ������ �����Ǿ����� �ٸ� ���ʹ� �������� ����
            UpdateTimerAndLevel(); // Ÿ�̸ӿ� ���� ������Ʈ
            SpawnBossIfTime(); // �ð��� �Ǿ����� ���� ����
            SpawnMonsterIfTime(); // �ð��� �Ǿ����� ���� ����
        }
    }

    /// <summary>
    /// �ð��� �Ǿ����� ���� ����
    /// </summary>
    private void SpawnMonsterIfTime()
    {
        if (timer > StageSpawnDatas[GameManager.stageCount - 1][level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    /// <summary>
    /// �ð��� �Ǿ����� ���� ���� ����
    /// </summary>
    private void SpawnBossIfTime()
    {
        if (GameManager.Instance.stageLapseTime > GameManager.Instance.bossZenTime && !GameManager.Instance.bossZen)
        {
            Debug.Log($"Spawn Boss {GameManager.Instance.stageLapseTime} / {GameManager.Instance.bossZenTime}");
            boss = GameManager.Instance.poolManager.Get(GameManager.stageCount - 1);
            boss.transform.localScale = new Vector3(10, 10, 1);
            boss.transform.GetComponent<CapsuleCollider2D>().size = new Vector2(1, 1);
            boss.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
            boss.GetComponent<Monster>().Init(StageSpawnDatas[GameManager.stageCount - 1][StageSpawnDatas[GameManager.stageCount - 1].Length - 1]);
            GameManager.Instance.bossZen = true;
        }
    }

    /// <summary>
    /// Ÿ�̸ӿ� ���� ������Ʈ
    /// </summary>
    private void UpdateTimerAndLevel()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.stageLapseTime / 2f), StageSpawnDatas[GameManager.stageCount - 1].Length - 1);
        level = level == StageSpawnDatas[GameManager.stageCount - 1].Length - 1 ? level - 1 : level;
    }

    /// <summary>
    /// ���� ����
    /// </summary>        
    void Spawn()
    {
        GameObject monster = GameManager.Instance.poolManager.Get(GameManager.stageCount - 1);
        monster.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
        monster.GetComponent<Monster>().Init(StageSpawnDatas[GameManager.stageCount - 1][level]);
    }
}

[System.Serializable]
public class SpawnData // ���� ������ Ŭ����
{
    public int spriteType; // ��������Ʈ Ÿ��
    public float spawnTime; // ���� �ð�
    public int health; // ü��
    public float speed; // �ӵ�
    public float Damege; // �����
    public bool IsBoss; // ���������ΰ�
}
