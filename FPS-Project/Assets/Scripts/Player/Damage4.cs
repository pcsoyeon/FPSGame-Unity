using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage4 : MonoBehaviour
{
    public float currHp = 100;

    public Image bloodScreen;
    public Image hpBar;

    Color initColor = new Vector4(0, 1.0f, 0.0f, 1.0f);
    Color currColor;

    // GameManager 저장 변수
    public GameManager2 gameMng;

    private void Start()
    {
        hpBar.color = initColor;
        currColor = initColor;
    }

    // 충돌한 collider의 isTrigger 옵션이 체크 되었을 때 발생
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "HEALTH")
        {
            currHp += 5.0f;
            DisplayHpbar();
        }

        // 충돌한 collider의 태그가 BULLET이면 Player의 currHp를 차감 
        if (coll.tag == "BULLET")
        {
            Destroy(coll.gameObject);
            currHp -= 5.0f;

            // Player의 생명이 0 이하면 사망 처리
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }

            // 혈흔 효과를 표현할 코루틴 함수 호출
            StartCoroutine(ShowBloodScreen());

            // 생명 게이지의 색상 및 변경 함수 호출
            DisplayHpbar();
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

        gameMng.isGameOver = true;
    }

    IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.5f, 1.0f));
        yield return new WaitForSeconds(0.1f);
        bloodScreen.color = Color.clear;
    }

    void DisplayHpbar()
    {
        float ratio = currHp / 100.0f;
        if (ratio > 0.5f)
            currColor.r = (1 - ratio) * 2.0f;
        else
            currColor.g = ratio * 2.0f;

        hpBar.color = currColor;
        hpBar.fillAmount = ratio;
    }
}
