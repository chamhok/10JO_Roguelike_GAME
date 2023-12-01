using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
        public Transform[] spqwnPoint;
        public SpawnData[] spawnData;
        int level;
        float timer;

        private void Awake()
        {
                spqwnPoint = GetComponentsInChildren<Transform>();
        }
        // Update is called once per frame
        void Update()
        {
                //if (!GameManager.Instance.player.isLive) return;

                timer += Time.deltaTime;
                level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.stageLapseTime / 10f), spawnData.Length - 1);

                if (timer > spawnData[level].spawnTime)
                {
                        timer = 0;
                        Spawn();
                }
        }

        void Spawn()
        {
                GameObject monster = GameManager.Instance.poolManager.Get(0);
                monster.transform.position = spqwnPoint[Random.Range(1, spqwnPoint.Length)].position;
                monster.GetComponent<Monster>().Init(spawnData[level]);
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