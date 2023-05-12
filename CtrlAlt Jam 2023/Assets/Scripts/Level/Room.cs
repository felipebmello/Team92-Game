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
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private Transform collectableContainer;
    [Header("Only for Debugging in the Editor")]
    [SerializeField] private List<Transform> enemyList;
    [SerializeField] private Transform levelExit = null;
    [SerializeField] private bool hasSkillHolder;
    private PolygonCollider2D myRoomBounds;
    public event EventHandler OnRoomEnter;
    public event EventHandler OnAllEnemiesDead;
    private bool playerLeftRoom = true;

    private void Awake() 
    {
        myRoomBounds = GetComponent<PolygonCollider2D>();
    }

    private void Start() 
    {
        PopulateEnemyList();
        CheckForSkillHolder();
        CheckForLevelExit();
    }

    private void PopulateEnemyList()
    {
        foreach (EnemyController enemyController in enemyContainer.GetComponentsInChildren<EnemyController>())
        {
            enemyList.Add(enemyController.transform);
            enemyController.GetHealthSystem().OnDead += EnemyController_GetHealthSystem_OnDead;
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
            CameraController.Instance.SetActiveCinemachineCamera(cinemachineVirtualCamera, other.transform);
            StartCoroutine(TryCloseAllDoors());
            //CameraController.Instance.SetRoomBounds(myRoomBounds);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) 
    {

        if (other.tag.Equals("Player") && !other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            playerLeftRoom = true;
            AudioSource.PlayClipAtPoint(roomExitSFX, AudioManager.Instance.GetAudioListener().transform.position, roomExitSFXVolume);
            //CameraController.Instance.SetRoomBounds(myRoomBounds);
        }
        
        enemyList.RemoveAll(enemy => enemy == null);
    }

    private void EnemyController_GetHealthSystem_OnDead (object sender, EventArgs e)
    {
        HealthSystem health = sender as HealthSystem;
        enemyList.Remove(health.transform);
        Debug.Log(gameObject+" aware of death of "+health);
        enemyList.RemoveAll(enemy => enemy == null);
        if (enemyList.Count == 0) StartCoroutine(OpenAllDoors());
    }

    
    public IEnumerator TryCloseAllDoors()
    {
        
        yield return new WaitForSeconds(1f);
        if (!playerLeftRoom && (enemyList.Count > 0))
        {
            OnRoomEnter?.Invoke(this, EventArgs.Empty);
        }
    }

    public IEnumerator OpenAllDoors()
    {
        yield return new WaitForSeconds(2f);
        OnAllEnemiesDead?.Invoke(this, EventArgs.Empty);
        if (levelExit != null) levelExit.gameObject.SetActive(true);
    }
}
