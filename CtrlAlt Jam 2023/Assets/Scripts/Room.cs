using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    private PolygonCollider2D myRoomBounds;

    public event EventHandler<PolygonCollider2D> OnPlayerEnteringRoom;

    private void Awake() {
        myRoomBounds = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag.Equals("Player") && !other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            CameraController.Instance.SetRoomBounds(myRoomBounds);
        }
        
    }
}
