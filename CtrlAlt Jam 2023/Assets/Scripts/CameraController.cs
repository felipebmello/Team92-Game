using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//Script para que a camera acompanhe a posição do Mouse. 
public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private Vector2 offsetThreshold;
    [SerializeField] private Vector2 offsetRadius;
    [SerializeField] private float smoothTime;
    void FixedUpdate()
    {
        Vector3 playerPosition = cinemachineVirtualCamera.Follow.transform.position;
        Vector3 targetPosition = MouseWorld.GetPosition();
        //Se o mouse estiver dentro dos limites, não é aplicado o offset na camera
        if (Mathf.Abs(playerPosition.x - targetPosition.x) < offsetRadius.x)
        {
            targetPosition.x = playerPosition.x;
        }
        else 
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, playerPosition.x - offsetThreshold.x, playerPosition.x + offsetThreshold.x);
        }
        if (Mathf.Abs(playerPosition.y - targetPosition.y) < offsetRadius.y)
        {
            targetPosition.y = playerPosition.y;
        }
        else 
        {
            targetPosition.y = Mathf.Clamp(targetPosition.y, playerPosition.y - offsetThreshold.y, playerPosition.y + offsetThreshold.y);
        }
        //Debug.Log("Player: "+playerPosition + " - Mouse: "+MouseWorld.GetPosition()+" - Target Clamped: "+targetPosition);
        //Interpola entre sua posição e o offset
        targetPosition.z = 0f;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = Vector3.Lerp(
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset,
            targetPosition - playerPosition,
            Time.fixedDeltaTime * smoothTime);
        
    }
}
