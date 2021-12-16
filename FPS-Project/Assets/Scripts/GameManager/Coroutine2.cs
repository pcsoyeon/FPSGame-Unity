using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine2 : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PrintOddNumber());
    }

    IEnumerator PrintOddNumber()
    {
        for (int i = 1; i < 10000; i += 2)
        {
            print(i);

            // 메인으로 제어권 넘기고 0.5초마다 대기
            yield return new WaitForSeconds(0.5f);
        }
    }
}
