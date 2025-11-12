using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{

    void OnCreate() { }
    void OnUpdate() { }

    void OnDrawGizmos()
    {

        float maxDistance = 10;
        RaycastHit hit;
        // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, maxDistance);

        Gizmos.color = Color.red;
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }
    }
}
