using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Room room;
    [SerializeField] private BoxCollider2D fullWallCollider;
    private Animator myAnimator;

    private void Start() 
    {
        myAnimator = GetComponent<Animator>();
        room.OnRoomEnter += Room_OnRoomEnter;
        room.OnAllEnemiesDead += Room_OnAllEnemiesDead;
    }

    private void OnDisable() 
    {
        room.OnRoomEnter -= Room_OnRoomEnter;
        room.OnAllEnemiesDead -= Room_OnAllEnemiesDead;
    }

    private void Room_OnRoomEnter(object sender, EventArgs e)
    {
        myAnimator.SetTrigger("CloseDoor");
        fullWallCollider.enabled = true;
    }
    private void Room_OnAllEnemiesDead(object sender, EventArgs e)
    {
        myAnimator.SetTrigger("OpenDoor");
        fullWallCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            if (other.tag.Equals("Player"))
            {
                other.gameObject.GetComponent<PlayerMovement>().ForceMoveOutOfRoom();
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (!other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            if (other.tag.Equals("Player"))
            {
                other.gameObject.GetComponent<PlayerMovement>().ReturnControl();
            }
        }
    }

}
