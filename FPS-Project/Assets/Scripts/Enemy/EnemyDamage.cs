using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // 생명 게이지
    float hp = 100.0f;

    // 피격 시 사용할 혈혼 효과
    GameObject bloodEffect;

    void Start()
    {
        // 혈흔 효과 프리팹 로드
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            Destroy(coll.gameObject);

            //hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;

            BulletCtrl bc = coll.gameObject.GetComponent<BulletCtrl>();
            if (bc != null)
            {
                hp -= bc.damage;
                ShowBloodEffect(coll);
            }

            if (hp <= 0.0f)
            {
                GetComponent<EnemyAI7>().state = EnemyAI7.State.DIE;
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
}
