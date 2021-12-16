using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage2 : MonoBehaviour
{
    public float currHp = 100;

    // 충돌한 collider의 isTrigger 옵션이 체크 되었을 때 발생
    void OnTriggerEnter(Collider coll)
    {
        // 충돌한 collider의 태그가 BULLET이면 Player의 currHp를 차감 
        if (coll.tag == "BULLET")
        {
            Destroy(coll.gameObject);
            currHp -= 5.0f;

            Debug.Log("PlayerHP = " + currHp.ToString());

            // Player의 생명이 0 이하면 사망 처리
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    // Player의 사망 처리 루틴 
    void PlayerDie()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }
    }
}
