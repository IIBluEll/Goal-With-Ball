using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCtl : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform ballTransform;
    public float arrowMaxLength = 2f;
    public float arrowHeadSize = 0.2f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4; // 라인의 점 개수를 4로 설정
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
        
        DrawArrowHead(arrowStartPosition, expectedPosition);
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
    
    private void DrawArrowHead(Vector3 startPoint, Vector3 endPoint)
    {
        // 화살표 머리의 길이 계산
        float arrowHeadLength = arrowHeadSize * Vector3.Distance(startPoint, endPoint);

        // 라인 렌더러의 마지막 두 점 설정 (화살표 머리 부분)
        Vector3 arrowDirection = (endPoint - startPoint).normalized;
        Vector3 arrowHeadStartPoint = endPoint - arrowDirection * arrowHeadLength;
        Vector3 arrowHeadOrthogonal = new Vector3(-arrowDirection.y, arrowDirection.x, 0f) * arrowHeadSize * 0.5f;
        Vector3 arrowHeadEndPoint1 = arrowHeadStartPoint + arrowHeadOrthogonal;
        Vector3 arrowHeadEndPoint2 = arrowHeadStartPoint - arrowHeadOrthogonal;

        lineRenderer.SetPosition(2, arrowHeadEndPoint1);
        lineRenderer.SetPosition(3, arrowHeadEndPoint2);
    }
}
