using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
        Collider2D coll;

        private void Awake()
        {
                coll = GetComponent<Collider2D>();
        }
        void OnTriggerExit2D(Collider2D collision)
        {
                if (!collision.CompareTag("Area")) // 박스콜라이더를 플레이어에게 만들고 그것에 태그를 달면 됨.
                        return;

                Vector3 playerPos = GameManager.Instance.player.transform.position;
                Vector3 myPos = transform.position;

                switch (transform.tag)
                {
                        case "Monster":
                                if (coll.enabled)
                                {
                                        Vector3 dist = playerPos - myPos;
                                        Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);

                                        transform.Translate(ran + dist * 2);
                                }
                                break;
                }
        }
}