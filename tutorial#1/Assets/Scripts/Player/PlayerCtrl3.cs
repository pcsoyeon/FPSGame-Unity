using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// default motion 출력 
public class PlayerCtrl3 : MonoBehaviour
{
    // 이동 속도 변수
    public float moveSpeed = 10.0f;

    // 회전 속도 변수
    public float rotSpeed = 80.0f;

    // Animation 컴포넌트를 저장하기 위한 변수
    Animation anim;

    void Start()
    {
        // Animation 컴포넌트를 변수에 할당
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        // 전후,좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        // Translate(이동방향 * 속도 * 변위값 * Time.deltaTime, 기준 좌표)
        // normalized -> moveDir을 길이를 1로 설정
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // Vector3.up축을 기준으로 rotSpeed만큼의 속도로 회전
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.3f);
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.3f);
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.3f);
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunL", 0.3f);
        } else
        {
            anim.CrossFade("Idle", 0.3f);
        }
    }
}
