using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
        public Transform[] SpawnPoint; // 스폰 위치를 저장하는 배열
        public static List<SpawnData[]> StageSpawnDatas = new List<SpawnData[]>(); // 각 스테이지별 스폰 데이터를 저장하는 리스트
        public SpawnData[] StageOneSpawnData; // 스테이지 1 스폰 데이터
        public SpawnData[] StageTwoSpawnData; // 스테이지 2 스폰 데이터
        public SpawnData[] StageThreeSpawnData; // 스테이지 3 스폰 데이터

        int level; // 현재 레벨
        float timer; // 타이머

        // 초기 설정
        private void Awake()
        {
                SpawnPoint = GetComponentsInChildren<Transform>();
                StageSpawnDatas.Add(StageOneSpawnData);
                StageSpawnDatas.Add(StageTwoSpawnData);
                StageSpawnDatas.Add(StageThreeSpawnData);
        }

        void Update()
        {
                if (GameManager.Instance.player.isDead) return; // 플레이어가 죽었으면 아무것도 실행하지 않음
                if (GameManager.Instance.bossZen)
                        return; // 보스가 스폰되었으면 다른 몬스터는 스폰하지 않음
                UpdateTimerAndLevel(); // 타이머와 레벨 업데이트
                SpawnBossIfTime(); // 시간이 되었으면 보스 스폰
                SpawnMonsterIfTime(); // 시간이 되었으면 몬스터 스폰
        }

        /// <summary>
        /// 시간이 되었으면 몬스터 스폰
        /// </summary>
        private void SpawnMonsterIfTime()
        {
                if (timer > StageSpawnDatas[GameManager.stageCount-1][level].spawnTime)
                {
                        timer = 0;
                        Spawn();
                }
        }

        /// <summary>
        /// 시간이 되었으면 보스 몬스터 스폰
        /// </summary>
        private void SpawnBossIfTime()
        {
                if (GameManager.Instance.stageLapseTime > GameManager.Instance.bossZenTime && !GameManager.Instance.bossZen)
                {
                        GameObject monster = GameManager.Instance.poolManager.Get(GameManager.stageCount-1);
                        monster.transform.localScale = new Vector3(10, 10, 1);
                        monster.transform.GetComponent<CapsuleCollider2D>().size = new Vector2(1, 1);
                        monster.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
                        monster.GetComponent<Monster>().Init(StageSpawnDatas[GameManager.stageCount-1][StageSpawnDatas[GameManager.stageCount-1].Length - 1]);
                        GameManager.Instance.bossZen = true;
                }
        }

        /// <summary>
        /// 타이머와 레벨 업데이트
        /// </summary>
        private void UpdateTimerAndLevel()
        {
                timer += Time.deltaTime;
                level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.stageLapseTime / 2f), StageSpawnDatas[GameManager.stageCount-1].Length - 1);
                level = level == StageSpawnDatas[GameManager.stageCount-1].Length - 1 ? level - 1 : level;
        }

        /// <summary>
        /// 몬스터 스폰
        /// </summary>        
        void Spawn()
        {
                GameObject monster = GameManager.Instance.poolManager.Get(GameManager.stageCount-1);
                monster.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
                monster.GetComponent<Monster>().Init(StageSpawnDatas[GameManager.stageCount-1][level]);
        }
}

[System.Serializable]
public class SpawnData // 스폰 데이터 클래스
{
        public int spriteType; // 스프라이트 타입
        public float spawnTime; // 스폰 시간
        public int health; // 체력
        public float speed; // 속도
}
