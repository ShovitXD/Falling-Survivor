using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinLoseHandler : MonoBehaviour
{
    public MovemeontP PlayerScipt;
    public GameObject WonPanel;
    public GameObject LostPanel;

    private void Awake()
    {
        Time.timeScale = 1f;   
        WonPanel.SetActive(false);
        LostPanel.SetActive(false);
    }
    private void FixedUpdate()
    {
        if(PlayerScipt.won == true)
        {
            Won();
        }
        if (PlayerScipt.lost == true)
        {
            Lost();
        }
    }

    void Won()
    {
        WonPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    void Lost()
    {
        LostPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
}
