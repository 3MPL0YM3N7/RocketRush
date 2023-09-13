using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void ExitApp()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pushed Escape to quit game");
            Application.Quit();
        }
    }

    void Update()
    {
        ExitApp();
    }
}
