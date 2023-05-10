using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : ScriptableObject
{
    [Header("Skill Settings")]
    [SerializeField] private new string name;
    [SerializeField] private string effect;
    [TextArea(14,10)] [SerializeField] private string description;
    [SerializeField] private PlayerController.SkillState state;
    [Header("Movement Settings")]
    [SerializeField] private Sprite newSprite;
    [Range(6f, 18f)]
    [SerializeField] private float newMovementSpeed;
    [Header("Shooting Settings")]
    [SerializeField] private Transform newBulletPrefab;
    [SerializeField] private bool newHoldToShoot;
    [Range(0.1f, 1f)]
    [SerializeField] private float newFireRate;
    [Header("Modifier Settings")]
    [SerializeField] private float healthModifier = 1f;

    public string Name { get => name; set => name = value; }
    public PlayerController.SkillState State { get => state; set => state = value; }
    public Sprite NewSprite { get => newSprite; set => newSprite = value; }
    public float NewMovementSpeed { get => newMovementSpeed; set => newMovementSpeed = value; }
    public Transform NewBulletPrefab { get => newBulletPrefab; set => newBulletPrefab = value; }
    public bool NewHoldToShoot { get => newHoldToShoot; set => newHoldToShoot = value; }
    public float NewFireRate { get => newFireRate; set => newFireRate = value; }
    public string Description { get => description; set => description = value; }
    public string Effect { get => effect; set => effect = value; }
    public float HealthModifier { get => healthModifier; set => healthModifier = value; }

    public virtual void Use() 
    {

    }
}
