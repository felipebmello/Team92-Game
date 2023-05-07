using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton para guardar a posição do Mouse
public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {

        this.transform.position = MouseWorld.GetPosition();
    }

    public static Vector3 GetPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePosition.x, mousePosition.y, -2f);
    }

}
