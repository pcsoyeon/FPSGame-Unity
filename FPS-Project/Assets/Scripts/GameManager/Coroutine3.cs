using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine3 : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PrintOddNumber());
        StartCoroutine(PrintEvenNumber());
    }

    IEnumerator PrintOddNumber()
    {
        for (int i = 1; i < 10000; i += 2)
        {
            // 메인으로 제어권 넘기고 0.5초마다 대기
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator PrintEvenNumber()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 2; i < 10000; i += 2)
        {
            // 메인으로 제어권 넘기고 0.5초마다 대기
            yield return new WaitForSeconds(1.0f);
        }
    }
}
