using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    // 총알 프리팹
    public GameObject bullet;

    // 탄피 추출 파티클 
    public ParticleSystem catridge;

    // 총알 발사 좌표
    public Transform firePos;

    // 오디오 클립을 저장할 변수
    public AudioClip fireSound;

    // 총구 화염 파티클
    ParticleSystem muzzleFlash;

    // AudioSource 컴포넌트를 저장할 변수
    AudioSource _audio; // _(언더바 사용할 것)

    void Start()
    {
        // FirePos 하위에 있는 컴포넌트 추출
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();

        // AudioSource 컴포넌트 추출
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 마우스 왼쪽 버튼을 눌렀을 때 Fire 함수 호출 
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Bullet prefab을 동적으로 생성 
        Instantiate(bullet, firePos.position, firePos.rotation);

        // 파티클 실행
        catridge.Play();
        // 총구 화염 파티클 실행
        muzzleFlash.Play();
        // 사운드 실행
        _audio.PlayOneShot(fireSound, 1.0f);
    }
}
