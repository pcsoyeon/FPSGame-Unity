using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public bool isFire = false;
    public AudioClip fireSfx;
    public GameObject Bullet;
    public Transform firePos;

    AudioSource _audio;
    Animator animator;
    Transform playerTr;
    Transform enemyTr;
    ParticleSystem muzzleFlash;

    // 다음 발사할 시간 계산하기 위한 변수 
    float nextFire = 0.0f;
    // 총알 발사 간격 
    float fireRate = 0.5f;
    // 주인공을 향해 회전할 속도 변수 
    float damping = 10.0f;

    void Start()
    {
        // 컴포넌트 추출 및 변수 저장
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").transform;
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
    }
    
    void Update()
    {
        if (isFire)
        {
            // 현재 시간이 다음 발사시간보다 큰지 계산 
            if (Time.time >= nextFire)
            {
                Fire();
                // 다음 발사 시간 계산
                nextFire = Time.time + fireRate + Random.Range(0.0f, 0.5f);
            }

            // 주인공이 있는 위치까지의 회전 각도 계산
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            // 보간 함수를 이용해서 점진적으로 회전
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
    }

    void Fire()
    {
        animator.SetTrigger("Fire");
        _audio.PlayOneShot(fireSfx, 0.5f);
        muzzleFlash.Play();

        // 총알 생성
        GameObject _bullet = Instantiate(Bullet, firePos.position, firePos.rotation);
        // 일정 시간이 지난 후 삭제
        Destroy(_bullet, 3.0f);
    }
}
