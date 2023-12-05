using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
        public float damage;
        Rigidbody2D rigid;
        Animator anim;

        private void Awake()
        {
                rigid = GetComponent<Rigidbody2D>();
                anim = GetComponent<Animator>();  
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
                if (!collision.collider.CompareTag("Player")) return;
                else if (collision.collider.CompareTag("Player"))
                {
                        rigid.velocity = Vector2.zero;
                        gameObject.SetActive(false);

                }
        }
}
