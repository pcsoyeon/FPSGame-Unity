using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    // 폭발 효과 프리팹을 저장할 변수
    public GameObject expEffect;
    // 총알이 맞은 횟수
    private int hitCount = 0;
    // Rigidbody 컴포넌트를 저장할 변수
    private Rigidbody rb;

    void Start()
    {
        // Rigidbody 컴포넌트를 추출해 저장
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        // 폭발 효과 프리팹 동적 생성
        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);

        // Rigidbody 컴포넌트의 mass를 1.0으로 수정해서 무게를 가볍게 함
        rb.mass = 1.0f;
        // 위로 솟구치는 힘을 가함
        rb.AddForce(Vector3.up * 1000.0f);
    }
}
