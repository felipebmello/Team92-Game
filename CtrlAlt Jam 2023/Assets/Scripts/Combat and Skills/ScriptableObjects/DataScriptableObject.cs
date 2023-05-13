using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu]
public class DataScriptableObject : ScriptableObject
{
    [SerializeField] private KarmaScriptableObject.KarmaState baseCurrentState;
    [SerializeField] private KarmaScriptableObject baseCurrentKarmaScrObj;
    [SerializeField] private SkillScriptableObject baseCurrentSkill;
    [SerializeField] private List<KarmaScriptableObject.KarmaState> baseAllStates = new List<KarmaScriptableObject.KarmaState>();
    [SerializeField] private List<SkillScriptableObject> baseAllSkills = new List<SkillScriptableObject>();
    [SerializeField] private int baseNumberOfSaves = 0;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private Transform baseBulletPrefab;
    [SerializeField] private bool baseHoldToShoot;
    [SerializeField] private float baseFireRate;

    [SerializeField] private KarmaScriptableObject.KarmaState currentState;
    [SerializeField] private KarmaScriptableObject currentKarmaScrObj;
    [SerializeField] private SkillScriptableObject currentSkill;
    [SerializeField] private List<KarmaScriptableObject.KarmaState> allStates = new List<KarmaScriptableObject.KarmaState>();
    [SerializeField] private List<SkillScriptableObject> allSkills = new List<SkillScriptableObject>();
    [SerializeField] public int numberOfSaves;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private bool holdToShoot;
    [SerializeField] private float fireRate;

    private void OnEnable() 
    {
        currentState = baseCurrentState;
        currentKarmaScrObj = baseCurrentKarmaScrObj;
        currentSkill = baseCurrentSkill;
        numberOfSaves = baseNumberOfSaves;
        MovementSpeed = baseMovementSpeed;
        BulletPrefab = baseBulletPrefab;
        HoldToShoot = baseHoldToShoot;
        FireRate = baseFireRate;
        allStates = new List<KarmaScriptableObject.KarmaState>();
        allSkills = new List<SkillScriptableObject>();
    }


    public KarmaScriptableObject.KarmaState CurrentState { get => currentState; set => currentState = value; }
    public KarmaScriptableObject CurrentKarmaScrObj { get => currentKarmaScrObj; set => currentKarmaScrObj = value; }
    public SkillScriptableObject CurrentSkill { get => currentSkill; set => currentSkill = value; }
    public List<KarmaScriptableObject.KarmaState> AllStates { get => allStates; set => allStates = value; }
    public List<SkillScriptableObject> AllSkills { get => allSkills; set => allSkills = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public Transform BulletPrefab { get => bulletPrefab; set => bulletPrefab = value; }
    public bool HoldToShoot { get => holdToShoot; set => holdToShoot = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
}
