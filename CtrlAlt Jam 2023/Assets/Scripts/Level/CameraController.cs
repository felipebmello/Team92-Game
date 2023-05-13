using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


//Script para que a camera acompanhe a posição do Mouse. 
public class CameraController : MonoBehaviour
{
    public static CameraController Instance {get; private set; }
    public enum CameraControllerType
    {
        FollowTargetObject,
        FollowPlayerOffset
    }
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private CinemachineConfiner2D cinemachineConfiner2D;
    [SerializeField] private CameraControllerType cameraType = CameraControllerType.FollowPlayerOffset;
    [SerializeField] private Vector2 offsetThreshold;
    [SerializeField] private Vector2 offsetRadius;
    [SerializeField] private float smoothTime;
    [SerializeField] private float threshold;
    private Transform playerTransform;
    
    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one CameraController on the Level! "+ transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
    }

    void FixedUpdate()
    {
        if (cinemachineVirtualCamera != null)
        {
            switch (cameraType)
            {
                case CameraControllerType.FollowPlayerOffset:
                    cinemachineVirtualCamera.Follow = playerTransform;
                    FollowPlayerOffset();
                    return;
                case CameraControllerType.FollowTargetObject:
                    cinemachineVirtualCamera.Follow = this.transform;
                    FollowTargetObject();
                    return;
                default:
                    break;
            }
        }
    }

    private void FollowPlayerOffset()
    {
        Vector3 playerPosition = playerTransform.position;
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

    private void FollowTargetObject()
    {
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = Vector3.zero;

        Vector3 playerPosition = playerTransform.position;
        Vector3 mousePosition = MouseWorld.GetPosition();
        Vector3 targetPosition = (playerPosition + mousePosition) / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, playerPosition.x - threshold, playerPosition.x + threshold);
        targetPosition.y = Mathf.Clamp(targetPosition.y, playerPosition.y - threshold, playerPosition.y + threshold);

        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.fixedDeltaTime * smoothTime);

    }

    public void SetActiveCinemachineCamera (CinemachineVirtualCamera newCinemachineVirtualCamera, Transform transform)
    {
        if (cinemachineVirtualCamera != null)
        {
            cinemachineVirtualCamera.gameObject.SetActive(false);
        }
        this.cinemachineVirtualCamera = newCinemachineVirtualCamera;
        this.playerTransform = transform;
        cinemachineVirtualCamera.Follow = playerTransform;
        cinemachineVirtualCamera.LookAt = playerTransform;
        cinemachineVirtualCamera.gameObject.SetActive(true);
    }

    public void ClearPlayerOffset()
    {
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = Vector3.zero;
    }

    public void ZoomOut(CinemachineVirtualCamera zoomOutVirtualCamera)
    {
        SetActiveCinemachineCamera(zoomOutVirtualCamera, playerTransform);
    }

}
