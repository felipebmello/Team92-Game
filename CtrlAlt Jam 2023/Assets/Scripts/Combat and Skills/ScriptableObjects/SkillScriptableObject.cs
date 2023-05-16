using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillScriptableObject : ScriptableObject
{
    public enum SkillState
    {
        BadKarma,           //0
        Neutral,        //1
        GoodKarma,      //2
    }
    [SerializeField] private SkillState state;
    [SerializeField] private Sprite imageSprite;
    [Header("Skill Settings")]
    [SerializeField] private new string name;
    [SerializeField] private string effect;
    [TextArea(14,10)] [SerializeField] private string description;
    [Header("Movement Settings")]
    [Range(6f, 18f)]
    [SerializeField] private float newMovementSpeed;
    [Header("Shooting Settings")]
    [SerializeField] private bool newHoldToShoot;
    [SerializeField] private bool backShot;
    [SerializeField] private bool tripleShot;
    [SerializeField] private bool healingShot;
    [Range(0.1f, 1f)]
    [SerializeField] private float newFireRate;
    [Header("Modifier Settings")]
    [SerializeField] private float healthModifier = 1f;

    public SkillState State { get => state; set => state = value; }
    public Sprite ImageSprite { get => imageSprite; set => imageSprite = value; }
    public string Name { get => name; set => name = value; }
    public string Effect { get => effect; set => effect = value; }
    public string Description { get => description; set => description = value; }
    public float NewMovementSpeed { get => newMovementSpeed; set => newMovementSpeed = value; }
    public bool NewHoldToShoot { get => newHoldToShoot; set => newHoldToShoot = value; }
    public float NewFireRate { get => newFireRate; set => newFireRate = value; }
    public float HealthModifier { get => healthModifier; set => healthModifier = value; }
    public bool BackShot { get => backShot; set => backShot = value; }
    public bool TripleShot { get => tripleShot; set => tripleShot = value; }
    public bool HealingShot { get => healingShot; set => healingShot = value; }
}
