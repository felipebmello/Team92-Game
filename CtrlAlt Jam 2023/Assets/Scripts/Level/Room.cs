using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    
    [Header("Room Exit SFX Settings")]
    [SerializeField] protected AudioClip roomExitSFX;
    [SerializeField] float roomExitSFXVolume = 0.75f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private PolygonCollider2D myRoomBounds;

    public event EventHandler<PolygonCollider2D> OnPlayerEnteringRoom;

    private void Awake() {
        myRoomBounds = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag.Equals("Player") && !other.TryGetComponent<Bullet>(out Bullet bullet))
        {

            CameraController.Instance.SetActiveCinemachineCamera(cinemachineVirtualCamera, other.transform);
            //CameraController.Instance.SetRoomBounds(myRoomBounds);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag.Equals("Player") && !other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            AudioSource.PlayClipAtPoint(roomExitSFX, AudioManager.Instance.GetAudioListener().transform.position, roomExitSFXVolume);
            //CameraController.Instance.SetRoomBounds(myRoomBounds);
        }
        
    }
}
