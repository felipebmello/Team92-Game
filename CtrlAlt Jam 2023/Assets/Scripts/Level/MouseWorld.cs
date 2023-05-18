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
        Cursor.visible = false;
    }

    void Update()
    {

        this.transform.position = MouseWorld.GetPosition();
    }

    public static Vector2 GetPosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }

}
