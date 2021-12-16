using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI5 : MonoBehaviour
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

    // 이동을 제어하는 MoveAgent 클래스를 저장할 변수
    MoveAgent4 moveAgent;

    // Animator 컴포넌트를 저장할 변수
    Animator animator;

    // 총 발사 제어 
    EnemyFire enemyFire;

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

        // 이동을 제어하는 MoveAgent 클래스를 추출
        moveAgent = GetComponent<MoveAgent4>();

        // Amimator 컴포넌트 추출
        animator = GetComponent<Animator>();

        // 총 발사 제어 클래스 추출
        enemyFire = GetComponent<EnemyFire>();

        // 코루틴의 지연시간 생성 
        ws = new WaitForSeconds(0.3f);
    }

    void OnEnable()
    {
        // check state 코루틴 함수 실행
        StartCoroutine(CheckState());

        // Action 코루틴 함수 실행
        StartCoroutine(Action());
    }

    IEnumerator CheckState()
    {
        while (!isDie)
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

    // 상태에 따라 적 캐릭터의 행동을 처리하는 코루틴 함수 
    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state) // 상태에 따라 분기 처리 
            {
                case State.PATROL:
                    enemyFire.isFire = false;
                    // 순찰 모드 활성화
                    moveAgent.SetPatrolling(true);
                    animator.SetBool("IsMove", true);
                    break;

                case State.TRACE:
                    enemyFire.isFire = false;
                    // 주인공의 위치를 넘겨 추적 모드로 변경
                    moveAgent.SetTraceTarget(playerTr.position);
                    animator.SetBool("IsMove", true);
                    break;

                case State.ATTACK:
                    moveAgent.Stop(); // 순찰 및 추적을 정지 
                    animator.SetBool("IsMove", false);
                    if (enemyFire.isFire == false)
                    {
                        enemyFire.isFire = true;
                    }
                    break;

                case State.DIE:
                    isDie = true;
                    enemyFire.isFire = false;
                    moveAgent.Stop();
                    animator.SetTrigger("Die");
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;
            }
        }
    }

    void Update()
    {
        // Speed 파라미터에 이동 속도를 전달
        animator.SetFloat("Speed", moveAgent.GetSpeed());
    }

}
