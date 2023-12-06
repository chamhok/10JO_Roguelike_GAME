using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    public Rigidbody2D _rigidbody;
    public SpriteRenderer _sprite;
    AudioSource ApplyDamage;
    
    
    public float maxHp;     //최대체력
    public float hp;        //체력
    public float atk;       //공격력 배율
    public float speed;     //이동속도 배율
    public int level;       //레벨
    public int currentExp;  //경험치   
    public int maxExp;
    public int money;       //돈
    public bool isDead;

    int layer_name;
    

    public Player()
    {
        maxHp = 100; //기본 최대 HP
        hp = maxHp;  //최대 HP -> 스테이지 입장마다 최대 HP로 초기화
        atk = 1;     //공격력 배율(무기 데미지 * atk)
        speed = 1;   //이동속도 배율
        level = 1;   //현재 레벨(게임 오버, 게임 클리어 시 초기화 - 스테이지 클리어 아님)
        currentExp = 0;     //현재 exp(게임 오버, 게임 클리어 시 초기화 - 스테이지 클리어 아님)
        maxExp = 50;
        money = 0;   //현재 gold(메인화면, 스테이터스 강화 화면에서 사용하는 것으로 maxHp, atk, speed를 영구적으로 증가)
                     //증가할때 마다 필요한 money 증가
        isDead = false;
    }

    public Player(PlayerData playerData)
    {
        maxHp = playerData.maxHp;
        atk = playerData.atk;
        speed = playerData.speed;
        level = playerData.level;

        currentExp = playerData.currentExp;
        money = playerData.money;
        isDead = false;
    }

    private void Awake()
    {
        layer_name = LayerMask.NameToLayer("Player");
        this.gameObject.layer = layer_name;
        _rigidbody = GetComponent<Rigidbody2D>();
        ApplyDamage = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //ChangeHpBar(hp); //매 프레임 플레이어 체력바 갱신 


    }

    private void ChangeHpBar(float hp) //현재 체력 체력바에 표시
    {
        hpBar.fillAmount = hp / maxHp;
    }

    private void OnCollisionEnter2D(Collision2D collision)  //적 투사체에 맞을 때 데미지 판정
    {
        layer_name = LayerMask.NameToLayer("Attack");
        if (GameManager.Instance.player.isDead)
            return;
        else
        {
            if (GameManager.Instance.player.hp <= 0)
            {
                isDead = true;
                GameManager.Instance.GameOver();
            }
            if (collision.gameObject.layer == layer_name)
            {
                ApplyDamage.Play();
                OnDamage(collision.gameObject.layer); //몬스터 공격 데미지로 수정 예정
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) //적과 접촉 시 데미지 적용
    {
        layer_name = LayerMask.NameToLayer("Monster");
        if (GameManager.Instance.player.isDead)
            return;
        else
        {
            if (GameManager.Instance.player.hp <= 0)
            {
                isDead = true;
                GameManager.Instance.GameOver();
            }
            if (collision.gameObject.layer == layer_name)
            {
                ApplyDamage.Play();
                OnDamage(10);
            }
        }
    }

    void OnDamage(int damage)
    {
        Debug.Log("Attacked");
        if (GetComponent<ItemManager>().HaveActivatedShield())
        {
            gameObject.layer = 20;
            Invoke("OffDamage", 1);
            return;
        }
        _sprite.color = new Color(1, 1 , 1, 0.4f);
        GameManager.Instance.player.hp -= damage;
        gameObject.layer = 20;
        Invoke("OffDamage", 1);

        // [우진영] 데미지를 입었을 때 체력바를 갱신하게 변경
        ChangeHpBar(hp);
    }//데미지를 입은 경우

    void OffDamage()
    {
        _sprite.color = new Color(1, 1, 1, 1);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }//데미지를 받고 일정시간 동안 무적판정

    public void GetExp(int _exp)
    {
        currentExp += _exp;
        while (currentExp > maxExp)
        {
            GameManager.Instance.uiManager.LvFlag++;
            level++;
            currentExp -= maxExp;
            maxExp = (int)(1.2f * maxExp);
            Debug.Log($"player level up, LvFlag: {GameManager.Instance.uiManager.LvFlag}");
        }
    }//경험치 획득 시
}
