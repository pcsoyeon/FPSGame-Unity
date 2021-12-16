using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage3 : MonoBehaviour
{
    // 생명 게이지
    float hp = 100.0f;

    // 피격 시 사용할 혈혼 효과
    GameObject bloodEffect;

    // 생명 게이지 프리팹을 저장할 변수
    public GameObject hpBarPrefab;
    // 생명 게이지의 위치를 보정할 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    // 부모가 될 Canvas 객체
    private Canvas uiCanvas;
    // 생명 수치에 따라 fillAmount 속성을 변경할 Image
    private Image hpBarImage;

    void Start()
    {
        // 혈흔 효과 프리팹 로드
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");

        // 생명 게이지의 생성 및 초기화
        SetHpBar();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            ShowBloodEffect(coll);
            Destroy(coll.gameObject);
            BulletCtrl bc = coll.gameObject.GetComponent<BulletCtrl>();

            if (bc != null)
            {
                hp -= bc.damage;

                // 생명 게이지의 fillAmount 속성을 변경
                hpBarImage.fillAmount = hp / 100.0f;
            }

            if (hp <= 0.0f)
            {
                // 적 캐릭터의 상태를 DIE로 변경 
                GetComponent<EnemyAI7>().state = EnemyAI7.State.DIE;

                // 적 캐릭터가 사망한 이후 생명 게이지를 투명 처리
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;

                // 적 캐릭터 사망시 제거
                Destroy(gameObject, 5);
                // kill count 횟수 증가
                GameObject.Find("GameManager").GetComponent<GameManager2>().AddKillCount();
            }
        }
    }

    // 혈흔 효과 생성 
    void ShowBloodEffect(Collision coll)
    {
        Vector3 pos = coll.contacts[0].point;
        Vector3 _normal = coll.contacts[0].normal;
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
        GameObject blood = Instantiate(bloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        // UI Canvas 하위로 생명 게이지를 생성
        GameObject hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);

        // fillAmount 속성을 변경할 Image 추출
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        // 생명 게이지가 따라가야 할 대상과 오프셋 값 설정
        EnemyHpBar bar = hpBar.GetComponent<EnemyHpBar>();

        bar.targetTr = gameObject.transform;
        bar.offset = hpBarOffset;
    }
}
