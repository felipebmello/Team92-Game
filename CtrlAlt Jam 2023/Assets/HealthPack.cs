using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GetComponent<GameObject>();
    }

    protected void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag.Equals("Player"))
        {
            Debug.Log(player.name);
            player.GetComponent<HealthSystem>().health += 25;
            Destroy(gameObject);
        }
    }
}
