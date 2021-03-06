using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // 적 캐릭터의 상태를 표현하기 위한 열거형 변수 정의
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    // 상태를 저장할 변수
    public State state = State.PATROL;

    // 공격 사정거리
    public float attackDist = 5.0f;

    // 추적 사정거리
    public float traceDist = 10.0f;

    // 사망 여부를 판단할 변수
    public bool isDie = false;

    // 주인공의 위치를 저장할 변수
    Transform playerTr;

    // 적 캐릭터의 위치를 저장할 변수
    Transform enemyTr;

    // 코루틴에서 사용할 지연시간 변수
    WaitForSeconds ws;


    // Start보다 먼저 실행
    // 오브젝트가 disable인 상태에서도 실행 
    void Awake()
    {
        // 주인공 게임 오브젝트 추출 
        GameObject player = GameObject.FindGameObjectWithTag("PLAYER");

        // 주인공의 transform 컴포넌트 추출 
        if (player != null)
        {
            playerTr = player.transform;
        }

        // 적 캐릭터의 transform 컴포넌트 추출 
        enemyTr = GetComponent<Transform>();

        // 코루틴의 지연시간 생성 
        ws = new WaitForSeconds(0.3f);
    }

    void OnEnable()
    {
        // check state 코루틴 함수 실행
        StartCoroutine(CheckState());
    }

    IEnumerator CheckState()
    {
        while (isDie == false)
        {
            // 적 캐릭터가 사망하기 전까지 실행하는 무한 루프 
            if (state == State.DIE) yield break;

            // 주인공과 적 캐릭터 간의 거리를 계산 
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            if (dist <= attackDist) // 공격 사정거리 이내의 경우 
                state = State.ATTACK;
            else if (dist <= traceDist) // 추적 사정거리 이내의 경우 
                state = State.TRACE;
            else
                state = State.PATROL;

            // 0.3초 대기하는 동안 제어권을 양보 
            yield return ws;
        }
    }
}
