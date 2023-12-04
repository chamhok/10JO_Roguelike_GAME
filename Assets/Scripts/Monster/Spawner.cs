using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
        public Transform[] spqwnPoint;
        public static List<SpawnData[]> StageSpawnDatas = new List<SpawnData[]>();
        public SpawnData[] StageOneSpawnData;
        public SpawnData[] StageTowSpawnData;
        public SpawnData[] StageThreeSpawnData;
        
        int level;
        float timer;
      
        private void Awake()
        {
                spqwnPoint = GetComponentsInChildren<Transform>();
                StageSpawnDatas.Add(StageOneSpawnData);
                StageSpawnDatas.Add(StageTowSpawnData);
                StageSpawnDatas.Add(StageThreeSpawnData);
        }
        // Update is called once per frame
        void Update()
        {
                //if (!GameManager.Instance.player.isLive) return;

                timer += Time.deltaTime;
                level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.stageLapseTime /2f), StageSpawnDatas[GameManager.stageCount].Length - 1);
                level = level == StageSpawnDatas[GameManager.stageCount ].Length - 1 ? level - 1 : level;
                if (GameManager.Instance.stageLapseTime > GameManager.Instance.bossZenTime && !GameManager.Instance.bossZen)
                {
                        GameObject monster = GameManager.Instance.poolManager.Get(GameManager.stageCount );
                        monster.transform.localScale = new Vector3(10, 10, 1);
                        Debug.Log(monster.gameObject.GetInstanceID());
                        monster.transform.GetComponent<CapsuleCollider2D>().size = new Vector2(1, 1);
                        monster.transform.position = spqwnPoint[Random.Range(1, spqwnPoint.Length)].position;
                        monster.GetComponent<Monster>().Init(StageSpawnDatas[GameManager.stageCount][StageSpawnDatas[GameManager.stageCount].Length-1]);
                        GameManager.Instance.bossZen = true;
                }
                if (timer > StageSpawnDatas[GameManager.stageCount][level].spawnTime)
                {
                        timer = 0;
                        Spawn();
                }

        }

        void Spawn()
        {
                GameObject monster = GameManager.Instance.poolManager.Get(GameManager.stageCount);
                monster.transform.position = spqwnPoint[Random.Range(1, spqwnPoint.Length)].position;
                monster.GetComponent<Monster>().Init(StageSpawnDatas[GameManager.stageCount ][level]);
        }
}

[System.Serializable]
public class SpawnData
{
        public int spriteType;
        public float spawnTime;
        public int health;
        public float speed;
}