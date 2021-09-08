using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMng : MonoBehaviour
{
    public Text scoreText;

    int score = 0;

    private void OnCollisionEnter(Collision coll)
    {
        score += 10; // 충돌이 발생하면 10 추가
        scoreText.text = "점수" + score; // UI에 반영 
    }
}
