using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCtl : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform ballTransform;
    public float arrowMaxLength = 2f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // 라인의 점 개수를 2로 설정
    }
    private void Update()
    {
        Vector3 ballPosition = ballTransform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = ballPosition - mousePosition;
        direction.z = 0f;
        direction = Vector3.ClampMagnitude(direction, arrowMaxLength);

        Vector3 arrowEndPosition = ballPosition + direction;
        Vector3 arrowStartPosition = ballPosition;

        // 예상 이동 위치를 계산
        Vector3 expectedPosition = CalculateExpectedPosition(arrowEndPosition);

        // 라인의 시작점과 끝점 설정
        lineRenderer.SetPosition(0, arrowStartPosition);
        lineRenderer.SetPosition(1, expectedPosition);
    }

    private Vector3 CalculateExpectedPosition(Vector3 arrowEndPosition)
    {
        // 예상 이동 위치를 계산하는 로직을 구현해야 함
        // 예시로 현재는 드래그한 방향의 벡터를 0.5만큼 곱하여 이동 위치를 계산
        float distance = Vector3.Distance(ballTransform.position, arrowEndPosition);
        Vector3 normalizedDirection = (arrowEndPosition - ballTransform.position).normalized;
        Vector3 expectedPosition = ballTransform.position + normalizedDirection * (distance * 0.5f);

        return expectedPosition;
    }
}
