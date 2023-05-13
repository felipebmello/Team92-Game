using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    
    [Header("Room SFX Settings")]
    [SerializeField] protected AudioClip roomExitSFX;
    [SerializeField] protected AudioClip doorsOpeningClosingSFX;
    [SerializeField] float roomSFXVolume = 0.2f;
    [SerializeField] private CinemachineVirtualCamera zoomInVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera zoomOutVirtualCamera;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private Transform collectableContainer;
    [Header("Only for Debugging in the Editor")]
    [SerializeField] private List<Transform> enemyList;
    [SerializeField] private Transform levelExit = null;
    [SerializeField] private bool hasSkillHolder;
    public event EventHandler OnRoomEnter;
    public event EventHandler OnAllEnemiesDead;
    [SerializeField] private bool playerLeftRoom = true;
    [SerializeField] private bool doorsClosed = false;

    private void Start() 
    {
        PopulateEnemyList();
        CheckForSkillHolder();
        CheckForLevelExit();
    }

    private void PopulateEnemyList()
    {
        enemyList = new List<Transform>();
        foreach (EnemyController enemyController in enemyContainer.GetComponentsInChildren<EnemyController>())
        {
            enemyList.Add(enemyController.transform);
        }
    }

    
    private void CheckForSkillHolder()
    {
        foreach (SkillHolder skillHolder in collectableContainer.GetComponentsInChildren<SkillHolder>())
        {
            hasSkillHolder = true;
        }
    }

    private void CheckForLevelExit()
    {
        foreach (LevelExit l in collectableContainer.GetComponentsInChildren<LevelExit>())
        {
            levelExit = l.transform;
            levelExit.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag.Equals("Player") && !other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            playerLeftRoom = false;
            CameraController.Instance.SetActiveCinemachineCamera(zoomOutVirtualCamera, other.transform);
            if (!doorsClosed) StartCoroutine(TryCloseAllDoors(other));
            //CameraController.Instance.SetRoomBounds(myRoomBounds);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (!other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            if (other.tag.Equals("Player"))
            {
                playerLeftRoom = true;
                AudioSource.PlayClipAtPoint(roomExitSFX, AudioManager.Instance.GetAudioListener().transform.position, roomSFXVolume);
                return;
            }
        }
        if (enemyList.Count != 0)
        {
            enemyList.Remove(other.gameObject.transform);
            StartCoroutine(CheckEnemyList());
            enemyList.RemoveAll(enemy => enemy == null);
        }
    }

    
    public IEnumerator TryCloseAllDoors(Collider2D other)
    {
        yield return new WaitForSeconds(0.5f);
        if (!playerLeftRoom && (enemyList.Count > 0))
        {
            OnRoomEnter?.Invoke(this, EventArgs.Empty);
            AudioSource.PlayClipAtPoint(doorsOpeningClosingSFX, AudioManager.Instance.GetAudioListener().transform.position, roomSFXVolume);
            CameraController.Instance.SetActiveCinemachineCamera(zoomInVirtualCamera, other.transform);
            doorsClosed = true;
        }
    }

    public IEnumerator OpenAllDoors()
    {        
        CameraController.Instance.ZoomOut(zoomOutVirtualCamera);
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(doorsOpeningClosingSFX, AudioManager.Instance.GetAudioListener().transform.position, roomSFXVolume);
        doorsClosed = false;
        OnAllEnemiesDead?.Invoke(this, EventArgs.Empty);
        if (levelExit != null) levelExit.gameObject.SetActive(true);
    }
    
    public IEnumerator CheckEnemyList()
    {
        yield return new WaitForSeconds(1.5f);
        enemyList.RemoveAll(enemy => enemy == null);
        if (enemyList.Count == 0 && doorsClosed) StartCoroutine(OpenAllDoors());
    }
}
