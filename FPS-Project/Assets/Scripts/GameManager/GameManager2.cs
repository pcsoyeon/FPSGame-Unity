using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    // 적 캐릭터가 출현할 위치를 담은 배열 
    public Transform[] points;
    // 적 캐릭커 프리팹을 저장할 변수 
    public GameObject enemy;
    // 적 캐릭터를 생성할 주기 
    public float createTime = 2.0f;
    // 적 캐릭터의 최대 생성 갯수
    public int maxEnemy = 10;
    // 게임 종료 여부를 판단할 변수 
    public bool isGameOver = false;

    // kill count
    public Text killText;
    float killCount = 0;

    // 재시작 버튼
    public GameObject restartBtn;

    void Start()
    {
        // 하이러키 뷰의 SpawnPointGroup을 찾아 하위에 있는 모든 Transform 컴포넌트를 찾아옴
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if (points.Length > 0)
        {
            StartCoroutine(this.CreateEnemy());
        }
    }

    // 적 캐릭터를 생성하는 코루틴 함수 
    IEnumerator CreateEnemy()
    {
        // 게임 종료 시까지 무한 루프 
        while(isGameOver == false)
        {
            // 현재 생성된 적 캐릭터의 갯수 산출 
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("ENEMY").Length;
            // 적 캐릭터의 최대 생성 개수보다 적을 때만 적 캐릭터를 생성 
            if (enemyCount < maxEnemy)
            {
                // 적 캐릭터의 생성 주기 시간만큼 대기 
                yield return new WaitForSeconds(createTime);

                // 불규칙적인 위치 산출 
                int idx = Random.Range(1, points.Length);
                // 적 캐릭터의 동적 생성 
                Instantiate(enemy, points[idx].position, points[idx].rotation);
            } else
            {
                yield return null;
            }
        }

        // 재시작 버튼 활성화
        restartBtn.SetActive(true);
    }

    public void AddKillCount()
    {
        ++killCount;
        killText.text = "Kill Count" + killCount;
    }

    public void Restart()
    {
        // 씬 다시 로드 
        SceneManager.LoadScene("SampleScene");

        // 재시작 버튼 비활성화
        restartBtn.SetActive(false);
        isGameOver = false;
    }
}
