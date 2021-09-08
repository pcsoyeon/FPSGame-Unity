using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true; // 정지
        GetComponentInChildren<ParticleSystem>().Play(); // effect play

        Destroy(gameObject, 2); // 2초 후 제거 
    }
}
