using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float Damege;
    public bool IsBoss;

    bool isLive;

    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    // [우진영] 드랍 아이템 연결을 위해 추가
    public int exp;
    public int money;
    GameObject expPrefab;
    GameObject moneyPrefab;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();

        expPrefab = Resources.Load<GameObject>("Item/Droppable/Droppable Exp");
        moneyPrefab = Resources.Load<GameObject>("Item/Droppable/Droppable Money");
    }

    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.player.isDead) return;

        if (!isLive) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        Damege = data.Damege;
        IsBoss = data.IsBoss;
        exp = data.exp;
        money = data.money;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon") || !isLive) return;

        if (collision.GetComponent<Weapon>() != null)
            health -= collision.GetComponent<Weapon>().Damage;

        if (health > 0)
        {
            StartCoroutine(KnockBack());

            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            if (IsBoss)
            {
                GameManager.Instance.GameOver(true);
            }
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 다음 하나의 물리 프레임까지 기다림
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void Dead()
    {
        gameObject.SetActive(false);
        var expObj = Instantiate(expPrefab, transform.position, Quaternion.identity).GetComponent<Droppable_EXP>();
        var moneyObj = Instantiate(moneyPrefab, transform.position, Quaternion.identity).GetComponent<Droppable_Money>();
        expObj.value = exp;
        moneyObj.value = money;
    }
}
