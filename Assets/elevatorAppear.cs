using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorAppear : MonoBehaviour
{
    public Canvas CanvasObject;

    void Start()
    {
        CanvasObject = GetComponent<Canvas>();
        CanvasObject.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            CanvasObject.enabled = !CanvasObject.enabled;
            if (CanvasObject.enabled)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
