using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bullet; // 발사체 공용 변수 
    public AudioClip sound; // 사운드 공용 변수 

    Vector3 beginPos; // 마우스 위치 저장할 변수 
    AudioSource ads; // 오디오 저장 변수 

    void Start()
    {
        ads = GetComponent<AudioSource>(); // Audio Source 갖고 오기 
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 좌측 마우스 버튼을 누른다면 
        {
            beginPos = Input.mousePosition; // 현재의 마우스 위치 저장 
        }
        else if (Input.GetMouseButtonUp(0)) // 만약 마우스 버튼을 떼는 순간에는 
        {
            ads.PlayOneShot(sound); // 사운드 출력 

            Vector3 delta = Input.mousePosition - beginPos; // 마우스 위치 값 측정 (얼마나 이동했는지)
            Vector3 force = new Vector3(delta.x, delta.y, Mathf.Abs(delta.y)); // 물리 객체를 밀어줄 힘 측정 (움직인 만큼) -> 절대값 사용해서 앞으로만(양수값) 이동 

            GameObject obj = Instantiate(bullet);
            obj.GetComponent<Rigidbody>().AddForce(force * 5);
        }
    }
}
