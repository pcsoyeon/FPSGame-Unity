using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    // Canvas 를 렌더링 하는 카메라
    Camera uiCamera;

    // UI용 최상위 캔버스
    Canvas canvas;

    // 부모 RecTransform 컴포넌트
    RectTransform recParent;
    // 자신 RectTransform 컴포넌트
    RectTransform rectHp;

    // HpBar 이미지의 위치를 조절할 오프셋
    public Vector3 offset = Vector3.zero;

    // 추적할 대상의 Transform 컴포넌트
    public Transform targetTr;
    
    void Start()
    {
        // 컴포넌트 추출 및 할당
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;

        recParent = canvas.GetComponent<RectTransform>();
        rectHp = gameObject.GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        // 월드 좌표계를 스크린의 좌표로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);

        // 카메라의 뒷쪽 영역(180도 회전)일 때 좌표값 보정
        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }

        // RectTransform 좌표값을 전달받을 변수
        Vector2 localPos = Vector2.zero;

        // 스크린 좌표를 RectTransform 기준의 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            recParent, screenPos, uiCamera, out localPos
            );
        rectHp.localPosition = localPos;
    }

    void Update()
    {
        
    }
}
