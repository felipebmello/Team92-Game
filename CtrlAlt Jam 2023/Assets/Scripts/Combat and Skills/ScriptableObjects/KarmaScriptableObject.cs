using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KarmaScriptableObject : ScriptableObject
{
    public enum KarmaState
    {
        Evil,           //0
        Corrupt,        //1
        Malicious,      //2
        TooInnocent,    //3
        Innocent,       //4
        Enlightned,     //5
        Virtuous,       //6
        Good,           //7
        Angel         //8
    }

    
    [SerializeField] private KarmaState state;

    [SerializeField] private Sprite newSprite;
    
    [Header("Shooting Settings")]
    [SerializeField] private Transform newBulletPrefab;

    public KarmaState State { get => state; set => state = value; }
    public Sprite NewSprite { get => newSprite; set => newSprite = value; }
    public Transform NewBulletPrefab { get => newBulletPrefab; set => newBulletPrefab = value; }
}
