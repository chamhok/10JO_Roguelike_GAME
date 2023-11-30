using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int maxHp;
    int hp;
    int atk; //공격력 배율
    public int speed;//이동속도 배율
    int level;
    int exp;
    int money;

    public Player()
    {
        maxHp = 100; //기본 최대 HP
        hp = maxHp;  //최대 HP -> 스테이지 입장마다 최대 HP로 초기화
        atk = 1;     //공격력 배율(무기 데미지 * atk)
        speed = 1;   //이동속도 배율
        level = 1;   //현재 레벨(게임 오버, 게임 클리어 시 초기화 - 스테이지 클리어 아님)
        exp = 0;     //현재 exp(게임 오버, 게임 클리어 시 초기화 - 스테이지 클리어 아님)
        money = 0;   //현재 gold(메인화면, 스테이터스 강화 화면에서 사용하는 것으로 maxHp, atk, speed를 영구적으로 증가)
                     //증가할때 마다 필요한 money 증가
    }       

    public Player(PlayerData playerData)
    {
        maxHp = playerData.maxHp;
        atk = playerData.atk;
        speed = playerData.speed;
        level = playerData.level;
        exp = playerData.currentExp;
        money = playerData.money;
    }
}
