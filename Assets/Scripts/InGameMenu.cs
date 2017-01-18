using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject InGameMenuObject;

    private void Start()
    {
        InGameMenuObject.SetActive(false);
        Unpause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InGameMenuObject.SetActive(!InGameMenuObject.activeSelf);
            if (InGameMenuObject.activeSelf)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }
}
