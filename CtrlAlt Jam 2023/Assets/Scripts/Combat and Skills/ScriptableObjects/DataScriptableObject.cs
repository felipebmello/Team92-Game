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


    private KarmaScriptableObject.KarmaState lastState;
    private KarmaScriptableObject lastKarmaScrObj;
    private SkillScriptableObject lastSkill;
    private List<KarmaScriptableObject.KarmaState> lastAllStates = new List<KarmaScriptableObject.KarmaState>();
    private List<SkillScriptableObject> lastAllSkills = new List<SkillScriptableObject>();
    private float lastMovementSpeed;
    private Transform lastBulletPrefab;
    private bool lastHoldToShoot;
    private float lastFireRate;


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
        lastState = baseCurrentState;
        lastKarmaScrObj = baseCurrentKarmaScrObj;
        lastSkill = baseCurrentSkill;
        lastMovementSpeed = baseMovementSpeed;
        lastBulletPrefab = baseBulletPrefab;
        lastHoldToShoot = baseHoldToShoot;
        lastFireRate = baseFireRate;
        lastAllStates = new List<KarmaScriptableObject.KarmaState>();
        lastAllSkills = new List<SkillScriptableObject>();
        ResetData();
    }

    public void ResetData()
    {
        currentState = lastState;
        currentKarmaScrObj = lastKarmaScrObj;
        currentSkill = lastSkill;
        numberOfSaves = baseNumberOfSaves;
        MovementSpeed = lastMovementSpeed;
        BulletPrefab = lastBulletPrefab;
        HoldToShoot = lastHoldToShoot;
        FireRate = lastFireRate;
        allStates = lastAllStates;
        allSkills = lastAllSkills;
    }

    public void SetLastData()
    {
        lastState = currentState;
        lastKarmaScrObj = currentKarmaScrObj;
        lastSkill = currentSkill;
        lastMovementSpeed = MovementSpeed;
        lastBulletPrefab = BulletPrefab;
        lastHoldToShoot = HoldToShoot;
        lastFireRate = FireRate;
        lastAllStates = allStates;
        lastAllSkills = allSkills;
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
